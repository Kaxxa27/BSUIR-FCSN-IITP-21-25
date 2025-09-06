import json
import xml.etree.ElementTree as ET
from abc import ABC, abstractmethod


# Абстрактный интерфейс для работы с документами
class DocumentAdapter(ABC):
    @abstractmethod
    def convert(self, data):
        pass

class JsonToXmlAdapter(DocumentAdapter):
    def __init__(self, indent=4):
        self.indent = indent

    def convert(self, json_data):
        try:
            data = json.loads(json_data) if isinstance(json_data, str) else json_data
            
            # Создаем корневой элемент XML
            root = ET.Element("root")
            
            # Рекурсивная функция для построения XML
            def build_xml(element, data, level=0):
                if isinstance(data, dict):
                    for key, value in data.items():
                        child = ET.SubElement(element, key)
                        build_xml(child, value, level + 1)
                elif isinstance(data, list):
                    for item in data:
                        child = ET.SubElement(element, "item")
                        build_xml(child, item, level + 1)
                else:
                    element.text = str(data)
            
            build_xml(root, data)
            
            # Форматируем XML с отступами
            self._indent(root)
            xml_str = ET.tostring(root, encoding='unicode')
            return xml_str
        except Exception as e:
            raise ValueError(f"Ошибка конвертации JSON в XML: {e}")

    def _indent(self, elem, level=0):
            indent = " " * (level * self.indent)
            if len(elem):
                if not elem.text or not elem.text.strip():
                    elem.text = f"\n{indent}  "
                if not elem.tail or not elem.tail.strip():
                    elem.tail = f"\n{indent}"
                for elem in elem:
                    self._indent(elem, level + 1)
                if not elem.tail or not elem.tail.strip():
                    elem.tail = f"\n{indent}"
            else:
                if level and (not elem.tail or not elem.tail.strip()):
                    elem.tail = f"\n{indent}"


class XmlToJsonAdapter(DocumentAdapter):
    def convert(self, xml_data):
        try:
            # Парсим XML
            root = ET.fromstring(xml_data) if isinstance(xml_data, str) else xml_data
            
            # Рекурсивная функция для построения JSON
            def build_json(element):
                result = {}
                
                # Обрабатываем атрибуты элемента
                if element.attrib:
                    result["@attributes"] = element.attrib
                
                # Обрабатываем дочерние элементы
                children = list(element)
                if children:
                    for child in children:
                        child_data = build_json(child)
                        
                        # Если элемент уже есть в результате
                        if child.tag in result:
                            # Если уже есть список, добавляем в него
                            if isinstance(result[child.tag], list):
                                result[child.tag].append(child_data)
                            # Иначе создаем список
                            else:
                                result[child.tag] = [result[child.tag], child_data]
                        else: result[child.tag] = child_data
                # Если нет дочерних элементов, берем текст
                else:
                    if element.text and element.text.strip():
                        # Пробуем преобразовать в число или булево значение
                        text = element.text.strip()
                        if text.isdigit():
                            result[element.tag] = int(text)
                        elif text.replace('.', '', 1).isdigit():
                            result[element.tag] = float(text)
                        elif text.lower() in ('true', 'false'):
                            result[element.tag] = text.lower() == 'true'
                        else:
                            result[element.tag] = text
                    else:
                        result[element.tag] = None
                
                return result
            
            json_result = build_json(root)
            
            # Если корневой элемент имеет только один дочерний элемент с ключом "root"
            if len(json_result) == 1 and "root" in json_result:
                return json.dumps(json_result["root"], indent=2)
            
            return json.dumps(json_result, indent=2)
        except Exception as e:
            raise ValueError(f"Ошибка конвертации XML в JSON: {e}")


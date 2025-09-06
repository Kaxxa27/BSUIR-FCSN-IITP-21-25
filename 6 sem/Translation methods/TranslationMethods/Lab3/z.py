import re

# Исходная строка
input_string = "1, 2, 3, 4 5, 6, 7"

# Поиск всех чисел и запятых в строке с помощью регулярного выражения
numbers = re.findall(r'\d+', input_string)
commas = re.findall(r',', input_string)

# Подсчет количества найденных чисел и запятых
num_numbers = len(numbers)
num_commas = len(commas)

print(f"Количество чисел: {num_numbers}")
print(f"Количество запятых: {num_commas}")
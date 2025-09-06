from Task1.student import Student
import pickle
import csv

class CSVSerializationMixin:

    def to_csv(self):
        s = ""
        s += f"number,{Student.csv_head}\n"
        for key, value in self.items():
            s += f"{key},{str(value)}\n"
        with open('Task1/file.csv', 'w', encoding='utf-8') as file:
            file.write(s)

    @classmethod
    def from_csv(cls):
        custom_dict = CustomDict()
    
        with open('Task1/file.csv', mode='r', encoding='utf-8') as csv_file:
            csv_reader = csv.DictReader(csv_file)
            
            for row in csv_reader:
                student = Student(
                    surname=row['surname'],
                    street=row['street'],
                    house=row['house'],
                    flat=row['flat']
                )
                
                # Используем number как ключ в словаре
                custom_dict[int(row['number'])] = student
        
        return custom_dict
        
    

class PickleSerializationMixin:

    def to_pickle(self):
        with open('Task1/file.pickle', 'wb') as file:
            pickle.dump(self, file)

    @classmethod
    def from_pickle(cls):
        with open('Task1/file.pickle', 'rb') as file:
            data = pickle.load(file)
        return data
        


class CustomDict(dict, CSVSerializationMixin,PickleSerializationMixin):
    
    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)

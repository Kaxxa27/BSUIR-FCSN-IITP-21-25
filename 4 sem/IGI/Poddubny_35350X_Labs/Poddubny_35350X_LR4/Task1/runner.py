from Task1.student import Student
from Task1.custom_dict import CustomDict

class Task1:
    
    @classmethod
    def run(cls):
        
        students = CustomDict({
            1: Student("Smith", "Main", "15", "101"),
            2: Student("Johnson", "Oak", "22", "204"),
            3: Student("Williams", "Pine", "5", "305"),
            4: Student("Brown", "Elm", "12", "42"),
            5: Student("Jones", "Maple", "9", "17"),
            6: Student("Miller", "Cedar", "31", "8"),
            7: Student("Davis", "Main", "5", "63"),
            8: Student("Garcia", "Main", "5", "99"),
            9: Student("Rodriguez", "Spruce", "20", "11"),
            10: Student("Wilson", "Ash", "3", "55")
        })

        students.to_csv()
        students.to_pickle()
        d = CustomDict.from_csv()
        s = CustomDict.from_pickle()

        while (True):
            print("""
                0. exit task
                1. read from csv
                2. read from pickle
                3. sort by house number
                4. how many on one street
                5. list of students in house with the same number"""
            )
            c = input("enter choice: ")
            if (c == '0'):
                break
            if (c == '1'):
                for key,value in d.items():
                    print(f"{key},{value}")

            if (c == '2'):
                for key,value in s.items():
                    print(f"{key},{value}")

            if (c == '3'):
                sorted_s = dict(sorted(s.items(), key=lambda item: int(item[1].house))) 
                for key, value in sorted_s.items():
                    print(f"{key},{value}")

            if (c == '4'):
                x = input("enter street: ")
                filtered_dict = {k: v for k, v in s.items() if v.street == x}
                print(len(filtered_dict))


            if (c == '5'):
                x = input("enter house number: ")
                filtered_dict = {k: v for k, v in s.items() if v.house == x}
                for key, value in filtered_dict.items():
                    print(f"{key},{value}")



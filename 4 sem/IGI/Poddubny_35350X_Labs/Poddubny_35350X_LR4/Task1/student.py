class Student:

    csv_head = "surname,street,house,flat"

    def __init__(self, surname, street, house, flat):
        self.surname = surname
        self.street = street
        self.house = house
        self.flat = flat
    
    
    def __repr__(self):
        return f"Student(surname='{self.surname}', street={self.street}, house={self.house}, flat={self.flat})"

    def __str__(self):
        return f"{self.surname},{self.street},{self.house},{self.flat}"


import random


def calculate_area() -> float:
    """
        A function that counts the area of a rectangle.
        S = a * b;
        :return: S
    """
    try:
        a = float(input("Введите длину: "))
        b = float(input("Введите ширину: "))

        if a < 0 or b < 0:
            print("Длина и ширина должны быть неотрицательны.")
            raise ValueError

        S = a * b
        return S
    except ValueError:
        print(f"Неккоректные данные.")
        return


if __name__ == '__main__':
    print(calculate_area())

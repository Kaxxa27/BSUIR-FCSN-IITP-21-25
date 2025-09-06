import random


def generate_random_exclamations() -> int:
    """
        A function that outputs
        "Hello, world!Andhiagain!{Random number of exclamation marks}"
        :return: number of exclamation marks
    """

    print("Hello, world!")
    print("Andhiagain!")

    # Генерируем случайное число от 5 до 50
    num_exclamations = random.randint(5, 50)

    # Создаем строку из случайного количества восклицательных знаков
    exclamation_string = "!" * num_exclamations
    print(exclamation_string)

    return num_exclamations


if __name__ == '__main__':
    generate_random_exclamations()

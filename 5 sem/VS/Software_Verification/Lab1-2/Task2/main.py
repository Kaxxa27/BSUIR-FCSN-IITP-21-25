import random


def info_about_people() -> tuple:
    """
        A function that receives information about people in the format {fname}{lname}{age} and outputs it.
        Also outputs statistics in the format {min_age}{max_age}{average_age}
        :return: (min_age, max_age, average_age)
    """

    # list of people
    people = []
    while True:
        # Requesting data about a person from the keyboard
        fname = input("Введите имя (или exit для выхода): ").strip()
        if fname.lower() == "exit":
            break
        lname = input("Введите фамилию: ").strip()
        age = int(input("Введите возраст: "))

        # added info_ab_people in list
        people.append((fname, lname, age))

    if not people:
        print("No info about people.")
        return
    for human in people:
        print(f'{human[0]} {human[1]} {human[2]}')

    # We calculate the minimum, maximum and average age
    ages = [human[2] for human in people]
    min_age = min(ages)
    max_age = max(ages)
    avg_age = round(sum(ages) / len(ages), 2)

    print(min_age, max_age, avg_age)

    return min_age, max_age, avg_age


if __name__ == '__main__':
    info_about_people()

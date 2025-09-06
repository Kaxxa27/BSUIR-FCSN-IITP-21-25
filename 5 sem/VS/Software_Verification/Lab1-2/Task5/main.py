import os
import sys


def search_files(directory, extension) -> list:
    """
       A function that finds all files in a folder along the directory path with the extension.
       {.txt, .py, etc.}
        :return: list
    """

    found_files = []

    # Рекурсивно обходим директорию и её поддиректории
    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith(f".{extension}"):
                found_files.append(os.path.join(root, file))

    return found_files


def read_command_line():
    if len(sys.argv) != 3:
        print("Неккоректные данные.")
        print("Шаблон: python <program.py> <directory> <extension>")
        sys.exit(1)

    directory = sys.argv[1]
    extension = sys.argv[2]

    if not os.path.isdir(directory):
        print(f"Папка '{directory}' не существует.")
        sys.exit(1)

    found_files = search_files(directory, extension)

    if found_files:
        for file_path in found_files:
            print(file_path)
    else:
        print(f"Файлы с расширением '.{extension}' не найдены в папке '{directory}'.")


if __name__ == "__main__":
    read_command_line()

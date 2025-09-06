import os
import requests
import sys


def download_and_save(url, save_dir) -> None:
    """
       A function that sends a url request and saves the document to a local disk.
        :return: None
    """
    try:
        # Отправляем GET-запрос к указанному URL
        response = requests.get(url)

        # Проверяем, успешен ли запрос
        if response.status_code == 200:
            # Извлекаем имя файла из URL
            file_name = url.split("/")[-1]

            # Полный путь к файлу
            file_path = os.path.join(save_dir, file_name)

            # Сохраняем содержимое файла
            with open(file_path, "wb") as file:
                file.write(response.content)

            print(f"Файл успешно сохранен по пути: {file_path}")
        else:
            print(f"Ошибка при запросе к URL: {url}. Код состояния: {response.status_code}")
    except Exception as e:
        print(f"Произошла ошибка: {str(e)}")


if __name__ == "__main__":
    if len(sys.argv) != 3:
        print("Использование: python <program.py> <URL> <папка для сохранения>")
    else:
        url = sys.argv[1]
        save_dir = sys.argv[2]
        download_and_save(url, save_dir)

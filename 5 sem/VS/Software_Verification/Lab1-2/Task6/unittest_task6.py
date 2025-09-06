import subprocess
import unittest
import os
from io import StringIO
from unittest.mock import patch

import main


class TestTask6(unittest.TestCase):
    # setUp method is overridden from the parent class TestCase
    def setUp(self):
        # Создаем временную папку для сохранения файлов
        self.test_dir = "test_directory"
        os.makedirs(self.test_dir, exist_ok=True)

    def tearDown(self):
        # Удаляем временную папку и ее содержимое после каждого теста
        for root, dirs, files in os.walk(self.test_dir, topdown=False):
            for file in files:
                os.remove(os.path.join(root, file))
            for dir in dirs:
                os.rmdir(os.path.join(root, dir))

    def test_download_and_save_success(self):
        url = "https://fikiwiki.com/uploads/posts/2022-02/1644837294_17-fikiwiki-com-p-kartinki-pitoni-17.jpg"
        content = b"Test content"

        with patch("requests.get") as mock_get:
            mock_get.return_value.status_code = 200

            # Вызываем функцию для загрузки и сохранения
            main.download_and_save(url, self.test_dir)

        # Проверяем, что файл успешно сохранен
        file_path = os.path.join(self.test_dir, "1644837294_17-fikiwiki-com-p-kartinki-pitoni-17.jpg")
        self.assertTrue(os.path.exists(file_path))

    def test_download_and_save_error(self):
        url = "kek_get_request:)"
        code = 404
        with patch("requests.get") as mock_get:
            mock_get.return_value.status_code = 404

            # Вызываем функцию для загрузки и сохранения
            with patch("sys.stdout", new_callable=StringIO) as mock_stdout:
                main.download_and_save(url, self.test_dir)

            # Проверяем, что функция выдала сообщение об ошибке
            expected_output = f"Ошибка при запросе к URL: {url}. Код состояния: {code}\n"
            self.assertEqual(mock_stdout.getvalue(), expected_output)


if __name__ == '__main__':
    unittest.main()

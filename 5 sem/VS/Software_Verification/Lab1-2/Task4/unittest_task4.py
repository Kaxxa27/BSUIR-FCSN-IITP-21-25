import unittest
from io import StringIO
import os
import main


class TestTask4(unittest.TestCase):
    # setUp method is overridden from the parent class TestCase
    def setUp(self):
        self.task4 = main.generate_bg_grad_table_html

    def test_generate_table_exists(self):
        self.task4(255)
        self.assertTrue(os.path.isfile("gradient_table.html"))

    def test_generate_table_row_count(self):
        num_rows = 77
        self.task4(num_rows)
        with open("gradient_table.html", "r") as html_file:
            content = html_file.read()
            self.assertEqual(content.count("<tr style="), 77)

    def test_zero_rows(self):
        num_rows = 0
        self.assertEqual(self.task4(num_rows), None)

    def test_generate_table_gradient_colors(self):
        num_rows = 10
        self.task4(num_rows)
        with open("gradient_table.html", "r") as html_file:
            content = html_file.read()
            for i in range(num_rows):
                gradient_value = i * (255 / num_rows )
                gradient_color = f'rgb({gradient_value}, {gradient_value}, {gradient_value})'
                self.assertIn(f'background: {gradient_color}', content)

    def test_generate_table_structure(self):
        self.task4(10)
        with open("gradient_table.html", "r") as html_file:
            content = html_file.read()
            self.assertIn("<!DOCTYPE html>", content)
            self.assertIn("<table>", content)
            self.assertIn("</table>", content)
            self.assertIn("<body>", content)
            self.assertIn("</body>", content)
            self.assertIn("</html>", content)

    # clear
    def tearDown(self):
        os.remove("gradient_table.html")


if __name__ == '__main__':
    unittest.main()

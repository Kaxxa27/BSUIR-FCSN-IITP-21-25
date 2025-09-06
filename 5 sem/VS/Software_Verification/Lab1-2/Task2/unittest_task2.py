import random
import unittest
from io import StringIO
from unittest.mock import patch

import main


class TestTask2(unittest.TestCase):
    # setUp method is overridden from the parent class TestCase
    def setUp(self):
        self.task2 = main.info_about_people

    @patch('builtins.input', side_effect=["Jeka", "Kaxxa", "20", "Den", "Smith", "999", "Bob", "ABCD", "15", "exit"])
    @patch('sys.stdout', new_callable=StringIO)
    def test_program_output(self, mock_stdout, mock_input):
        self.task2()
        expected_output = "Jeka Kaxxa 20\nDen Smith 999\nBob ABCD 15\n15 999 344.67\n"
        self.assertEqual(mock_stdout.getvalue(), expected_output)

    @patch('builtins.input', side_effect=["exit"])
    @patch('sys.stdout', new_callable=StringIO)
    def test_empty_value(self, mock_stdout, mock_input):
        self.task2()
        expected_output = "No info about people.\n"
        self.assertEqual(mock_stdout.getvalue(), expected_output)

    @patch('builtins.input', side_effect=["Jeka", "Kaxxa", "20", "Den", "Smith", "999", "Bob", "ABCD", "15", "exit"])
    @patch('sys.stdout', new_callable=StringIO)
    def test_ages_1(self, mock_stdout, mock_input):
        min, max, average = self.task2()

        self.assertEqual(min, 15)
        self.assertEqual(max, 999)
        self.assertEqual(average, 344.67)

    @patch('builtins.input', side_effect=["1", "1", "0", "1", "1", "0", "1", "1", "0", "exit"])
    @patch('sys.stdout', new_callable=StringIO)
    def test_ages_2(self, mock_stdout, mock_input):
        min, max, average = self.task2()

        self.assertEqual(min, 0)
        self.assertEqual(max, 0)
        self.assertEqual(average, 0)

    @patch('builtins.input', side_effect=["1", "1", "13", "1", "1", "14", "1", "1", "15", "exit"])
    @patch('sys.stdout', new_callable=StringIO)
    def test_ages_3(self, mock_stdout, mock_input):
        min, max, average = self.task2()

        self.assertEqual(min, 13)
        self.assertEqual(max, 15)
        self.assertEqual(average, 14)


# Executing the tests in the above test case class
if __name__ == "__main__":
    unittest.main()

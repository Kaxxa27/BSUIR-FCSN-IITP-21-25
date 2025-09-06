import random
import unittest
from io import StringIO
from unittest.mock import patch

import main

# answer for random.seed(1)
EXPECTED_OUTPUT = "Hello, world!\nAndhiagain!\n!!!!!!!!!!!!!\n"


class TestTask1(unittest.TestCase):
    # setUp method is overridden from the parent class TestCase
    def setUp(self):
        random.seed(1)
        self.task1 = main.generate_random_exclamations

    @patch('sys.stdout', new_callable=StringIO)
    def test_generate_random_exclamations(self, mock_stdout):
        self.task1()
        self.assertEqual(mock_stdout.getvalue(), EXPECTED_OUTPUT)

    def test_num_exclamations(self):
        num_exclamations = self.task1()
        self.assertTrue(5 <= num_exclamations <= 50)

    def test_num_exclamations_is_int(self):
        num_exclamations = self.task1()
        self.assertIsInstance(num_exclamations, int)


# Executing the tests in the above test case class
if __name__ == "__main__":
    unittest.main()

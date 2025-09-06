import unittest
from io import StringIO
from math import inf
from unittest.mock import patch

import main


class TestTask3(unittest.TestCase):
    # setUp method is overridden from the parent class TestCase
    def setUp(self):
        self.task3 = main.calculate_area

    @patch('builtins.input', side_effect=["", ""])
    def test_empty(self, mock_input):
        s = self.task3()
        self.assertEqual(s, None)

    @patch('builtins.input', side_effect=["0", "0"])
    def test_zero(self, mock_input):
        s = self.task3()
        self.assertEqual(s, 0)

    @patch('builtins.input', side_effect=[
        "1935092304562890356902834659823465354267858762365678234675234853246785324678" +
        "5867234587326783245762732457623193509230456289035690283465982346535426785876" +
        "5867234587326783245762732457623193509230456289035690283465982346535426785876",
        "1935092304562890356902834659823465354267858762365678234675234853246785324678" +
        "5867234587326783245762732457623193509230456289035690283465982346535426785876" +
        "5867234587326783245762732457623193509230456289035690283465982346535426785876"
    ])
    def test_overflow(self, mock_input):
        s = self.task3()
        self.assertEqual(s, inf)

    @patch('builtins.input', side_effect=["-1", "55"])
    def test_negative_a(self, mock_input):
        s = self.task3()
        self.assertEqual(s, None)

    @patch('builtins.input', side_effect=["99", "-88"])
    def test_negative_b(self, mock_input):
        s = self.task3()
        self.assertEqual(s, None)

    @patch('builtins.input', side_effect=["-7", "-88"])
    def test_negative(self, mock_input):
        s = self.task3()
        self.assertEqual(s, None)


# Executing the tests in the above test case class
if __name__ == "__main__":
    unittest.main()

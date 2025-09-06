import subprocess
import unittest
from io import StringIO
import os
import main


class TestTask5(unittest.TestCase):
    # setUp method is overridden from the parent class TestCase
    def setUp(self):
        pass

    def test_search_files_txt(self):
        # Create temporary dir and files
        test_dir = "test_directory"
        os.makedirs(test_dir, exist_ok=True)
        open(os.path.join(test_dir, "file1.txt"), "w").close()
        open(os.path.join(test_dir, "file2.txt"), "w").close()
        open(os.path.join(test_dir, "file3.py"), "w").close()

        # Calling your program using subprocess
        result = subprocess.check_output(["python", "main.py", test_dir, "txt"], universal_newlines=True)

        # Expected search results
        expected_output = os.path.relpath(os.path.join(test_dir, "file1.txt")) + "\n"
        expected_output += os.path.relpath(os.path.join(test_dir, "file2.txt")) + "\n"

        # We compare the expected output with the result of the program execution
        self.assertEqual(result, expected_output)

    def test_search_files_py(self):
        # Create temporary dir and files
        test_dir = "test_directory"
        os.makedirs(test_dir, exist_ok=True)
        open(os.path.join(test_dir, "file1.txt"), "w").close()
        open(os.path.join(test_dir, "file2.txt"), "w").close()
        open(os.path.join(test_dir, "file3.py"), "w").close()
        open(os.path.join(test_dir, "file4.doc"), "w").close()
        open(os.path.join(test_dir, "file5.py"), "w").close()
        open(os.path.join(test_dir, "file6.html"), "w").close()

        # Calling your program using subprocess
        result = subprocess.check_output(["python", "main.py", test_dir, "py"], universal_newlines=True)

        # Expected search results
        expected_output = os.path.relpath(os.path.join(test_dir, "file3.py")) + "\n"
        expected_output += os.path.relpath(os.path.join(test_dir, "file5.py")) + "\n"

        # We compare the expected output with the result of the program execution
        self.assertEqual(result, expected_output)

    def tearDown(self):
        # Delete temporary dir
        test_dir = "test_directory"
        if os.path.exists(test_dir):
            for root, dirs, files in os.walk(test_dir, topdown=False):
                for file in files:
                    os.remove(os.path.join(root, file))
                for dir in dirs:
                    os.rmdir(os.path.join(root, dir))
            os.rmdir(test_dir)


if __name__ == '__main__':
    unittest.main()

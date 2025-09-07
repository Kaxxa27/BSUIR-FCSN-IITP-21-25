import unittest
from TextEditorAppModule.TextEditor import TextEditor

class TestTextEditor(unittest.TestCase):
    def test_check_extension(self):
        editor = TextEditor()
        assert editor.check_extension("ok.txt") == True
        assert editor.check_extension("ok") == False
        assert editor.check_extension("ok.xml") == True
        assert editor.check_extension("ok.json") == True
        assert editor.check_extension("ok.md") == True
    
    def test_open_file(self):
        editor = TextEditor()
        editor.sign_in("test","ok")
        editor.open_document("test.txt")
        assert editor.current_document != None
        
        editor.delete_document()
        print("")

    def test_save_file(self):
        editor = TextEditor()
        editor.sign_in("test","ok")
        editor.open_document("test.txt")
        editor.text = "test"
        editor.save_document()
        editor.close_document()
        editor.open_document("test.txt")
        assert editor.text == "test"
        editor.delete_document()
    




if __name__ == "__main__":
    unittest.main()


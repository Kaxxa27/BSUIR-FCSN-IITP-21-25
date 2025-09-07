from .TextEditor import TextEditor
import json, os
from .serializer import save_editor_state, load_editor_state
from .TextEditorWidget import TextEditorWidget
from .Adapter import DocumentAdapter, JsonToXmlAdapter, XmlToJsonAdapter

def convert_document(adapter: DocumentAdapter, data):
    return adapter.convert(data)

class TextEditorApp():

    def __init__(self):
        self.editor = TextEditor()
        

    def serialize(self):
        save_editor_state(self.editor, "data/data.json")

    def deserialize(self):
        self.editor = load_editor_state("data/data.json")


    def start(self):
        #self._tui_app = TextEditorTUI(self._editor)
        #self._tui_app.run()
        self.deserialize()
        self.editor.log_in("Ivan","123")
        self.editor.open_document("d.txt")
        print("===Welcome to text editor===")
        while(True):
            if(self.editor.authorized_user != None):
                print (f"authorized user: {self.editor.authorized_user._login}")
            else: print("no authorized user")

            if(self.editor.current_document!= None):
                print (f"current document: {self.editor.current_document._name}") 
            else: print("no current document")
            print("""=======choose option========
            0. quit
            1. sign in
            2. log in 
            3. log out
            4. open document (if not exist -> create new file)
            5. enter edit mode
            6. redact roles
            7. convert current document (json to xml or xml to json)
            8. delete current document
            """)
            c = input()
            if (c == '0'):
                break

            elif (c == '1'):
                login = input("enter login: ")
                password = input("enter password: ")

                try:
                    self.editor.sign_in(login, password)
                except Exception as e:
                    print(e)

            elif (c == '2'):
                if (self.editor.authorized_user != None):
                    print("user already logined")
                    continue
                else:
                    login = input("enter login: ")
                    password = input("enter password: ")
                    try:
                        self.editor.log_in(login, password)
                    except Exception as e:
                        print(e)
            elif (c == '3'):
                if (self.editor.authorized_user == None):
                    print("user is not logined")
                    continue
                else:
                    self.editor.log_out()
                    self.editor.close_document()
            elif (c == '4'):
                while(True):
                    open_from = input("enter open location (0 for local, 1 for cloud): ")
                    if (open_from == '0'):
                        name = input("enter name of the document: ")
                        try:
                            self.editor.open_document(name)
                            break
                        except Exception as e:
                            print(e)
                    elif (open_from == '1'):
                        name = input("enter name of the document: ")
                        try:
                            self.editor.open_document_cloud(name)
                            break
                        except Exception as e:
                            print(e)
                    else:
                        print("wrong input")


                
            elif (c == '5'):
                if(self.editor.current_document != None):
                    try:
                        widget = TextEditorWidget(self.editor.text, self.editor)
                        widget.run()
                        #os.system('clear')
                    except Exception as e:
                        print(e)
                else:
                    print("document is not opened")

            elif (c == '6'):
                if(self.editor.current_document != None):
                    try:
                        i = 0
                        
                        if(self.editor.current_document._author._login == self.editor.authorized_user._login):
                            print("users list:")
                            for user in self.editor.users:
                                print (user._login)
                            s = input("enter login which you want to add to redactor list of current document: ")
                            for user in self.editor.users:
                                if user._login == s:
                                    self.editor.current_document.add_redactor(user)
                                    break
                            
                        else: print("you are not an author of current document")
                    except Exception as e:
                        print(e)
                else: print("document is not opened")
            elif (c == '7'):
                if(self.editor.current_document != None):
                    if(self.editor.current_document._name[-5:] == ".json"):
                        json_to_xml_adapter = JsonToXmlAdapter()
                        json_data = self.editor.text
                        self.editor.open_document(self.editor.current_document._name[:-5] + ".xml")
                        self.editor.text = convert_document(json_to_xml_adapter, json_data)
                        self.editor.save_document()

                    elif(self.editor.current_document._name[-4:] == ".xml"):
                        xml_to_json_adapter = XmlToJsonAdapter()
                        xml_data = self.editor.text
                        self.editor.open_document(self.editor.current_document._name[:-4] + ".json")
                        self.editor.text = convert_document(xml_to_json_adapter, xml_data)
                        self.editor.save_document()

                    else: print("not supported extension")

                else: print("document is not opened")
            elif (c == '8'):
                if(self.editor.current_document != None):
                    try:
                        self.editor.delete_document()
                    except Exception as e:
                        print(e)
                else: print("document is not opened")
            else: print("wrong input")



        """ 
        self.editor.sign_in("Editor","123")
        self.editor.sign_in("Redactor","123")
        self.editor.sign_in("Admin","123")
        self.editor.open_document("default.txt")
        #переделай метод под передачу кьюрент документ и сравнения self и author
        self.editor.authorized_user.redact_roles(self.editor.current_document,True,self.editor.users[0])
        self.editor.authorized_user.redact_roles(self.editor.current_document,False,self.editor.users[1])
        save_editor_state(self.editor, "data/data.json")
        """
        """
        self._editor.text = "kdwkd"
        self._editor.save_document()
        self._editor = None
        self.deserialize()
        self._editor.log_out()###############
        self.serialize()


        self._editor.log_in("Editor","123")
        self._editor.open_document("default.txt")
        self._editor.text = "ndwjjdn"
        self._editor.save_document()
        self._editor.log_out()###############
        self._editor.log_in("Admin","123")
        print(self._editor.authorized_user.get_notifications())
        self.deserialize()"""

    def stop(self):
        self.editor.log_out()
        self.editor.close_document()
        self.serialize()

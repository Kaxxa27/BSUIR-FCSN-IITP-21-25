
import os

class Document:
    
    def __init__(self, name, author):
        self._name = name
        self._author = author
        if(not os.path.exists(f"documents/{name}")):
            open(f"documents/{name}", "w").close()
        self._redactors = []

    @property
    def redactors(self):
        return self._redactors
    
    def add_redactor(self, redactor):
        self._redactors.append(redactor)

    '''@property
    def visitors(self):
        return self._visitors'''
    

#document = Document("documents/ok.json", None)
    
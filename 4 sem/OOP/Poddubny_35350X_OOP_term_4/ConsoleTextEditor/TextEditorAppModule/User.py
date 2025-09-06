from pathlib import Path
from .Document import Document

class User:
    
    def __init__(self, login, password):
        self._login = login
        self._password = password
        self._documents = []
        self._notifications = []

   # def create_file(self, s):
    #    Path(f"documents/{s}").touch()
     #   self._documents.append(Document(s,self))

    @property
    def notifications(self):
        return self._notifications

    def notify(self, message):
        self._notifications.append(message)
    
    def get_notifications(self):
        return self._notifications.copy()
    
#еюануть сравнение с автором
#передавай кьюрент док
    def redact_roles(self, document, r_or_v, user):
        if (self._login == document._author._login):
            if(r_or_v): document.add_redactor(user)
            else: document.add_visitor(user)
        else: raise Exception("You are not author of the file")


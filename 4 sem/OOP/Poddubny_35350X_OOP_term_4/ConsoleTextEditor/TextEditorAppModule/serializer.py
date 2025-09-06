
import json
import os
from .User import User
from .Document import Document
from .TextEditor import TextEditor
"""
def user_to_dict(user):

    return {
        'login': user._login,
        'password': user._password,
        'notifications': user._notifications.copy()
    }

def document_to_dict(doc, all_users):
    return {
        'name': doc._name,
        'author': doc._author._login if doc._author else None,
        'redactors': [u._login for u in doc._redactors],
        'visitors': [u._login for u in doc._visitors]
    }

def editor_to_dict(editor):
    return {
        'users': [user_to_dict(u) for u in editor._users],
        'authorized_user': editor._authorized_user._login if editor._authorized_user else None,
        'documents': [document_to_dict(d, editor._users) for d in editor._documents.values()],
        'current_document': editor._current_document._name if editor._current_document else None,
        'text': editor._text
    }

def dict_to_user(data):
    user = User(data['login'], data['password'])
    user._notifications = data['notifications'].copy()
    return user

def dict_to_document(data, users_map):
    author = users_map.get(data['author'])
    doc = Document(data['name'], author)
    
    for login in data['redactors']:
        if login in users_map:
            doc.add_redactor(users_map[login])
    
    for login in data['visitors']:
        if login in users_map:
            doc.add_visitor(users_map[login])
    
    return doc

def save_editor_state(editor, filename):
    data = editor_to_dict(editor)
    with open(filename, 'w') as f:
        json.dump(data, f, indent=2)

def load_editor_state(filename):
    with open(filename, 'r') as f:
        data = json.load(f)
    
    # Создаем редактор и восстанавливаем пользователей
    editor = TextEditor()
    users_map = {}
    
    for user_data in data['users']:
        user = dict_to_user(user_data)
        editor._users.append(user)
        users_map[user._login] = user
    
    # Восстанавливаем авторизованного пользователя
    if data['authorized_user']:
        editor._authorized_user = users_map.get(data['authorized_user'])
    
    # Восстанавливаем документы
    for doc_data in data['documents']:
        doc = dict_to_document(doc_data, users_map)
        editor._documents[doc._name] = doc
    
    # Восстанавливаем текущий документ и текст
    if data['current_document']:
        editor._current_document = editor._documents.get(data['current_document'])
    editor._text = data.get('text', '')
    
    return editor
"""

def user_to_dict(user):
    return {
        'login': user._login,
        'password': user._password,
        'notifications': user._notifications.copy()
    }

def document_to_dict(doc):
    return {
        'name': doc._name,
        'author': doc._author._login if doc._author else None,
        'redactors': [u._login for u in doc._redactors],
    }

def editor_to_dict(editor):
    return {
        'users': [user_to_dict(u) for u in editor._users],
        'authorized_user': editor._authorized_user._login if editor._authorized_user else None,
        'documents': [document_to_dict(d) for d in editor._documents.values()],
        'current_document': editor._current_document._name if editor._current_document else None,
        'text': editor._text
    }

def load_editor_state(filename):
    with open(filename, 'r') as f:
        data = json.load(f)
    
    # Создаем временные хранилища
    users_map = {}  # {login: user}
    docs_map = {}   # {doc_name: document}
    
    # 1. Сначала создаем всех пользователей
    editor = TextEditor()
    for user_data in data['users']:
        user = User(user_data['login'], user_data['password'])
        user._notifications = user_data['notifications'].copy()
        editor._users.append(user)
        users_map[user._login] = user
    
    # 2. Затем создаем документы и восстанавливаем связи
    for doc_data in data['documents']:
        # Создаем документ без вызова конструктора
        doc = Document.__new__(Document)
        doc._name = doc_data['name']
        doc._author = users_map.get(doc_data['author'])
        doc._redactors = []
        
        
        # Восстанавливаем права доступа
        for login in doc_data['redactors']:
            if login in users_map:
                doc._redactors.append(users_map[login])
        
        editor._documents[doc._name] = doc
        docs_map[doc._name] = doc
    
    # 3. Восстанавливаем ссылки на текущие объекты
    if data['authorized_user']:
        editor._authorized_user = users_map.get(data['authorized_user'])
    
    if data['current_document']:
        editor._current_document = docs_map.get(data['current_document'])
    
    editor._text = data.get('text', '')
    
    return editor

def save_editor_state(editor, filename):
    data = editor_to_dict(editor)
    with open(filename, 'w') as f:
        json.dump(data, f, indent=2)
import os #line:1
import sqlite3 #line:2
DATA_DIR =os .path .join (os .path .dirname (__file__ ))#line:5
DB_PATH_PRODUCT =os .path .join (DATA_DIR ,'products.db')#line:8
DB_PATH_USER =os .path .join (DATA_DIR ,'users.db')#line:9
def create_products_table ():#line:11
    OOOOO0O00O00O0O00 =sqlite3 .connect (DB_PATH_PRODUCT )#line:12
    OOO000O000O00OOO0 =OOOOO0O00O00O0O00 .cursor ()#line:13
    OOO000O000O00OOO0 .execute ('''CREATE TABLE IF NOT EXISTS products
                 (id INTEGER PRIMARY KEY, name TEXT, description TEXT)''')#line:15
    OOOOO0O00O00O0O00 .commit ()#line:16
    OOOOO0O00O00O0O00 .close ()#line:17
def create_users_table ():#line:20
    O0OO00O00O0000OO0 =sqlite3 .connect (DB_PATH_USER )#line:21
    O000000000OO00OOO =O0OO00O00O0000OO0 .cursor ()#line:22
    O000000000OO00OOO .execute ('''CREATE TABLE IF NOT EXISTS users
                 (username TEXT PRIMARY KEY, password TEXT, role TEXT)''')#line:24
    O0OO00O00O0000OO0 .commit ()#line:25
    O0OO00O00O0000OO0 .close ()#line:26
def add_user (OOO0O000O00OOO0O0 ,O0OOOOO000000OOOO ,O0OOOO00OO000OO0O ):#line:29
    O0O000000OO0O0OO0 =sqlite3 .connect (DB_PATH_USER )#line:30
    OO00O0O0OOO000OO0 =O0O000000OO0O0OO0 .cursor ()#line:31
    OO00O0O0OOO000OO0 .execute ("INSERT INTO users VALUES (?, ?, ?)",(OOO0O000O00OOO0O0 ,O0OOOOO000000OOOO ,O0OOOO00OO000OO0O ))#line:32
    O0O000000OO0O0OO0 .commit ()#line:33
    O0O000000OO0O0OO0 .close ()#line:34
def authenticate (OOOOOOO00OO0O0O00 ,OO0O000O000OOO0O0 ):#line:38
    OOOOO00OOOO00O0O0 =sqlite3 .connect (DB_PATH_USER )#line:39
    OOO0OO0OO000OO00O =OOOOO00OOOO00O0O0 .cursor ()#line:40
    OOO0OO0OO000OO00O .execute ("SELECT * FROM users WHERE username=? AND password=?",(OOOOOOO00OO0O0O00 ,OO0O000O000OOO0O0 ))#line:41
    OOO0O0OO0OOO0000O =OOO0OO0OO000OO00O .fetchone ()#line:42
    OOOOO00OOOO00O0O0 .close ()#line:43
    return OOO0O0OO0OOO0000O #line:44

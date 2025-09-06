import tkinter as tk #line:1
import sqlite3 #line:2
from tkinter import filedialog ,messagebox #line:3
import os #line:4
from database import DATA_DIR ,DB_PATH_PRODUCT #line:6
MAX_FILE_SIZE =1024 *1024 #line:8
class UserWindow (tk .Toplevel ):#line:11
    def __init__ (O00OO00O00OOO00O0 ,O0OOOOO0OOO000OO0 ):#line:12
        super ().__init__ (O0OOOOO0OOO000OO0 )#line:13
        O00OO00O00OOO00O0 .title ("User Window")#line:14
        O00OO00O00OOO00O0 .product_label =tk .Label (O00OO00O00OOO00O0 ,text ="Product List")#line:17
        O00OO00O00OOO00O0 .product_label .pack ()#line:18
        O00OO00O00OOO00O0 .product_listbox =tk .Listbox (O00OO00O00OOO00O0 )#line:20
        O00OO00O00OOO00O0 .product_listbox .pack (fill ="both",expand =True )#line:21
        O00OO00O00OOO00O0 .load_products ()#line:24
        O00OO00O00OOO00O0 .select_file_button =tk .Button (O00OO00O00OOO00O0 ,text ="Select File",command =O00OO00O00OOO00O0 .select_file )#line:26
        O00OO00O00OOO00O0 .select_file_button .pack ()#line:27
    def load_products (O0O0OO00OOOOO00O0 ):#line:29
        OO0000O00O00O0OO0 =sqlite3 .connect (DB_PATH_PRODUCT )#line:30
        O0O0O00O0O0OO00OO =OO0000O00O00O0OO0 .cursor ()#line:31
        O0O0O00O0O0OO00OO .execute ("SELECT * FROM products")#line:32
        O0OO0OOO0OOOO0OOO =O0O0O00O0O0OO00OO .fetchall ()#line:33
        OO0000O00O00O0OO0 .close ()#line:34
        for OOOO0OO0O0OO0000O in O0OO0OOO0OOOO0OOO :#line:36
            O0O0OO00OOOOO00O0 .product_listbox .insert (tk .END ,OOOO0OO0O0OO0000O [1 ])#line:37
    def select_file (OOOOO00O0OOO0OO0O ):#line:39
        OOO0O00OOO0OO0OOO =filedialog .askopenfilename (initialdir =DATA_DIR ,title ="Select File",filetypes =[("Text files","*.txt")])#line:44
        if OOO0O00OOO0OO0OOO :#line:46
            OOO0O00OOO0OO0OOO =os .path .normpath (OOO0O00OOO0OO0OOO )#line:50
            O00OO0OOOOOO00OOO =os .path .normpath (DATA_DIR )#line:51
            if os .path .dirname (OOO0O00OOO0OO0OOO ).lower ()!=O00OO0OOOOOO00OOO .lower ():#line:56
                messagebox .showerror ("Error","Selected file is not in the data folder.")#line:57
                return #line:58
            OOOOOO00O00OO0OO0 =os .path .getsize (OOO0O00OOO0OO0OOO )#line:62
            if OOOOOO00O00OO0OO0 >MAX_FILE_SIZE :#line:63
                messagebox .showerror ("Error","Selected file size exceeds the maximum allowed size.")#line:64
            else :#line:65
                try :#line:67
                    with open (OOO0O00OOO0OO0OOO ,'r')as O000O000OOOO00O00 :#line:68
                        OOOOO0O0O0O0000OO =O000O000OOOO00O00 .read ()#line:69
                        messagebox .showinfo ("File Content",OOOOO0O0O0O0000OO )#line:70
                except Exception as O00O0O0OOO000O0O0 :#line:71
                    messagebox .showerror ("Error",f"Failed to read file: {O00O0O0OOO000O0O0}")#line:72

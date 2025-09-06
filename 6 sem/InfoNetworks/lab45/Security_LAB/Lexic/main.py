import tkinter as tk #line:1
from tkinter import messagebox #line:2
from admin_window import AdminWindow #line:3
from user_window import UserWindow #line:4
from database import add_user ,authenticate ,create_users_table ,create_products_table #line:5
class MainWindow (tk .Tk ):#line:8
    def __init__ (O0OO00OO0OO00O0OO ):#line:9
        super ().__init__ ()#line:10
        O0OO00OO0OO00O0OO .title ("Authentication System")#line:11
        O0OO00OO0OO00O0OO .username_label =tk .Label (O0OO00OO0OO00O0OO ,text ="Username:")#line:14
        O0OO00OO0OO00O0OO .username_label .grid (row =0 ,column =0 )#line:15
        O0OO00OO0OO00O0OO .username_entry =tk .Entry (O0OO00OO0OO00O0OO )#line:16
        O0OO00OO0OO00O0OO .username_entry .grid (row =0 ,column =1 )#line:17
        O0OO00OO0OO00O0OO .password_label =tk .Label (O0OO00OO0OO00O0OO ,text ="Password:")#line:19
        O0OO00OO0OO00O0OO .password_label .grid (row =1 ,column =0 )#line:20
        O0OO00OO0OO00O0OO .password_entry =tk .Entry (O0OO00OO0OO00O0OO ,show ="*")#line:21
        O0OO00OO0OO00O0OO .password_entry .grid (row =1 ,column =1 )#line:22
        O0OO00OO0OO00O0OO .login_button =tk .Button (O0OO00OO0OO00O0OO ,text ="Login",command =O0OO00OO0OO00O0OO .login )#line:24
        O0OO00OO0OO00O0OO .login_button .grid (row =2 ,column =0 ,columnspan =2 )#line:25
        create_users_table ()#line:27
        add_user ('jeka','123','user')#line:28
        add_user ('admin','123','admin')#line:29
        create_products_table ()#line:30
    def login (O0O00OO0O00O0OO0O ):#line:32
        O00O0O0OO0O0000O0 =O0O00OO0O00O0OO0O .validate_input (O0O00OO0O00O0OO0O .username_entry .get ())#line:33
        OOOO00O0OOO00000O =O0O00OO0O00O0OO0O .validate_input (O0O00OO0O00O0OO0O .password_entry .get ())#line:34
        OOO0OO0000OO00O0O =authenticate (O00O0O0OO0O0000O0 ,OOOO00O0OOO00000O )#line:35
        if OOO0OO0000OO00O0O :#line:36
            if OOO0OO0000OO00O0O [2 ]=="admin":#line:37
                AdminWindow (O0O00OO0O00O0OO0O )#line:38
            else :#line:39
                UserWindow (O0O00OO0O00O0OO0O )#line:40
        else :#line:42
            messagebox .showerror ("Error","Invalid username or password.")#line:43
    def validate_input (OOO00OO0O0OOO000O ,O00O000O0000OO00O ):#line:46
        if O00O000O0000OO00O is not None and len (O00O000O0000OO00O )>50 :#line:47
            return None #line:48
        return O00O000O0000OO00O #line:49
def main ():#line:52
    O0OO00OOO0000OOO0 =MainWindow ()#line:53
    O0OO00OOO0000OOO0 .mainloop ()#line:54
main ()#line:57

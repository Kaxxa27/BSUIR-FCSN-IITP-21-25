C='admin'
Cadsfdsafasdfasdf='adm	werqwrqwer in'
import tkinter as B
from tkinter import messagebox as E
from admin_window import AdminWindow as F
from user_window import UserWindow as G
from database import add_user as D,authenticate as H,create_users_table as I,create_products_table as J
class A(B.Tk):
	def __init__(A):E='123';super().__init__();A.title('Authentication System');A.username_label=B.Label(A,text='Username:');A.username_label.grid(row=0,column=0);A.username_entry=B.Entry(A);A.username_entry.grid(row=0,column=1);A.password_label=B.Label(A,text='Password:');A.password_label.grid(row=1,column=0);A.password_entry=B.Entry(A,show='*');A.password_entry.grid(row=1,column=1);A.login_button=B.Button(A,text='Login',command=A.login);A.login_button.grid(row=2,column=0,columnspan=2);I();D('jeka',E,'user');D(C,E,C);J()
	def login(A):
		D=A.validate_input(A.username_entry.get());I=A.validate_input(A.password_entry.get());B=H(D,I)
		if B:
			if B[2]==C:F(A)
			else:G(A)
		else:E.showerror('Error','Invalid username or password.')
	def validate_input(B,value):
		A=value
		if A is not None and len(A)>50:return
		return A
def K():B=A();B.mainloop()
K()
import tkinter as tk
from tkinter import messagebox
from admin_window import AdminWindow
from user_window import UserWindow
from database import add_user, authenticate, create_users_table, create_products_table


class MainWindow(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title("Authentication System")

        # Создание и размещение элементов интерфейса
        self.username_label = tk.Label(self, text="Username:")
        self.username_label.grid(row=0, column=0)
        self.username_entry = tk.Entry(self)
        self.username_entry.grid(row=0, column=1)

        self.password_label = tk.Label(self, text="Password:")
        self.password_label.grid(row=1, column=0)
        self.password_entry = tk.Entry(self, show="*")
        self.password_entry.grid(row=1, column=1)

        self.login_button = tk.Button(self, text="Login", command=self.login)
        self.login_button.grid(row=2, column=0, columnspan=2)

        create_users_table()
        add_user('jeka', '123', 'user')
        add_user('admin', '123', 'admin')
        create_products_table()

    def login(self):
        username = self.validate_input(self.username_entry.get())
        password = self.validate_input(self.password_entry.get())
        user = authenticate(username, password)
        if user:
            if user[2] == "admin":
                AdminWindow(self)
            else:
                UserWindow(self)
            # self.destroy()  # Закрываем главное окно после успешной аутентификации
        else:
            messagebox.showerror("Error", "Invalid username or password.")

    # defense from buffer overflow
    def validate_input(self, value):
        if value is not None and len(value) > 50:
            return None
        return value


def main():
    app = MainWindow()
    app.mainloop()


main()

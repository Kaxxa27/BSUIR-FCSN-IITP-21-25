import tkinter as tk
import sqlite3
from tkinter import filedialog, messagebox
import os

from database import DATA_DIR, DB_PATH_PRODUCT

MAX_FILE_SIZE = 1024 * 1024  # Максимальный размер файла (в байтах), например, 1 МБ
# DATA_DIR = os.path.join(os.path.dirname(__file__), 'data')

class UserWindow(tk.Toplevel):
    def __init__(self, master):
        super().__init__(master)
        self.title("User Window")

        # Displaying the list of products
        self.product_label = tk.Label(self, text="Product List")
        self.product_label.pack()

        self.product_listbox = tk.Listbox(self)
        self.product_listbox.pack(fill="both", expand=True)

        # Connecting to the database and loading products
        self.load_products()

        self.select_file_button = tk.Button(self, text="Select File", command=self.select_file)
        self.select_file_button.pack()

    def load_products(self):
        conn = sqlite3.connect(DB_PATH_PRODUCT)
        c = conn.cursor()
        c.execute("SELECT * FROM products")
        products = c.fetchall()
        conn.close()

        for product in products:
            self.product_listbox.insert(tk.END, product[1])  # Assuming the product name is in the second column

    def select_file(self):
        # Получаем путь к папке data внутри проекта

        # Открываем диалоговое окно для выбора файла из папки data
        selected_file = filedialog.askopenfilename(initialdir=DATA_DIR, title="Select File",
                                                   filetypes=[("Text files", "*.txt")])
      
        if selected_file:
            # Проверяем, что выбранный файл находится в папке data
            # defense from canonization error

            selected_file = os.path.normpath(selected_file)
            data_dir = os.path.normpath(DATA_DIR)

            # print(data_dir)
            # print(os.path.dirname(selected_file))

            if os.path.dirname(selected_file).lower() != data_dir.lower():
                messagebox.showerror("Error", "Selected file is not in the data folder.")
                return

            # Проверяем размер выбранного файла
            # defense from DoS-attack
            file_size = os.path.getsize(selected_file)
            if file_size > MAX_FILE_SIZE:
                messagebox.showerror("Error", "Selected file size exceeds the maximum allowed size.")
            else:
                # Читаем содержимое файла и выводим его в messagebox
                try:
                    with open(selected_file, 'r') as file:
                        file_content = file.read()
                        messagebox.showinfo("File Content", file_content)
                except Exception as e:
                    messagebox.showerror("Error", f"Failed to read file: {e}")

import tkinter as tk
from tkinter import messagebox, simpledialog
import sqlite3

from database import DB_PATH_PRODUCT


class AdminWindow(tk.Toplevel):
    def __init__(self, master):
        super().__init__(master)
        self.title("Admin Window")

        # Отображение списка продуктов
        self.product_label = tk.Label(self, text="Product Management")
        self.product_label.pack()

        self.product_listbox = tk.Listbox(self)
        self.product_listbox.pack(fill="both", expand=True)

        # Кнопки для CRUD операций
        self.add_button = tk.Button(self, text="Add", command=self.add_product)
        self.add_button.pack()

        self.edit_button = tk.Button(self, text="Edit", command=self.edit_product)
        self.edit_button.pack()

        self.delete_button = tk.Button(self, text="Delete", command=self.delete_product)
        self.delete_button.pack()

        # Подключение к базе данных и загрузка продуктов
        self.load_products()

    def load_products(self):
        conn = sqlite3.connect(DB_PATH_PRODUCT)
        c = conn.cursor()
        c.execute("SELECT * FROM products")
        products = c.fetchall()
        conn.close()

        for product in products:
            self.product_listbox.insert(tk.END,
                                        product[1])  # Предполагается, что название продукта находится во втором столбце

    def add_product(self):
        name = self.validate_input(simpledialog.askstring("Add Product", "Enter product name:"))
        if name:
            conn = sqlite3.connect(DB_PATH_PRODUCT)
            c = conn.cursor()
            c.execute("INSERT INTO products (name) VALUES (?)", (name,))
            conn.commit()
            conn.close()
            self.product_listbox.insert(tk.END, name)

    def edit_product(self):
        selected_index = self.product_listbox.curselection()
        if selected_index:
            name = self.validate_input(simpledialog.askstring("Edit Product", "Enter new name:",
                                                              initialvalue=self.product_listbox.get(selected_index)))
            if name:
                conn = sqlite3.connect(DB_PATH_PRODUCT)
                c = conn.cursor()
                c.execute("UPDATE products SET name=? WHERE id=?",
                          (name, selected_index[0] + 1))  # Предполагается, что id начинается с 1
                conn.commit()
                conn.close()
                self.product_listbox.delete(selected_index)
                self.product_listbox.insert(selected_index, name)

    def delete_product(self):
        selected_index = self.product_listbox.curselection()
        if selected_index:
            conn = sqlite3.connect(DB_PATH_PRODUCT)
            c = conn.cursor()
            c.execute("DELETE FROM products WHERE id=?", (selected_index[0] + 1,))
            conn.commit()
            conn.close()
            self.product_listbox.delete(selected_index)

    # defense from buffer overflow
    def validate_input(self, value):
        if value is not None and len(value) > 50:
            messagebox.showerror("Error", "Input exceeds 50 characters.")
            return None
        return value

import math
import tkinter as tk

def calculate_metrics(lambda_rate, mu, m):
    p = lambda_rate / mu  # Интенсивность нагрузки

    p0 = (1 - p) / (1 - p**(m + 2))  # Вероятность простоя (система пуста)
    t0 = p0 * 60 # Время простоя
    
    # Вероятность отказа (система полностью занята)
    p_otk = p**(m + 1) * p0

    # Доля обслуживаемых заявок, поступающих в единицу времени
    Q = 1 - p_otk

    # Абсолютная пропускная способность
    A = (1 - p_otk) * lambda_rate

    # Среднее число заявок в очереди
    l_och = (p ** 2 )* (1 - (p ** m) * (m - m * p + 1)) / ((1 - p) ** 2) * p0

    # Среднее время ожидания заявки в очереди
    t_och = l_och / A

    # Среднее число обслуживаемых заявок
    l_obs = p * Q

    # Среднее число заявок в системе
    l_cmo = l_och + l_obs

    # Среднее время пребывания заявки в СМО
    t_cmo = l_cmo / A

    # Фактическая производительность СМО
    a_fact = A / mu
    
    return {
        "p": p,
        "p0": p0,
        "t0": t0,
        "p_otk": p_otk,                                                         
        "Q": Q,
        "A": A,
        "l_och": l_och,
        "t_och": t_och,
        "l_obs": l_obs,
        "l_cmo": l_cmo,
        "t_cmo": t_cmo,
        "a_fact": a_fact,
    }

# Функция для создания интерфейса
def create_interface():
    # Создание основного окна
    root = tk.Tk()
    root.title("Система массового обслуживания")

    # Заданные параметры
    lambda_rate = 4  # Интенсивность входящего потока (заявок в час)
    mu = 5  # Интенсивность обслуживания (заявок в час)

    # Расчёт для m = 3
    metrics_m3 = calculate_metrics(lambda_rate, mu, 3)

    # Расчёт для m = 4
    metrics_m4 = calculate_metrics(lambda_rate, mu, 4)

    print(metrics_m3["A"], metrics_m3["a_fact"])

    # Создание заголовков
    label_m3 = tk.Label(root, text="m=3", font=("Arial", 14))
    label_m3.grid(row=0, column=0, padx=10, pady=10)

    label_m4 = tk.Label(root, text="m=4", font=("Arial", 14))
    label_m4.grid(row=0, column=1, padx=10, pady=10)

    # Поля для вывода для m=3
    label_m3 = tk.Label(root, text=f"Интенсивность нагрузки: {metrics_m3['p']}", font=("Arial", 14))
    label_m3.grid(row=1, column=0, padx=10, pady=10)
    label_m3 = tk.Label(root, text=f"Вероятность простоя: {metrics_m3['p0']}", font=("Arial", 14))
    label_m3.grid(row=2, column=0, padx=10, pady=10)
    label_m3 = tk.Label(root, text=f"Время простоя: {metrics_m3['t0']}", font=("Arial", 14))
    label_m3.grid(row=3, column=0, padx=10, pady=10)
    label_m3 = tk.Label(root, text=f"Вероятность отказа: {metrics_m3['p_otk']}", font=("Arial", 14))
    label_m3.grid(row=4, column=0, padx=10, pady=10)
    label_m3 = tk.Label(root, text=f"Доля обслуживаемых заявок: {metrics_m3['Q']}", font=("Arial", 14))
    label_m3.grid(row=5, column=0, padx=10, pady=10)
    label_m3 = tk.Label(root, text=f"Абсолютная пропускная способность: {metrics_m3['A']}", font=("Arial", 14))
    label_m3.grid(row=6, column=0, padx=10, pady=10)
    label_m3 = tk.Label(root, text=f"Среднее время ожидания заявки в очереди: {metrics_m3['t_och']}", font=("Arial", 14))
    label_m3.grid(row=7, column=0, padx=10, pady=10)
    label_m3 = tk.Label(root, text=f"Среднее время пребывания заявки в СМО: {metrics_m3['t_cmo']}", font=("Arial", 14))
    label_m3.grid(row=8, column=0, padx=10, pady=10)
    
    label_m4 = tk.Label(root, text=f"Интенсивность нагрузки: {metrics_m4['p']}", font=("Arial", 14))
    label_m4.grid(row=1, column=1, padx=10, pady=10)
    label_m4 = tk.Label(root, text=f"Вероятность простоя: {metrics_m4['p0']}", font=("Arial", 14))
    label_m4.grid(row=2, column=1, padx=10, pady=10)
    label_m4 = tk.Label(root, text=f"Время простоя: {metrics_m4['t0']}", font=("Arial", 14))
    label_m4.grid(row=3, column=1, padx=10, pady=10)
    label_m4 = tk.Label(root, text=f"Вероятность отказа: {metrics_m4['p_otk']}", font=("Arial", 14))
    label_m4.grid(row=4, column=1, padx=10, pady=10)
    label_m4 = tk.Label(root, text=f"Доля обслуживаемых заявок: {metrics_m4['Q']}", font=("Arial", 14))
    label_m4.grid(row=5, column=1, padx=10, pady=10)
    label_m4 = tk.Label(root, text=f"Абсолютная пропускная способность: {metrics_m4['A']}", font=("Arial", 14))
    label_m4.grid(row=6, column=1, padx=10, pady=10)
    label_m4 = tk.Label(root, text=f"Среднее время ожидания заявки в очереди: {metrics_m4['t_och']}", font=("Arial", 14))
    label_m4.grid(row=7, column=1, padx=10, pady=10)
    label_m4 = tk.Label(root, text=f"Среднее время пребывания заявки в СМО: {metrics_m4['t_cmo']}", font=("Arial", 14))
    label_m4.grid(row=8, column=1, padx=10, pady=10)

    # Запуск главного цикла
    root.mainloop()

# Вызов функции для создания интерфейса
create_interface()
# Функция для расчета вероятностей состояний и пропускной способности
import tkinter as tk
import numpy as np
import matplotlib.pyplot as plt
from matplotlib.animation import FuncAnimation
import random
from scipy.linalg import lstsq

def solve_cmo():
    """
    Решение задачи для одноканальной СМО с отказами.
    
    Параметры берутся из пользовательского ввода:
    X: интенсивность потока заявок и отказов (lambda, gamma)
    t_obs: среднее время обслуживания
    t_p: среднее время ремонта
    
    Возвращает:
    p0, p1, p2: финальные вероятности состояний
    Q: относительная пропускная способность
    A: абсолютная пропускная способность
    """
    X = float(entry_X.get())  # интенсивность потока заявок
    t_obs = float(entry_Y.get())  # параметр времени обслуживания
    t_p = float(entry_R.get())  # параметр времени ремонта

    # Интенсивности переходов
    lambda_ = X                # Интенсивность потока заявок
    gamma = X                  # Интенсивность отказов
    mu = 1 / t_obs             # Интенсивность обслуживания
    r = 1 / t_p                # Интенсивность ремонта

    # Составляем матрицу системы уравнений Колмогорова
    # Коэффициенты при p0, p1, p2
    A = np.array([[-lambda_, 0, r],          # Уравнение для p0
                  [lambda_, -(mu + gamma), 0], # Уравнение для p1
                  [0, gamma, -r],           # Уравнение для p2
                  [1, 1, 1]])               # Нормировка p0 + p1 + p2 = 1

    # Правая часть системы
    b = np.array([0, 0, 0, 1])

    # Решаем систему линейных уравнений методом наименьших квадратов
    probabilities, _, _, _ = lstsq(A, b)
    p0, p1, p2 = probabilities

    # Характеристики СМО
    Q = p1                      # Относительная пропускная способность
    A = X * p1                 # Абсолютная пропускная способность

    return p0, p1, p2, Q, A

def simulate_markov_chain(X, t_obs, t_p, probabilities):
    """
    Симуляция Марковской цепи и визуализация переходов между состояниями.
    
    X: интенсивность потока заявок
    t_obs: среднее время обслуживания
    t_p: среднее время ремонта
    probabilities: вероятности состояний (p0, p1, p2)
    """
    num_steps = 100
    states = ['s0 (свободен)', 's1 (обслуживает)', 's2 (в ремонте)']
    state_history = []

    # Цвета для состояний
    state_colors = {0: 'green', 1: 'orange', 2: 'red'}

    # Для анимации создаем график
    fig, ax = plt.subplots(figsize=(10, 6))
    ax.set_xlim(0, num_steps)
    ax.set_ylim(0, 2)
    ax.set_title('Симуляция Марковской цепи и поступления заявок')
    ax.set_xlabel('Шаги времени')
    ax.set_ylabel('Состояние')
    ax.set_yticks([0, 1, 2])
    ax.set_yticklabels(states)

    def update(frame):
        nonlocal state_history

        # Генерация случайного перехода на основе вероятностей
        random_value = random.random()
        if random_value < probabilities[0]:
            state = 0  # Переход в состояние s0
        elif random_value < probabilities[0] + probabilities[1]:
            state = 1  # Переход в состояние s1
        else:
            state = 2  # Переход в состояние s2

        state_history.append(state)

        # Обновляем график
        ax.clear()
        ax.set_xlim(0, num_steps)
        ax.set_ylim(0, 2)
        ax.set_title('Симуляция Марковской цепи и поступления заявок')
        ax.set_xlabel('Шаги времени')
        ax.set_ylabel('Состояние')
        ax.set_yticks([0, 1, 2])
        ax.set_yticklabels(states)

        # Рисуем историю переходов
        ax.plot(state_history, marker='o', markersize=5, color='b', label='Переходы')
        for i in range(len(state_history)):
            ax.plot(i, state_history[i], marker='o', markersize=6, color=state_colors[state_history[i]])

        ax.legend()

    ani = FuncAnimation(fig, update, frames=num_steps, interval=200, repeat=False)
    plt.show()

def calculate():
    p0, p1, p2, Q, A = solve_cmo()

    # Отображаем результаты
    label_prob_s0.config(text=f"Вероятность s0 (свободен): {p0:.4f}")
    label_prob_s1.config(text=f"Вероятность s1 (обслуживает): {p1:.4f}")
    label_prob_s2.config(text=f"Вероятность s2 (в ремонте): {p2:.4f}")

    label_throughput_relative.config(text=f"Относительная пропускная способность: {Q:.4f}")
    label_throughput_absolute.config(text=f"Абсолютная пропускная способность: {A:.4f}")

    # Запуск симуляции
    simulate_markov_chain(float(entry_X.get()), float(entry_Y.get()), float(entry_R.get()), (p0, p1, p2))

# Создаем окно приложения
root = tk.Tk()
root.title("СМО с отказами и ремонтом")

# Создаем метки и поля для ввода значений
label_X = tk.Label(root, text="Интенсивность потока заявок (X):")
label_X.grid(row=0, column=0, padx=10, pady=5)

entry_X = tk.Entry(root)
entry_X.grid(row=0, column=1, padx=10, pady=5)

label_Y = tk.Label(root, text="Параметр времени обслуживания (Y):")
label_Y.grid(row=1, column=0, padx=10, pady=5)

entry_Y = tk.Entry(root)
entry_Y.grid(row=1, column=1, padx=10, pady=5)

label_R = tk.Label(root, text="Параметр времени ремонта (R):")
label_R.grid(row=2, column=0, padx=10, pady=5)

entry_R = tk.Entry(root)
entry_R.grid(row=2, column=1, padx=10, pady=5)

# Кнопка для вычисления
calculate_button = tk.Button(root, text="Вычислить", command=calculate)
calculate_button.grid(row=3, column=0, columnspan=2, pady=10)

# Метки для отображения результатов
label_prob_s0 = tk.Label(root, text="Вероятность s0 (свободен):")
label_prob_s0.grid(row=4, column=0, columnspan=2)

label_prob_s1 = tk.Label(root, text="Вероятность s1 (обслуживает):")
label_prob_s1.grid(row=5, column=0, columnspan=2)

label_prob_s2 = tk.Label(root, text="Вероятность s2 (в ремонте):")
label_prob_s2.grid(row=6, column=0, columnspan=2)

label_throughput_relative = tk.Label(root, text="Относительная пропускная способность:")
label_throughput_relative.grid(row=7, column=0, columnspan=2)

label_throughput_absolute = tk.Label(root, text="Абсолютная пропускная способность:")
label_throughput_absolute.grid(row=8, column=0, columnspan=2)

# Запускаем главный цикл приложения
root.mainloop()

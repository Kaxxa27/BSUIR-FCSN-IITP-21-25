# -*- coding: cp1251 -*-

import numpy as np
import matplotlib.pyplot as plt
import sympy
import math

plot_dots = 10**3
eps = 10**-3


## Задание
#m, a = 1, 0.7
#x0, y0 = 0, 0
#L, R = 0, 1


#def f(x, y): # y' = f(x,y)
#    return (a * (1 - y**2)) / ((1 + m) * x**2  +  y**2 + 1)


#def fderx(x, y):
#    numerator = 2*a*x+2*a*m*x-2*a*x*(y**2)-2*a*m*x*(y**2)
#    denominator = ((x**2)+m*(x**2)+(y**2)+1)**2
#    return -numerator/denominator

#def fdery(x, y):
#    numerator = -2*a*(x**2)*y-2*a*m*(x**2)*y-4*a*y
#    denominator = ((x**2)+m*(x**2)+(y**2)+1)**2
#    return numerator / denominator


 #Тестовый пример 1
x0, y0 = 0, 1
L, R = 0, 2


def f(x, y): # y' = -x
    return -y


def fderx(x, y):
    return 0

def fdery(x, y):
    return -1

def ans(x):
    return math.e ** (-x)

# Тестовый пример 2
#x0, y0 = 1, 1/3
#L, R = -2, 2
#
#
#def f(x, y): # y' = x^2
#    return x**2
#
#
#def fderx(x, y):
#    return 2*x
#
#def fdery(x, y):
#    return 0
#
#def ans(x):
#    return (1/3)* x**3

## Тестовый пример 3
#x0, y0 = 0, 0
#L, R = -1, 2


#def f(x, y): # y' = x+y
#    return x+y


#def fderx(x, y):
#    return 1

#def fdery(x, y):
#    return 1

#def ans(x):
#    return math.e ** x - x - 1



# Метод Эйлера
def EulerMethod(x, n):
    y_list = [y0]
    h = (x - x0) / n
    for k in range(n):
        x_k = x0 + k * h
        y_k = y_list[-1]
        y_list.append(y_k + h * f(x_k, y_k))
    return y_list

# Модифицированный метод Эйлера
def EulerModifiedMethod(x, n):
    y_list = [y0]
    h = (x - x0) / n
    for k in range(n):
        x_k = x0 + k * h
        y_k = y_list[-1]
        y_list.append(y_k + h * f(x_k + (h / 2), y_k + (h / 2) * f(x_k, y_k)))
    return y_list

# Метод Рунге-Кутта 2 порядка 
def RungeKuttaMethod2(x, n):
    y_list = [y0]
    h = (x - x0) / n
    for k in range(n):
        x_k = x0 + k * h
        y_k = y_list[-1]
        y_list.append(y_k + h * f(x_k, y_k) + ((h**2) / 2) * (fderx(x_k, y_k) + fdery(x_k, y_k) * f(x_k, y_k)))
    return y_list

# Метод Рунге-Кутта 4 порядка
def RungeKuttaMethod4(x, n):
    y_list = [y0]
    h = (x - x0) / n
    for k in range(n):
        x_k = x0 + k * h
        y_k = y_list[-1]
        K1 = h * f(x_k, y_k)
        K2 = h * f(x_k + h / 2, y_k + K1 / 2)
        K3 = h * f(x_k + h / 2, y_k + K2 / 2)
        K4 = h * f(x_k + h, y_k + K3)
        y_list.append(y_k + 1/6 * (K1 + 2 * K2 + 2 * K3 + K4))
    return y_list

# Вычислить Y по методу и X 
def CalculateY(method, x):
    n = 1 # Количество узлов от x0 до x для точности eps

    while True:
        y_list = method(x, n)
        y_list_correctly = method(x, n * 2)
        max_delta = max(abs(y_list_correctly[2 * i] - y_list[i]) for i in range(n + 1)) 

        if (max_delta < eps):
            return y_list_correctly[-1], n * 2
        else:
            n *= 2

# Создать список Y по методу и списку X
def CalculateListY(method, x_list):
    y_list = [] # Список игреков
    n_list = [] # Список n (числа узлов) для каждой точки
    for x in x_list:
        y, n = CalculateY(method, x)
        y_list.append(y)
        n_list.append(n)

    return y_list, max(n_list), sum(n_list) / len(x_list)


print(f"Количество точек для построения графика: {plot_dots}")
print(f"Точность: {eps}")                   

x_list = []
for i in range(plot_dots + 1):
    x_list.append(L + (R - L) / plot_dots * i)


print("\nN_max – максимальное количество узлов для одной из точек, \nнеобходимое для достижения заданной точности")
print("N_middle – среднее количество узлов по всем точкам при \nдостижении заданной точности")


xToCalculate = 0.9
print(f"\nТочка: {xToCalculate}")
print(f"Калькулятор: {ans(xToCalculate)}")

y_list, N_max, N_middle = CalculateListY(EulerMethod, x_list)
print("\nМетод Эйлера O(h):")
print(f"N_max: {N_max}")
print(f"N_middle: {int(N_middle)}")
print("На графике: красный")
print(CalculateY(EulerMethod, xToCalculate)[0])
print(f"Delta = {abs(CalculateY(EulerMethod, xToCalculate)[0] - ans(xToCalculate))}")
plt.plot(x_list, y_list, 'red', linewidth = 4)


y_list, N_max, N_middle = CalculateListY(EulerModifiedMethod, x_list)
print("\nМетод Эйлера (модифицированный) O(h^2):")
print(f"N_max: {N_max}")
print(f"N_middle: {int(N_middle)}")
print("На графике: голубой")
print(CalculateY(EulerModifiedMethod, xToCalculate)[0])
print(f"Delta = {abs(CalculateY(EulerModifiedMethod, xToCalculate)[0] - ans(xToCalculate))}")
plt.plot(x_list, y_list, '--b', linewidth = 3)



y_list, N_max, N_middle = CalculateListY(RungeKuttaMethod2, x_list)
print("\nМетод Рунге-Кутта 2 порядка O(h^2):")
print(f"N_max: {N_max}")
print(f"N_middle: {int(N_middle)}")
print("На графике: желтый")
print(CalculateY(RungeKuttaMethod2, xToCalculate)[0])
print(f"Delta = {abs(CalculateY(RungeKuttaMethod2, xToCalculate)[0] - ans(xToCalculate))}")
plt.plot(x_list, y_list, '-.y', linewidth = 3)



y_list, N_max, N_middle = CalculateListY(RungeKuttaMethod4, x_list)
print("\nМетод Рунге-Кутта 4 порядка O(h^4):")
print(f"N_max: {N_max}")
print(f"N_middle: {int(N_middle)}")
print("На графике: черный")
print(CalculateY(RungeKuttaMethod4, xToCalculate)[0])
print(f"Delta: {abs(CalculateY(RungeKuttaMethod4, xToCalculate)[0] - ans(xToCalculate))}")
plt.plot(x_list, y_list, ':k', linewidth = 3)




plt.show()



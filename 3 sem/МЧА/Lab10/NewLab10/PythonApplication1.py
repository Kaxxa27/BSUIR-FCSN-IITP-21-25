# -*- coding: cp1251 -*-

from cmath import sqrt
import numpy as np
import matplotlib.pyplot as plt
import sympy
import math

from calculate import CalculateListY
from calculate import CalculateY


plot_dots = 10**3
eps = 10**-3


# Тестовый пример 1

#x0, y0 = 1, 2
#L, R = 1, 2

#def f(x, y):
#    return 2 * (y/x + x**3)

#def p(x):
#    return -2/x

#def q(x):
#    return 2 * x**3

#def ans(x): 
#    return round(x**4 + x**2, 6)

# Тестовый пример 2
 #y'= x + y
 #y = 2*e^x - x - 1

x0, y0 = 0 ,1
L, R = 0, 6

def f(x, y):
    return y

def p(x):
    return -1

def q(x):
    return 0

def ans(x): 
    return round( math.exp(1), 6)



## Тестовый пример 3
## y'= (2/x)*y + 3/(x^2)
## y = -1/x
#x0, y0 = 1, -1
#L, R = 1, 6
#
#def f(x, y):
#    return (2/x) * y + 3 / (x**2)
#
#def p(x):
#    return -2/x
#
#def q(x):
#    return 3 / (x**2)
#
#def ans(x): 
#    return round(-1/x, 6)



# Задание
#m, a = 1, 0.7

#x0, y0 = 0, 0
#L, R = 0, 1


#def f(x, y): # y' = f(x,y)
#    return (a * (1 - y**2)) / ((1 + m) * x**2  +  y**2 + 1)



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

# Неявный метод Адамса 2 порядка
def AdamsImplicit2(x, n):
    y_list = [y0]
    h = (x - x0) / n
    for k in range(n):
        y_k = y_list[-1] 
        p_k = p(x0 + k * h)
        p_k_plus1 = p(x0 + (k + 1) * h) 
        q_k = q(x0 + k * h)
        q_k_plus1 = q(x0 + (k + 1) * h)
        y_list.append(((2 - h * p_k) * y_k + h * (q_k + q_k_plus1)) / (2 + h * p_k_plus1))
    return y_list


# Явный метод Адамса 2 порядка
def AdamsExplicit2(x, n):
    h = (x - x0) / n
    y_list = [y0, RungeKuttaMethod4(x0 + h, n)[-1]]
    for k in range (1, n):
        x_k_minus1 = x0 + (k - 1) * h
        x_k = x0 + k * h
        y_k_minus1 = y_list[-2]
        y_k = y_list[-1]
        y_list.append(y_k + h * ((3/2) * f(x_k,y_k) - (1/2) * f(x_k_minus1, y_k_minus1)))
    return y_list


# Явный метод Адамса 3 порядка
def AdamsExplicit3(x, n):
    h = (x - x0) / n
    #y_list = [y0, CalculateY(RungeKuttaMethod4, x0 + h, eps)[0], CalculateY(RungeKuttaMethod4, x0 + 2 * h, eps)[0]]
    y_list = [y0, RungeKuttaMethod4(x0 + h, n)[-1], RungeKuttaMethod4(x0 + 2 * h, n)[-1]]
    for k in range (2, n):
        x_k_minus2 = x0 + (k - 2) * h
        x_k_minus1 = x0 + (k - 1) * h
        x_k = x0 + k * h
        y_k_minus2 = y_list[-3]
        y_k_minus1 = y_list[-2]
        y_k = y_list[-1]
        y_list.append(y_k + h*((23/12)*f(x_k,y_k)-(4/3)*f(x_k_minus1,y_k_minus1)+(5/12)*f(x_k_minus2,y_k_minus2)))
    return y_list

# Явный метод Адамса 4 порядка
def AdamsExplicit4(x, n):
    h = (x - x0) / n
    y_list = [y0, RungeKuttaMethod4(x0 + h, n)[-1], RungeKuttaMethod4(x0 + 2 * h, n)[-1], RungeKuttaMethod4(x0 + 3 * h, n)[-1]]
    for k in range (3, n):
        x_k_minus3 = x0 + (k - 3) * h
        x_k_minus2 = x0 + (k - 2) * h
        x_k_minus1 = x0 + (k - 1) * h
        x_k = x0 + k * h
        y_k_minus3 = y_list[-4]
        y_k_minus2 = y_list[-3]
        y_k_minus1 = y_list[-2]
        y_k = y_list[-1]
        
        y_k_plus1 = y_k + h * ((55/24)*f(x_k,y_k)-(59/24)*f(x_k_minus1,y_k_minus1)+(37/24)*f(x_k_minus2,y_k_minus2)-(3/8)*f(x_k_minus3,y_k_minus3))
        y_list.append(y_k_plus1)
    return y_list


x_list = []
for i in range(plot_dots + 1):
    x_list.append(L + (R - L) / plot_dots * i)
xToCalculate = 1.00000001


print(f"Количество точек для построения графика: {plot_dots}")
print(f"Точность: {eps:.0e}")                   
print("\nN_max – максимальное количество узлов для одной из точек, \nнеобходимое для достижения заданной точности")
print("N_middle – среднее количество узлов по всем точкам при \nдостижении заданной точности")

print(f"\nТочка: {xToCalculate}")
print(f"Калькулятор: {ans(xToCalculate)}")

y_list, N_max, N_middle = CalculateListY(AdamsImplicit2, x_list, eps)
print("\nНеявный метод Адамса 2 порядка O(h^2):")
print(f"N_max: {N_max}")
print(f"N_middle: {int(N_middle)}")
print("На графике: красный")
print(f"В точке: {CalculateY(AdamsImplicit2, xToCalculate, eps)[0]} [{CalculateY(AdamsImplicit2, xToCalculate, eps)[1]}]")
print(f"Delta = {abs(CalculateY(AdamsImplicit2, xToCalculate, eps)[0] - ans(xToCalculate)):.2e}")
plt.plot(x_list, y_list, 'red', linewidth = 4)

y_list, N_max, N_middle = CalculateListY(AdamsExplicit2, x_list, eps)
print("\nЯвный метод Адамса 2 порядка O(h^2):")
print(f"N_max: {N_max}")
print(f"N_middle: {int(N_middle)}")
print("На графике: синий")
print(f"В точке: {CalculateY(AdamsExplicit2, xToCalculate, eps)[0]} [{CalculateY(AdamsExplicit2, xToCalculate, eps)[1]}]")
print(f"Delta = {abs(CalculateY(AdamsExplicit2, xToCalculate, eps)[0] - ans(xToCalculate)):.2e}")
plt.plot(x_list, y_list, 'b', linewidth = 3)

y_list, N_max, N_middle = CalculateListY(AdamsExplicit3, x_list, eps)
print("\nЯвный метод Адамса 3 порядка O(h^3):")
print(f"N_max: {N_max}")
print(f"N_middle: {int(N_middle)}")
print("На графике: желтый")
print(f"В точке: {CalculateY(AdamsExplicit3, xToCalculate, eps)[0]} [{CalculateY(AdamsExplicit3, xToCalculate, eps)[1]}]")
print(f"Delta = {abs(CalculateY(AdamsExplicit3, xToCalculate, eps)[0] - ans(xToCalculate)):.2e}")
plt.plot(x_list, y_list, '-.y', linewidth = 3)

y_list2, N_max, N_middle = CalculateListY(AdamsExplicit4, x_list, eps)
print("\nЯвный метод Адамса 4 порядка O(h^4):")
print(f"N_max: {N_max}")
print(f"N_middle: {int(N_middle)}")
print("На графике: черный")
print(f"В точке: {CalculateY(AdamsExplicit4, xToCalculate, eps)[0]} [{CalculateY(AdamsExplicit4, xToCalculate, eps)[1]}]")
print(f"Delta = {abs(CalculateY(AdamsExplicit4, xToCalculate, eps)[0] - ans(xToCalculate)):.2e}")
plt.plot(x_list, y_list, ':k', linewidth = 3)



plt.show()



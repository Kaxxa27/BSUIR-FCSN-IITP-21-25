# -*- coding: cp1251 -*-

import numpy as np
import matplotlib.pyplot as plt
from scipy.optimize import curve_fit
p = [0.0, 0.41, 0.79, 1.13, 1.46, 1.76, 2.04, 2.3, 2.55, 2.79, 3.01]
m_variants = [0.0, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 1.8, 2.53, 3.96,
5.33, 1.96]
k = 7
m = m_variants[k - 1]
x = [0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0]
y = [p[i] + ((-1) ** k) * m for i in range(len(p))]
x1 = np.array(x)
y1 = np.array(y)

def fin_difference(x, y, nodes_number):
    difference = list(range(nodes_number))
    difference[0] = y
    for i in range(1, nodes_number):
        r = list(range(nodes_number - i))
        for j in range(nodes_number - i):
            r[j] = (difference[i - 1][j + 1] - difference[i - 1][j]) / (x[j +i] - x[j])
        difference[i] = r
    return difference

def lagrange(x, y, t):
    result = 0
    for i in range(len(y)):
        poly = y[i]
        for j in range(len(x)):
            if i == j:
                continue
            poly *= (t - x[j]) / (x[i] - x[j])
        result += poly
    return result

def newton(x, y, t):
    difference = fin_difference(x, y, len(x))
    result = 0
    for i in range(len(y)):
        poly = difference[i][0]
        for j in range(i):
            poly *= (t - x[j])
        result += poly
    return result

def sq_polynom(x1, a, b, c):
    return a * x1 ** 2 + b * x1 + c

def main():
    global x
    global y

print("x =", x)
print("y =", y)
x = np.array(x, dtype=float)
y = np.array(y, dtype=float)
x_new = np.linspace(0, 1)
print('\nРезультат в точке 0.47 по полиному Лагранжа:\n',
lagrange(x, y, 0.47))
print('\nРезультат в точке 0.47 по полиному Ньютона:\n',
newton(x, y, 0.47))
y_new = [lagrange(x, y, i) for i in x_new]
plt.plot(x, y, 'o', x_new, y_new)
plt.title(r'$Лагранж$')
plt.grid(True)
plt.show()
y_new = [newton(x, y, i) for i in x_new]
plt.plot(x, y, 'o', x_new, y_new)
plt.title(r'$Ньютон$')
plt.grid(True)
plt.show()
popt, pcov = curve_fit(sq_polynom, x1, y1, (1e3, 1e-2, -1e1), maxfev=10
** 6)
print('\nКвадратный многочлен наилучшего приближения:')
print("A*x**2 + B*x + C = 0")
print('A = {0}\nB = {1}\nC = {2}'.format(*tuple(popt)))
A = popt[0]
B = popt[1]
C = popt[2]
plt.scatter(x1, y1, s=30, color='orange')
plt.plot(x1, sq_polynom(x1, *popt))
plt.title(r'$КвадратныйМногочлен$')
plt.grid(True)
plt.show()
print("Результат в точке", 0.47)
print(A * 0.47 ** 2 + B * 0.47 + C)



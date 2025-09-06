# -*- coding: cp1251 -*-

import math
import numpy as np
import matplotlib.pyplot as plt



def input():

    #Пример 1
    #def f(x): 
    #    return 2*x + 5
    #a, b = -2, 5
    #nodes = 4
    #xdot = 3
    
    #Пример 2
    #def f(x): 
    #    return np.cos(x)
    #a, b = -4, 4
    #nodes = 7
    #xdot = 3.2
    
    #Пример 3
    #def f(x): 
    #    return np.exp(x)
    #a, b = -3, 3
    #nodes = 4
    #xdot = 2.2
    
    #Пример 4
    #def f(x): 
    #    return x**3+x**2+x
    #a, b = -2, 5
    #nodes = 5
    #xdot = -1.2

    #Задание 
    def f(x): 
        return math.cosh(x)
    a, b = 0, 2
    nodes = 6
    xdot = 0.5*(b-a)

    dots = [] # Список пар (узел, значение в узле)
    for i in range(nodes):
        x = a + (b - a) * i / (nodes - 1)
        y = f(x)
        dots += [(x, y)]
    
    return dots, f, xdot




def tridiag_solve(A, b):
   
    A = A.copy()
    b = b.copy()
    n = len(A)

    A[0][1] /= A[0][0]
    for i in range(1, n-1):
        A[i][i+1] /= (A[i][i] - A[i][i-1] * A[i-1][i])

    b[0] /= A[0][0]
    for i in range(1, n):
        b[i] = (b[i] - A[i][i-1] * b[i-1]) / (A[i][i] - A[i][i-1] * A[i-1][i])

    x = np.zeros(n)
 
    x[-1] = b[-1]
    for i in range(n-2, -1, -1):
        x[i] = b[i] - A[i][i+1] * x[i+1]


    return x


def Spline(dots):

    n = len(dots) - 1
    (x, y) = map(list, zip(*dots))
    
    h = [None]
    for i in range(1,n+1):
        h += [ x[i] - x[i-1] ]
    
    A = [[None] * (n) for i in range(n)]
    for i in range(1,n):
        for j in range(1,n):
            A[i][j] = 0.0
    for i in range(1,n-1):
        A[i+1][i] = h[i+1]
    for i in range(1,n):
        A[i][i] = 2 * (h[i] + h[i+1])
    for i in range(1,n-1):
        A[i][i+1] = h[i+1]
    
    F = []
    for i in range(1,n):
        F += [ 3 * ( (y[i+1] - y[i]) / h[i+1] - (y[i] - y[i-1]) / h[i]) ]

    A = [A[i][1:] for i in range(len(A)) if i]
    
    c = tridiag_solve(A, F)
    c = [0.0] + list(c) + [0.0]
    
    def evaluate(xdot):
        for i in range(1, len(x)):
            if x[i-1] <= xdot <= x[i]:
                val = 0
                val += y[i]
                b = (y[i] - y[i-1]) / h[i] + (2 * c[i] + c[i-1]) * h[i] / 3
                val += b * (xdot - x[i])
                val += c[i] * ((xdot - x[i]) ** 2)
                d = (c[i] - c[i-1]) / (3 * h[i])
                val += d * ((xdot - x[i]) ** 3)
                return val
        return None
    
    def cout():
        print("Кубический сплайн:", '\n')
        for i in range(1, len(x)):
            val = 0
            b = (y[i] - y[i-1]) / h[i] + (2 * c[i] + c[i-1]) * h[i] / 3
            d = (c[i] - c[i-1]) / (3 * h[i])
            print(x[i-1], x[i], "->")
            print(np.poly1d([d, c[i], b, y[i]]), '\n')
    
    return evaluate, cout
    
dots, f, _ = input()

(x, y) = map(list, zip(*dots)) # Список x и y отдельно

print("(x,y) =", dots, '\n')

plotdots = 10**4

plt.plot(x, y, 'og') # Зеленые точки (x, y)
xplot = np.linspace(min(x), max(x), plotdots) # Очень много иксов

yplot = [f(xdot) for xdot in xplot] # Игреки от исходной функции
plt.plot(xplot, yplot, 'red') # Построение исходной функции

spl, cout = Spline(dots)
yplot = [spl(xdot) for xdot in xplot] # Игреки от сплайна
plt.plot(xplot, yplot, 'blue') # Построение сплайна
cout()


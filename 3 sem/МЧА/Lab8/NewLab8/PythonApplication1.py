# -*- coding: cp1251 -*-

import sympy
import math
import numpy


xSymbol = sympy.symbols('x')
func = sympy.log(xSymbol, 10)
L, R = 1, 3

func = sympy.exp(xSymbol)
L, R = 0, 2
#
#func = sympy.cos(xSymbol)
#L, R = -4, 0
#
#func = sympy.sqrt(xSymbol)
#L, R = 5, 9
#
#func = sympy.tan(xSymbol)
#L, R = 2, 4

xDot = (L + R) / 2


derFormat = "{:.7f}"  # Формат вывода чисел в производных
intFormat = "{:.7f}"  # Формат вывода чисел в интегралах
derTextWidth = 40     # Ширина вывода текста в производных
intTextWidth = 45     # Ширина вывода текста в интегралах

derEps = 0.01     # Точность в производных
intEps = 0.001     # Точность в интегралах

def f(x):
    return func.subs(xSymbol, x)

def fd(x, n):
    return (sympy.diff(func, xSymbol, n)).subs(xSymbol, x)


def DerFirst1(x):
    fd2 = sympy.diff(func, xSymbol, 2); # Производная второго порядка
    M2 = abs(sympy.calculus.util.maximum(fd2, xSymbol, sympy.Interval(L, R))) # M2
    h = abs(2 * derEps / M2) / 3 # Шаг

    return (f(x + h) - f(x)) / h

def DerFirst2(x):
    fd3 = sympy.diff(func, xSymbol, 3); # Производная третьего порядка
    M3 = abs(sympy.calculus.util.maximum(fd3, xSymbol, sympy.Interval(L, R))) # M3
    h = math.sqrt((6 * derEps) / M3) / 3

    return (f(x + h) - f(x - h)) / (2 * h)

def DerSecond(x):
    fd4 = sympy.diff(func, xSymbol, 4); # Производная четвертого порядка
    M4 = abs(sympy.calculus.util.maximum(fd4, xSymbol, sympy.Interval(L, R))) # M4
    h = math.sqrt((12 * derEps) / M4) / 3

    return (f(x + h) - 2 * f(x) + f(x - h)) / (h ** 2)

def IntCalculator():
    return sympy.integrate(func, (xSymbol, L, R))

def IntMiddleRectangles():
    fd2 = sympy.diff(func, xSymbol, 2); # Производная второго порядка
    M2 = abs(sympy.calculus.util.maximum(fd2, xSymbol, sympy.Interval(L, R))) # M2

    h = math.sqrt(24 * intEps / (R - L) / M2) / 2
    
    n = numpy.ceil((R - L) / h)
    
    h = (R - L) / n
    x = L + h / 2
    s = 0.0
    while x < R:
        s += f(x) * h
        x += h
    return s



def IntLeftRectangles():
    fd1 = sympy.diff(func, xSymbol, 1); # Производная первого порядка
    M1 = abs(sympy.calculus.util.maximum(fd1, xSymbol, sympy.Interval(L, R))) # M1

    h = math.sqrt((2*intEps)/(M1)) / 100
    n = numpy.ceil((R - L) / h)
    

    S = 0.0

    for k in range(0, int(n - 1)):
        S += f(L + k * h)

    S *= h
   
    return S

def IntRightRectangles():
    fd1= sympy.diff(func, xSymbol, 1); # Производная первого порядка
    M1 = abs(sympy.calculus.util.maximum(fd1, xSymbol, sympy.Interval(L, R))) # M1

    h = math.sqrt((2*intEps)/(M1)) / 100
    n = numpy.ceil((R - L) / h)
    
    S = 0.0
    
    for k in range(1, int(n)):
        if (L + k * h < R):
            S += f(L + k * h)

    S *= h
   
    return S

def IntTrapezoid():
    fd2 = sympy.diff(func, xSymbol, 2); # Производная второго порядка
    M2 = abs(sympy.calculus.util.maximum(fd2, xSymbol, sympy.Interval(L, R))) # M2

    h = math.sqrt((12 * intEps) / (M2)) / 5
    n = numpy.ceil((R - L) / h)

    h = (R - L) / n
    x = L + h / 2
    s = 0.0
    while x < R:
        s += ((f(x - h / 2) + f(x + h / 2)) / 2) * h
        x += h
    return s
  

def IntSimpson():
    fd4 = sympy.diff(func, xSymbol, 4); # Производная четвертого порядка
    M4 = abs(sympy.calculus.util.maximum(fd4, xSymbol, sympy.Interval(L, R))) # M4

    n = ((M4 * (R - L)**5 / 2880 / (intEps)) ** (1. / 4.)) * 2
    h = (R - L) / (2 * n)
    n = numpy.ceil(n)

    h = (R - L) / n
    x = L + h / 2
    s = 0.0
    while x < R:
        fa = f(x - h / 2)
        fm = f(x) 
        fb = f(x + h / 2)        
        s += (fa + 4 * fm  + fb) * h / 6
        x += h
    return s
   


    

print(f"Функция: y = {func}")
print(f"Интервал: [{L}, {R}]")
print(f"xDot = {xDot}")

print("\n")

print("Первая производная (калькулятор):".ljust(derTextWidth), derFormat.format(fd(xDot, 1)))
print("Первая производная (метод #1: O(h)): ".ljust(derTextWidth), derFormat.format(DerFirst1(xDot)))
print("delta =", intFormat.format(abs(fd(xDot, 1) - DerFirst1(xDot))))
print("Первая производная (метод #2: O(h^2)): ".ljust(derTextWidth), derFormat.format(DerFirst2(xDot)))
print("delta =", intFormat.format(abs(fd(xDot, 1) - DerFirst2(xDot))))

print("\n")

print("Вторая производная (калькулятор):".ljust(derTextWidth), derFormat.format(fd(xDot, 2)))
print("Вторая производная (O(h^2)): ".ljust(derTextWidth), derFormat.format(DerSecond(xDot)))
print("delta =", intFormat.format(abs(fd(xDot, 2) - DerSecond(xDot))))

print("\n")

print("Интеграл (калькулятор): ".ljust(intTextWidth), intFormat.format(IntCalculator()))
print("Интеграл (средних прямоугольников: O(h^2)): ".ljust(intTextWidth), intFormat.format(IntMiddleRectangles()))
print("delta =", intFormat.format(abs(IntCalculator() - IntMiddleRectangles())))
print("Интеграл (левых прямоугольников: O(h^2)): ".ljust(intTextWidth), intFormat.format(IntLeftRectangles()))
print("delta =", intFormat.format(abs(IntCalculator() - IntLeftRectangles())))
print("Интеграл (правых прямоугольников: O(h^2)): ".ljust(intTextWidth), intFormat.format(IntRightRectangles()))
print("delta =", intFormat.format(abs(IntCalculator() - IntRightRectangles())))
print("Интеграл (трапеций: O(h^2)): ".ljust(intTextWidth), intFormat.format(IntTrapezoid()))
print("delta =", intFormat.format(abs(IntCalculator() - IntTrapezoid())))
print("Интеграл (Симпсон: O(h^4)): ".ljust(intTextWidth), intFormat.format(IntSimpson()))
print("delta =", intFormat.format(abs(IntCalculator() - IntSimpson())))

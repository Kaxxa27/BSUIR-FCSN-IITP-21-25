from sympy import symbols

interval = (-10, 10)

a = 9.57496
b = -243.672
c = 773.65

x, y = symbols('x y')
y = x**3 + a * x**2 + b*x + c

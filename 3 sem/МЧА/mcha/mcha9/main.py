from EulerMethod import euler, better_euler
from RoungeKut import runge_kut
from tools import create_y_dots
import matplotlib.pyplot as plt
import numpy as np
import math
print("Euler method and Runge-Kutta method \n")
plotdots = 10 ** 3
eps = 10 ** -3

# def yder(x, y): return -y
# def y_sol(x): return math.exp(-x)
# y0 = 1
# LEFT, RIGHT = 0, 1
# def ans(x): return np.exp(-x)
# xplot = np.linspace(LEFT, RIGHT, plotdots)
# yplot = [ans(xdot) for xdot in xplot]
# plt.plot(xplot, yplot, 'black')


# def yder(x, y): return x**2
# def y_sol(x): return (x**3)/3
# y0 = 0
# LEFT, RIGHT = 0, 3


# def yder(x, y): return x + y
# y0 = 0
# LEFT, RIGHT = -1, 1
# def ans(x): return -x-1+np.exp(x)
# xplot = np.linspace(LEFT, RIGHT, plotdots)
# yplot = [ans(xdot) for xdot in xplot]
# plt.plot(xplot, yplot, 'black')


m, a = 1.5, 1.3


def yder(x, y): return (a * (1 - y ** 2)) / ((1 + m) * x ** 2 + y ** 2 + 1)
def y_sol(x): return 0

y0 = 0
LEFT, RIGHT = 0, 1

print("Dots for calculating = ", plotdots)
print("Epsilon = ", eps)

x_dots = [LEFT + (RIGHT - LEFT) / plotdots * i for i in range(plotdots + 1)]

ydots, midn, maxn = create_y_dots(euler, x_dots, y0, yder, eps)
for i in range(1, 10, 4):
    print(f"x[{i}] = {x_dots[i*100]}\n"
          f"y[{i}] = {ydots[i*100]}\n"
          f"y meaning: {y_sol(x_dots[i*100])}\n"
          f"diff = {abs(ydots[i*100]-y_sol(x_dots[i*100]))}")
print("\n MidN / MaxN per dot in " + "Euler" + " method       = ", midn, " / ", maxn)
plt.plot(x_dots, ydots, 'y')

ydots, midn, maxn = create_y_dots(better_euler, x_dots, y0, yder, eps)
for i in range(1, 10, 4):
    print(f"x[{i}] = {x_dots[i*100]}\n"
          f"y[{i}] = {ydots[i*100]}\n"
          f"y meaning: {y_sol(x_dots[i*100])}\n"
          f"diff = {abs(ydots[i*100]-y_sol(x_dots[i*100]))}")
print("\n MidN / MaxN per dot in " + "BetterEuler" + " method = ", midn, " / ", maxn)
plt.plot(x_dots, ydots, 'b--')

ydots, midn, maxn = create_y_dots(runge_kut, x_dots, y0, yder, eps)
for i in range(1, 10, 4):
    print(f"x[{i}] = {x_dots[i*100]}\n"
          f"y[{i}] = {ydots[i*100]}\n"
          f"y meaning: {y_sol(x_dots[i*100])}\n"
          f"diff = {abs(ydots[i*100]-y_sol(x_dots[i*100]))}")
print("\n MidN / MaxN per dot in " + "RungeKutta" + " method  = ", midn, " / ", maxn)
plt.plot(x_dots, ydots, 'r:')

plt.show()

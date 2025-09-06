import numpy as np
from sympy import *


def lagrange_l(x_ls: list, x_j):
    x = symbols('x')
    lg = 1
    n = len(x_ls)
    for i in range(n):
        if x_j == x_ls[i]:
            continue
        lg *= (x - x_ls[i]) / (x_j - x_ls[i])
    return lg


def lagrange_poly(x, y):
    if len(x) != len(y):
        raise ValueError("x and y are not same length")
    n = len(x)

    L = 0
    for i in range(n):
        L += y[i] * lagrange_l(x, x[i])
    return L

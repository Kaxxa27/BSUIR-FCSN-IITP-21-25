import numpy as np
import math


def int_middle_rectangle(f, l, r, n):
    h = (r - l) / n
    x = l + h / 2
    s = 0.0
    while x < r:
        s += f(x) * h
        x += h
    return s


def integral_trapezoid(f, l, r, n):
    h = (r - l) / n
    x = l + h / 2
    s = 0.0
    while x < r:
        s += ((f(x - h / 2) + f(x + h / 2)) / 2) * h
        x += h
    return s


def integral_simpson(f, l, r, n):
    h = (r - l) / n
    x = l + h / 2
    s = 0.0
    while x < r:
        fa = f(x - h / 2)
        fm = f(x)
        fb = f(x + h / 2)
        s += (fa + 4 * fm + fb) * h / 6
        x += h
    return s


def integral_rand_segments(method, f, l, r, epsilon=0.000001):
    left_coeff, right_coeff = 1 / 3, 1 / 2
    h_prev = r - l
    ans_prev = method(f, l, r, 1)
    while True:
        h_new = h_prev * (left_coeff + (right_coeff - left_coeff) * np.random.rand())
        n = math.floor((r - l) / h_new)
        m = l + h_new * n
        ans_new = method(f, l, m, n) + method(f, m, r, 1)
        if abs(ans_new - ans_prev) < epsilon:
            print("\nRandom intervals: N =", n)
            return ans_new
        ans_prev = ans_new
        h_prev = h_new


def integral_middle_rectangle_via_estimation(f, l, r, epsinon=0.000001, m2deLR=0):
    if m2deLR > 0.0:
        M2 = m2deLR
        h = (24 * epsinon / (r - l) / M2) ** (1 / 2)
        n = np.ceil((r - l) / h)
        return int_middle_rectangle(f, l, r, n)
    else:
        return integral_rand_segments(int_middle_rectangle, f, l, r)


def integral_trapezoid_via_estimation(f, l, r, epsinon=0.000001, m2deLR=0):
    if m2deLR > 0.0:
        M2 = m2deLR
        h = (12 * epsinon / (r - l) / M2) ** (1 / 2)
        n = np.ceil((r - l) / h)
        return integral_trapezoid(f, l, r, n)
    else:
        return integral_rand_segments(integral_trapezoid, f, l, r)


def integral_simpson_via_estimation(f, l, r, epsinon=0.000001, m4deLR=0):
    if m4deLR > 0.0:
        M4 = m4deLR
        h = (180 * epsinon / (r - l) / M4) ** (1 / 4)
        n = np.ceil((r - l) / h)
        return integral_simpson(f, l, r, n)
    else:
        return integral_rand_segments(integral_simpson, f, l, r)

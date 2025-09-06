import numpy
from scipy import integrate
from test import g, f


def pick_step(func, a, b, step):  # Функция pick_step возвращает массив точек t и точных значений в этих точках y(t):
    T = numpy.arange(a, b, step)
    # Y = numpy.copy(T)
    # for i in range(0, len(T)):
    #     Y[i] = func(T[i])
    Y = func(T)
    return (T, Y)


def RK45(f, T, Y0):
    Y = numpy.zeros((len(T), len(Y0)))
    Y[0] = Y0
    h = T[1] - T[0]
    for it in range(1, len(T)):
        k1 = f(T[it - 1], Y[it - 1])
        k2 = f(T[it - 1] + 0.5 * h, Y[it - 1] + 0.5 * h * k1)
        k3 = f(T[it - 1] + 0.5 * h, Y[it - 1] + 0.5 * h * k2)
        k4 = f(T[it - 1] + h, Y[it - 1] + h * k3)
        Y[it] = Y[it - 1] + (h / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4)
    return Y[:, 0]


def adams3(f, T, Y0):
    Y = numpy.zeros((len(T) + 2, len(Y0)))
    Y2 = numpy.zeros((len(T), len(Y0)))
    h = T[1] - T[0]
    Y987 = RK45(f, [T[0], T[0] - h, T[0] - 2 * h], Y0)
    Y[0] = Y987[2]
    Y[1] = Y987[1]
    Y[2] = Y0

    for it in range(3, len(T) + 2):
        Y[it] = Y[it - 1] + (h / 12.0) * (
                23 * f(T[it - 3] - h, Y[it - 1]) - 16 * f(T[it - 3] - 2 * h, Y[it - 2]) + 5 * f(T[it - 3] - 3 * h,
                                                                                                Y[it - 3]))

    for it in range(0, len(T)):
        Y2[it] = Y[it + 2]

    return Y2[:, 0]


def evaluate(a, b, h, Y0, eps):
    T, Y_exact = pick_step(g, a, b + h, step=h)
    Y_ADAMS3 = adams3(f, T, Y0)
    error_local_ADAMS3 = numpy.abs(Y_ADAMS3 - Y_exact)
    while (max(error_local_ADAMS3) > eps):
        h /= 2
        T, Y_exact = pick_step(g, a, b + h, step=h)
        Y_ADAMS3 = adams3(f, T, Y0)
        error_local_ADAMS3 = numpy.abs(Y_ADAMS3 - Y_exact)

    return h, T, Y_ADAMS3, Y_exact, error_local_ADAMS3

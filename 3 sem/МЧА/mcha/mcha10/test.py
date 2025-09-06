import math
import numpy
from scipy import integrate
eps = 0.001
a = 0
b = 1
Y0 = numpy.array([0])


def f(t, Y):  # Функция f (t, y(t)). В массиве Y хранятся начальные условия  и ,
    # в массиве dY - значения   на нулевой и первой позиции соответственно.
    dY = numpy.zeros(Y.shape)
    dY[0] = c*(1-Y[0]**2)/((1+m)*t**2+Y[0]**2+1)
    return dY


def g(T):  # Функция, вычисляющая точное решение систем
    return rkf45(f,T, Y0)


def rkf45(f, T, Y0):
    r = (integrate.ode(f).set_integrator('dopri5', atol=0.0001).set_initial_value(Y0, T[0]))
    Y = numpy.zeros((len(T), len(Y0)))
    Y[0] = Y0
    for it in range(1, len(T)):
        Y[it] = r.integrate(T[it])
        if not r.successful():
            raise RuntimeError("Нельзя интегрировать")
    return Y[:, 0]


c = 1.3
m = 1.5


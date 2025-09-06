from sympy import symbols, sqrt, tan
import numpy as np

k = 4
m = 0.2
a = 0.8

approx = (0.6, 0.6)
approx_2 = (0.05, -0.7)

x = symbols('x:2')
y_1 = tan(x[0]*x[1] + m) - x[0]
y_2 = a*x[0]**2 + 2*x[1]**2 - 1


def get_system():
    return np.array([
        [
            y_1,
            y_2
        ],
        [
            tan(x[0]*x[1] + m),
            sqrt((1 - a*x[0]**2) / 2)
        ]
    ])

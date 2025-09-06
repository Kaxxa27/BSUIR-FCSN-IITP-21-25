from sympy import symbols, log, sqrt
import numpy as np

x = symbols('x:2')

y_1_1 = 2*x[0]**2 - x[0]*x[1] - 5*x[0] + 1
y_1_2 = x[0] + 3*log(x[0], 10) - x[1]**2
y_1_approx = (3.5, 2.2)


def get_system_1_it():
    global y_1_1, y_1_2
    return np.array([
        [
            y_1_1,
            y_1_2
        ],
        [
            sqrt((x[0] * x[1] + 5*x[0] - 1) / 2),
            sqrt(x[0]+3*log(x[0], 10))
        ]
        ])


y_2_1 = x[0]**2 + x[0]*x[1] - 10
y_2_2 = x[1] + 3*x[0]*x[1]**2 - 57
y_2_approx = (1.5, 3.5)


def get_system_2_it():
    global y_2_1, y_2_2
    return np.array([
        [
            y_2_1,
            y_2_2
        ],
        [
            sqrt(10 - x[0]*x[1]),
            sqrt((57-x[1]) / (3*x[0]))
        ]
    ])


y_3_1 = x[0]**2 - x[1] - 1
y_3_2 = x[0] - x[1]**2 + 1
y_3_approx = (1.9, 1.8)


def get_system_3_it():
    global y_3_1, y_3_2
    return np.array([
        [
            y_3_1,
            y_3_2
        ],
        [
            sqrt(1+x[1]),
            sqrt(1 + x[0])
        ]
    ])


y_4_1 = x[0] + x[0]*x[1] - 4
y_4_2 = x[0] + x[1] - 3
y_4 = np.array([y_4_1, y_4_2])
y_4_approx = (1.9, 0.6)


y_5_1 = x[0]**2 + x[0]*x[1] + x[1]**2 - 7
y_5_2 = x[0]**3 + x[1]**3 - 9
y_5 = np.array([y_5_1, y_5_2])
y_5_approx = (1.5, 0.9)
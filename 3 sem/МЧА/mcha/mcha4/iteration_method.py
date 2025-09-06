from sympy import symbols
import numpy as np
from staff_matrix import get_jacobi, roots_to_dict
import random


def get_q(fi_equation, approx, e=0.1):
    x_0 = approx[0]
    x_fi = approx[1]
    x = symbols('x:2')
    e = 0.1
    x_1 = random.uniform(x_0 - e, x_0 + e)
    x_2 = random.uniform(x_0 - e, x_0 + e)

    q = (abs(fi_equation.subs({x[0]: x_1, x[1]: x_fi}) - fi_equation.subs({x[0]: x_2, x[1]: x_fi}))) / (abs(x_1 - x_2))
    return q


def iteration_solve(system_equations: np.array, approx, tol=0.00001, verbose=0):
    if verbose == 1:
        print('\niteration method computing...')

    n = system_equations.shape[0]
    x = symbols(f'x:{n}')

    fi_equations = system_equations[1]

    prev_roots = np.zeros(shape=(n, ))
    curr_roots = list(approx)

    errors = np.zeros(shape=(n, ))
    error = tol * 10000

    J = get_jacobi(system_equations[0])
    jacobi_values = np.zeros(shape=(n, n))

    roots_d = roots_to_dict(curr_roots, x)

    for i in range(n):
        for j in range(n):
            jacobi_values[i, j] = J[i, j].subs(roots_d)

    # compute q
    if verbose == 1:
        q_1 = float(get_q(fi_equations[0], (approx[0], approx[1]), 0.1))
        q_2 = float(get_q(fi_equations[1], (approx[1], approx[0]), 0.1))
        print(f'q_1 = {q_1}')
        print(f'q_2 = {q_2}')
        if q_1>=1 or q_2>=1:
            print("q is greater than 1")
            return None, None

    iteration = 0
    while error > tol:
        prev_roots = curr_roots.copy()
        roots_d = roots_to_dict(curr_roots, x)
        for i in range(n):
            try:
                curr_roots[i] = float(fi_equations[i].subs(roots_d))
            except TypeError:
                print("some complex numbers")

            errors[i] = abs(prev_roots[i] - curr_roots[i])
            roots_d = roots_to_dict(curr_roots, x)

        error = np.amax(errors)
        iteration += 1

    if verbose == 1:
        print('stopped iteration method\n')

    return curr_roots, iteration

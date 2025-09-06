import numpy as np
from sympy import symbols, core, exp


def get_jacobi(system_equations: np.array):
    n = system_equations.shape[0]
    x = symbols(f'x:{n}')
    J = np.empty(shape=(n, n), dtype=core.add.Add)
    for i in range(n):
        for j in range(n):
            J[i, j] = system_equations[i].diff(x[j])

    return J


def roots_to_dict(roots, x):
    dict_roots = dict()
    for i in range(len(roots)):
        dict_roots[x[i]] = roots[i]

    return dict_roots



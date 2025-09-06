import numpy as np
from sympy import symbols
from staff_matrix import get_jacobi, roots_to_dict


def newton_solve(system_equations: np.array, approx, tol=0.00001):
    n = system_equations.shape[0]
    x = symbols(f'x:{n}')

    J = get_jacobi(system_equations)

    error = tol * 10000
    iteration = 0
    roots = approx
    while error > tol:
        iteration += 1

        roots_d = roots_to_dict(roots, x)
        jacobi_values = np.zeros(shape=(n, n))
        for i in range(n):
            for j in range(n):
                jacobi_values[i, j] = J[i, j].subs(roots_d)

        jacobi_det = np.linalg.det(jacobi_values)
        print(f"Jacobi det = {jacobi_det}")
        if not jacobi_det:
            print("det equal 0. Can't solve system")
            exit(0)

        F = np.zeros(shape=(n, ))
        for i in range(0, n):
            F[i] = system_equations[i].subs(roots_d)

        delta_x = np.zeros(shape=(n, ), dtype=float)
        delta_x = np.linalg.solve(jacobi_values, -1 * F)

        roots = delta_x + roots
        error = np.amax(abs(delta_x))

    return roots, iteration
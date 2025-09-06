import numpy as np
from tools_func import *


def iteration_method(matrix_a, answers_b, error=0.0001, verbose=0) -> (np.array, int):
    A = np.array(matrix_a, dtype=float)
    b = np.array(answers_b, dtype=float)
    if A.shape[0] != A.shape[1]:
        raise Exception("Array A must be n*n size")
    n = len(A)
    A = check_zeros_diag(A)
    B = np.empty(shape=A.shape)
    for i in range(n):
        for j in range(n):
            if i == j:
                B[i, j] = 0
                continue
            B[i, j] = (-1) * A[i, j] / A[i, i]
    c = np.empty(shape=b.shape[0])
    for i in range(len(c)):
        c[i] = b[i] / A[i, i]
    c = c.reshape((n, 1))
    if verbose == 2:
        eigenvalues = np.linalg.eig(B)[0]
        print('Eigenvalues:')
        for i, eig in enumerate(eigenvalues):
            print(f'{i}. {eig}')
    if check_convergence(B):
        if verbose == 1:
            print('X solution converges by spectrum')
    elif norm_convergence(B):
        if verbose == 1:
            print('X solution converges by its norm')
    else:
        if verbose == 1:
            print("X solution not converges")
        raise Exception("Can't find roots, bcs not converges")
    # printing norms
    if verbose == 1:
        print(f'Spectrum of matrix: {find_spectrum(B)}')
        print(f'||B||_0 = {norm_row(B)}')
        print(f'||B||_1 = {norm_column(B)}')
        print(f'||B||_F = {norm_quad(B)}')
    errors_x = np.empty(shape=n)
    errors_y = np.zeros(shape=n)
    e = 1
    x_exact = np.linalg.solve(A, b).reshape((n, 1))
    x_prev = np.empty(shape=(n, 1))
    x_current = c.copy()
    iteration = 0
    while e > error:
        iteration += 1
        x_prev = x_current.copy()
        for i in range(n):
            x_current[i] = c[i]
            for j in range(n):
                x_current[i][0] += B[i, j] * x_prev[j][0]
        if verbose == 2:
            print(f'{iteration}. --- {x_current.reshape((1, n))}')

        # calculate error
        for i in range(n):
            errors_x[i] = abs(x_prev[i][0] - x_current[i][0])
        errors_y = abs(A.dot(x_current).reshape(n, ) - b)
        e = np.amax(errors_x) + np.amax(errors_y)
    return x_current, iteration
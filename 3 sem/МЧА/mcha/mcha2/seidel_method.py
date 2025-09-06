from tools_func import *


def seidel_method(matrix_a, answers_b, error=0.0001, verbose=0) -> (np.array, int):
    A = np.array(matrix_a, dtype=float)
    b = np.array(answers_b, dtype=float)
    if A.shape[0] != A.shape[1]:
        raise Exception("Array A must be n*n size")
    n = A.shape[0]
    check_zeros_diag(A)
    B = np.empty(shape=A.shape)
    for i in range(n):
        for j in range(n):
            if i == j:
                B[i, j] = 0
                continue
            B[i, j] = (-1) * A[i, j] / A[i, i]
    c = np.empty(shape=b.shape[0])
    for i in range(c.shape[0]):
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
    elif norm_trace(B):
        if verbose == 1:
            print('X solution converges by its norm trace')
    else:
        if verbose == 1:
            print("X solution not converges")
        raise Exception("Can't find roots, bcs not converges")
    # printing norms
    if verbose == 1:
        print(f'Spectrum of matrix: {find_spectrum(B)}')
        print(f'||B||_0 = {norm_row(B)}')
        print(f'||B||_1 = {norm_column(B)}')
        if norm_trace(B):
            print(f'for 1<i<n : | B[i][i] | > sum( B[i][j], 1 < j < n)')
    errors = np.empty(shape=n)
    e = 1
    x_exact = np.linalg.solve(A, b).reshape((n, 1))
    x_current = np.zeros(shape=c.shape, dtype=float)
    x_current[0, 0] = c[0, 0]
    iteration = 0
    while e > error:
        iteration += 1
        error_x = 0  # set big value for error
        for i in range(n):
            x_prev = x_current[i, 0]  # save prev to calc error
            x_current[i, 0] = c[i, 0]

            for j in range(n):
                x_current[i][0] += B[i, j] * x_current[j][0]

            if abs(x_prev - x_current[i, 0]) > error_x:
                error_x = abs(x_prev - x_current[i, 0])
        if verbose == 2:
            print(f'{iteration}. --- {x_current.reshape((1, n))}')
        errors_y = abs(A.dot(x_current).reshape((n,)) - b)
        e = (np.amax(errors_y) + error_x) / 2
    return x_current, iteration
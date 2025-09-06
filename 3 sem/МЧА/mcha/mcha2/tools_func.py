import numpy as np


def print_matrix(matrix):
    A = np.array(matrix)
    n = A.shape[0]
    m = A.shape[1]

    for i in range(n):
        for j in range(m):
            print('{:>6}'.format(A[i, j]), end='')
        print('\n')


def print_equation(matrix_a, answers_b):
    A = np.array(matrix_a)
    b = np.array(answers_b)

    n = A.shape[0]
    m = A.shape[1]

    for i in range(n):
        for j in range(m):
            print(f'{A[i, j]:>6.2f}', end='')
        print(f' --> {b[i]:>6.2f}')
    print('')


def print_x(ls):
    ls = np.array(ls)
    print(f'X = [', end='')
    for i in ls:
        print(f'{i:>8.4f},', end='')
    print(f']')


def norm_column(matrix_a):
    A = np.array(matrix_a, dtype=float)
    n = len(A)
    norms = np.zeros(shape=A.shape)
    for j in range(n):
        for i in range(n):
            norms[j] += abs(A[i, j])
    norm = np.amax(norms)
    return norm


def norm_row(matrix_a):
    A = np.array(matrix_a, dtype=float)
    n = len(A)
    norms = np.zeros(shape=A.shape)
    for i in range(n):
        for j in range(n):
            norms[i] += abs(A[i, j])
    norm = np.amax(norms)
    return norm


def norm_quad(matrix_a):
    A = np.array(matrix_a, dtype=float)
    n = len(A)
    quad = 0
    for i in range(n):
        for j in range(n):
            quad += abs(A[i, j]) ** 2
    quad = quad ** 1 / 2
    return quad


def norm_trace(matrix_a):
    A = np.array(matrix_a, dtype=float)
    n = A.shape[0]
    for i in range(n):
        row_sum = 0
        col_sum = 0
        for j in range(n):
            if i == j:
                continue
            row_sum += abs(A[i, j])
            col_sum += abs(A[j, i])
        if (row_sum < abs(A[i, i])) | (col_sum < abs(A[i, i])):
            continue
        else:
            return False
    return True


def norm_convergence(matrix_a):
    A = np.array(matrix_a, dtype=float)
    if (norm_column(A) < 1) | (norm_row(A) < 1) | (norm_quad(A) < 1):
        return True
    return False


def find_spectrum(matrix_a):
    A = np.array(matrix_a, dtype=float)
    eigen_values = np.linalg.eig(A)[0]
    max_eig = np.amax(abs(eigen_values))
    return max_eig


def check_convergence(matrix_a):
    A = np.array(matrix_a, dtype=float)
    if find_spectrum(A) < 1:
        return True
    return False


def swap_columns(a, i, j):
    for k in range(len(a)):
        a[k][i], a[k][j] = a[k][j], a[k][i]


def check_zeros_diag(matrix):
    nstr = len(matrix)
    for i in range(0, nstr):
        if matrix[i][i] == 0:
            check = True
            for j in range(0, nstr):
                if matrix[i][j] != 0 and matrix[j][i] != 0:
                    swap_columns(matrix, i, j)
                    check = False
                    break
            if check:
                raise Exception(f'Zeros (0) on main diagonal!\nPlace: A[{i}, {i}] Can\'t calculate solution')
    return matrix

def inaccuracy(res, x, epsilon):
    check = True
    for i in range(len(x)):
        tmp = (abs(x[i]-res[i])/abs(x[i]))
        if check > epsilon:
            check = False
            break
    return check
import numpy as np


def check_equal_dim(matrix):
    if matrix.shape[0] != matrix.shape[1]:
        return False
    return True


def frobenius_norm(matrix):
    if not check_equal_dim(matrix):
        raise ValueError("Matrix isn't n, n dimension")

    n = matrix.shape[0]
    f_norm = 0
    for i in range(n):
        for j in range(n):
            f_norm += abs(matrix[i, j]**2)
    f_norm = np.sqrt(f_norm)
    return f_norm


def off(matrix):
    if not check_equal_dim(matrix):
        raise ValueError("Matrix isn't n, n dimension")

    n = matrix.shape[0]
    trace_sum = 0
    for i in range(n):
        trace_sum += matrix[i, i]**2

    off_res = frobenius_norm(matrix)**2 - trace_sum
    return off_res


def calc_non_diag(matrix):
    if not check_equal_dim(matrix):
        raise ValueError("Matrix isn't n, n dimension")

    n = matrix.shape[0]
    sum_n_diag = 0
    for i in range(n):
        for j in range(n):
            if i == j:
                continue
            sum_n_diag += abs(matrix[i, j])
    return sum_n_diag


def max_no_diag(matrix):
    if not check_equal_dim(matrix):
        raise ValueError("Matrix isn't n, n dimension")

    n = matrix.shape[0]
    max_val = matrix[0, 1]
    val_row = 0
    val_col = 1
    for i in range(n):
        for j in range(i + 1, n):
            if abs(matrix[i, j]) > abs(max_val):
                max_val = matrix[i, j]
                val_row = i
                val_col = j

    return max_val, val_row, val_col


def make_zeros(matrix):
    if not check_equal_dim(matrix):
        raise ValueError("Matrix isn't n, n dimension")

    n = matrix.shape[0]
    for i in range(n):
        for j in range(n):
            if abs(matrix[i, j]) < 10**-10:
                matrix[i, j] = 0
    return matrix

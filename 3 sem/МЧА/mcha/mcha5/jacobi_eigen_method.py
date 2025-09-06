import numpy as np
from jacobi_staff import off, check_equal_dim, calc_non_diag, max_no_diag, make_zeros, frobenius_norm
from staff import print_array


def find_eigen(matrix, tol=0.0001, verbose=0):
    if not check_equal_dim(matrix):
        raise ValueError("Matrix isn't n, n dimension")

    if verbose == 1:
        print("Computing eigenvalues...")

    A = matrix.copy()
    n = A.shape[0]
    rotate_matrix = np.eye(n)  # diagonal matrix
    eig_vec = np.zeros(shape=A.shape)
    iteration = 0
    while calc_non_diag(A) > tol:

        if verbose == 1:
            print(f'Frobenius norm: {frobenius_norm(A):.4f}')
        off_A = off(A)
        max_el, p, q = max_no_diag(A)

        teta = np.pi
        if A[p, p] == A[q, q]:
            if max_el > 0:
                teta = np.pi / 4
            else:
                teta = -1 * np.pi / 4
        else:
            teta = np.arctan((2 * max_el) / (A[p, p] - A[q, q])) / 2

        # fill rotate matrix
        for i in range(n):
            for j in range(n):
                if i == j and i == p:
                    rotate_matrix[i, j] = np.cos(teta)
                elif i == j and i == q:
                    rotate_matrix[i, j] = np.cos(teta)
                elif i == p and j == q:
                    rotate_matrix[i, j] = np.sin(teta) * -1
                elif i == q and j == p:
                    rotate_matrix[i, j] = np.sin(teta)
                elif i == j:
                    rotate_matrix[i, j] = 1
                else:
                    rotate_matrix[i, j] = 0

        A = rotate_matrix.T @ A @ rotate_matrix
        off_A_new = off(A)

        if verbose == 2:
            print_array(rotate_matrix, msg="rotation")
            print_array(A, "A:")

        if verbose == 1:
            print(f'{iteration+1} step: \n\toff(A) = {off_A:.4f}\n'
                  f'\toff(A\') = {off_A_new:.4f}')

        if iteration != 0:
            eig_vec = eig_vec @ rotate_matrix
        else:
            eig_vec = rotate_matrix.copy()

        iteration += 1

    if verbose == 1:
        print('Stopping computing...\n')

    return A, eig_vec, iteration

import numpy as np
from jacobi_staff import check_equal_dim


def meth_danil(matrix, tol=0.0001, verbose=0):
    if not check_equal_dim(matrix):
        raise ValueError("Matrix isn't n, n dimension")
    if verbose == 1:
        print("Computing eigenvalues...")
    a = matrix.copy()
    f = matrix.copy()
    n = a.shape[0]
    s = np.eye(n)
    for i in range(n - 1):
        m = np.eye(n)
        m[n - 2 - i][:] = f[n - 1 - i][:]
        f = np.dot(m, f)  # умножаем A на M^(-1) слева
        f = np.dot(f, np.linalg.inv(m))  # умножаем A на M справа
        s = np.dot(s, np.linalg.inv(m))  # находим S
    p = f[0]  # выделяем p
    p = p * (-1)
    p = np.insert(p, 0, 1)
    eig_val = np.roots(p)
    eig_vec = np.zeros(shape=(eig_val.shape[0], n))
    for j in range(0, eig_val.shape[0]):
        y = np.zeros(shape=(n, 1))
        for i in range(0, n):
            y[n-1-i] = eig_val[j]**i
        x = np.dot(s, y)  # находим собственный вектор
        norm = np.linalg.norm(x)
        for i in range(0, n):
            eig_vec[i][j] = x[i]/norm
    return eig_val, eig_vec

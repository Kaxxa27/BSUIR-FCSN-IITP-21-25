import numpy as np
from jacobi_eigen_method import find_eigen
from method_danilevskogo import meth_danil
from staff import print_array, separator, is_matrix_symmetric
import task
import test


E = task.E
A = test.A_1

if not is_matrix_symmetric(A, 0.00001):
    print("Matrix not symmetric")
    exit(0)

eig_val, eig_vec, steps = find_eigen(A, E, 1)

print_array(A, 'Default Matrix:')
print_array(eig_val, '\nMatrix of Eigenvalues:')
print_array(eig_val @ np.ones(shape=(eig_val.shape[0], )), 'Eigenvalues:')
print_array(eig_vec, 'Eigenvectors:')


print("\nVerification:")

w, v = np.linalg.eig(A)
print_array(w, 'Eigenvalues (numpy):')
print_array(v, 'Eigenvectors (numpy):')


print("\nMethod Danilevskogo:")
eig_val, eig_vec = meth_danil(A, E, 1)
print_array(eig_val, '\nEigenvalues:')
print_array(eig_vec, 'Eigenvectors:')
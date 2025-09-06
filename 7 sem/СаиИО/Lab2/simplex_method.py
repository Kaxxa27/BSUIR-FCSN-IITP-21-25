from sympy import zeros, eye
from sympy.functions import transpose
import numpy as np
import bisect


def matrix_C_determinant_is_null(vector_c_k_with_wave, matrix_C_inverse, k):
    row_count, col_count = vector_c_k_with_wave.shape
    e_k = zeros(1, row_count)
    e_k[0, k] = 1
    return e_k * matrix_C_inverse * vector_c_k_with_wave == 0


def get_matrix_B_with_wave(vector_c_k_with_wave, matrix_B, k):
    if matrix_C_determinant_is_null(vector_c_k_with_wave, matrix_B, k):
        raise ArithmeticError("Determinant is null")
    row_count, col_count = vector_c_k_with_wave.shape
    matrix_D = eye(row_count)
    matrix_z = matrix_B * vector_c_k_with_wave
    z_k = matrix_z[k, 0]
    matrix_z[k, 0] = -1
    matrix_d = (-1 / z_k) * matrix_z
    matrix_D[:, k] = matrix_d
    return matrix_D


def get_inverse_matrix(matrix_C):
    row_count, col_count = matrix_C.shape
    i = 0
    matrix_C_construction = eye(row_count)
    matrix_B_construction = eye(row_count)
    set_J = set(range(row_count))
    permutation = range(row_count)
    while i < row_count:
        all_alpha_j_zeros = False
        non_zero_alpha_index = -1
        for j in set_J:
            e_i_1 = zeros(1, row_count)
            e_i_1[0, i] = 1
            alpha_j = e_i_1 * matrix_B_construction * matrix_C[:, j]
            if alpha_j[0, 0] != 0:
                non_zero_alpha_index = j
                break
        else:
            all_alpha_j_zeros = True
        if not all_alpha_j_zeros:
            c_i_with_wave = matrix_C[:, non_zero_alpha_index]
            set_J.remove(non_zero_alpha_index)
            permutation[non_zero_alpha_index] = i
            matrix_C_construction[:, i] = c_i_with_wave
            matrix_D = eye(row_count)
            matrix_z = matrix_B_construction * c_i_with_wave
            z_k = matrix_z[i, 0]
            matrix_z[i, 0] = -1
            matrix_d = (-1 / z_k) * matrix_z
            matrix_D[:, i] = matrix_d
            matrix_B_construction = matrix_D * matrix_B_construction
            i += 1
        else:
            raise ArithmeticError("Determinant is null")
    else:
        matrix_B_output = zeros(row_count, row_count)
        for j in range(row_count):
            matrix_B_output[j, :] = matrix_B_construction[permutation[j], :]
        return matrix_B_output


def get_synthetic_index(basis_indexes_set, j_u_indexes_set):
    j_u_in_basis = set(basis_indexes_set).intersection(set(j_u_indexes_set))
    if j_u_in_basis:
        return min(j_u_in_basis)
    return -1


def find_index_with_non_zero_alpha(not_basis_index_set, k, inverse_basis_matrix, matrix_A):
    e_matrix = zeros(inverse_basis_matrix.shape[0], 1)
    e_matrix[k, 0] = 1
    for j in not_basis_index_set:
        if transpose(e_matrix) * inverse_basis_matrix * matrix_A[:, j] != 0:
            return j
    return -1


def get_extended_matrix_A(matrix_A):
    m, n = matrix_A.shape
    extended_matrix = zeros(m, n + m)
    for i in range(n):
        extended_matrix[:, i] = matrix_A[:, i]
    for i in range(m):
        extended_matrix[i, n + i] = 1
    return extended_matrix


def get_extended_matrix_c(matrix_c, variable_count):
    extended_matrix = zeros(variable_count, 1)
    for i in range(matrix_c.shape[0]):
        extended_matrix[i, 0] = matrix_c[i, 0]
    for i in range(matrix_c.shape[0], variable_count):
        extended_matrix[i, 0] = 0
    return extended_matrix


def remove_negative_rows(matrix_b, matrix_A):
    for i in range(matrix_b.shape[0]):
        if matrix_b[i] < 0:
            matrix_b[i] *= -1
            matrix_A[i, :] *= -1


def get_index_with_negative_estimate(not_basis_index_set, estimates_vector):
    for i, j in enumerate(not_basis_index_set):
        if estimates_vector[i, 0] < 0:
            return j
    return -1


def get_theta_0(basis_indexes_set, vector_z, basis_plan):
    theta_indexes = [j for j in basis_indexes_set if vector_z[basis_indexes_set.index(j), 0] > 0]
    vector_theta = [basis_plan[j, 0] / vector_z[basis_indexes_set.index(j), 0] for j in theta_indexes]
    j_s = theta_indexes[0]
    theta_0 = vector_theta[0]
    for j, theta in zip(theta_indexes[1:], vector_theta[1:]):
        if theta < theta_0 or (theta == theta_0 and j < j_s):
            theta_0 = theta
            j_s = j
    return j_s, theta_0


class SimplexMethod:
    def __init__(self, matrix_c, matrix_A, matrix_b):
        self.matrix_c = matrix_c
        self.matrix_A = matrix_A
        self.matrix_b = matrix_b
        self.m, self.n = matrix_A.shape

    def find_optimum_plan(self, basis_plan, basis_indexes_set, matrix_A=None, matrix_c=None):
        if matrix_A is None:
            matrix_A = self.matrix_A
        if matrix_c is None:
            matrix_c = self.matrix_c
        basis_plan = basis_plan.copy()
        basis_indexes_set = list(basis_indexes_set)
        m, n = matrix_A.shape
        basis_indexes_set.sort()
        not_basis_index_set = sorted(set(range(n)) - set(basis_indexes_set))
        basis_inverse_matrix = get_basis_matrix(basis_indexes_set, matrix_A).inv()
        basis_cost_vector = get_basis_cost_vector(basis_indexes_set, matrix_c)
        while True:
            potentials_vector = transpose(basis_cost_vector) * basis_inverse_matrix
            estimates_vestor = zeros(len(not_basis_index_set), 1)
            for i, j in enumerate(not_basis_index_set):
                estimates_vestor[i, 0] = (potentials_vector * matrix_A[:, j] - matrix_c[j, :])[0, 0]
            for i in range(len(not_basis_index_set)):
                if estimates_vestor[i, 0] < 0:
                    break
            else:
                return basis_plan, basis_indexes_set
            j_0 = get_index_with_negative_estimate(not_basis_index_set, estimates_vestor)
            vector_z = basis_inverse_matrix * matrix_A[:, j_0]
            for i in range(vector_z.shape[0]):
                if vector_z[i, 0] > 0:
                    break
            else:
                raise Exception("Function is unlimited")
            j_s, theta_0 = get_theta_0(basis_indexes_set, vector_z, basis_plan)
            basis_plan[j_0, 0] = theta_0
            for i, j in enumerate(basis_indexes_set):
                basis_plan[j, 0] -= theta_0 * vector_z[i, 0]
            basis_indexes_set.remove(j_s)
            bisect.insort_left(basis_indexes_set, j_0)
            not_basis_index_set.remove(j_0)
            bisect.insort_left(not_basis_index_set, j_s)
            basis_inverse_matrix = get_basis_matrix(basis_indexes_set, matrix_A).inv()
            basis_cost_vector = get_basis_cost_vector(basis_indexes_set, matrix_c)

    def get_basis_plan(self):
        remove_negative_rows(self.matrix_b, self.matrix_A)
        matrix_A = get_extended_matrix_A(self.matrix_A)
        matrix_c = zeros(self.n + self.m, 1)
        variable_quantity = self.n
        for i in range(self.n, self.m + self.n):
            matrix_c[i, 0] = -1
        basis_plan = zeros(self.n + self.m, 1)
        for i in range(self.n, self.m + self.n):
            basis_plan[i, 0] = self.matrix_b[i - self.n]
        j_u_indexes_set = range(self.n, self.n + self.m)
        j_indexes_set = range(self.n)
        basis_plan, basis_indexes_set = self.find_optimum_plan(basis_plan, j_u_indexes_set, matrix_A, matrix_c)
        for i in j_u_indexes_set:
            if abs(basis_plan[i]) >= 0.000001:
                raise Exception("No plans can be found")
        basis_matrix = get_basis_matrix(basis_indexes_set, matrix_A)
        inverse_basis_matrix = basis_matrix.inv()
        while True:
            j_k = get_synthetic_index(basis_indexes_set, j_u_indexes_set)
            if j_k == -1:
                return basis_plan[:variable_quantity, :], basis_indexes_set
            j_0 = find_index_with_non_zero_alpha(basis_indexes_set.index(j_k),
                                                 inverse_basis_matrix, matrix_A)
            if j_0 != -1:
                basis_indexes_set.remove(j_k)
                bisect.insort_left(basis_indexes_set, j_0)
                inverse_basis_matrix = get_basis_matrix(basis_indexes_set, matrix_A).inv()
            else:
                i_0 = j_k - self.n
                k = basis_indexes_set.index(j_k)
                self.matrix_A.row_del(i_0)
                self.matrix_b.row_del(i_0)
                matrix_A = get_extended_matrix_A(self.matrix_A)
                self.m -= 1
                basis_matrix.row_del(i_0)
                basis_matrix.col_del(k)
                inverse_basis_matrix.row_del(k)
                inverse_basis_matrix.col_del(i_0)
                basis_indexes_set.remove(j_k)
                j_u_indexes_set.remove(j_k)
                basis_plan.row_del(j_k)
                decrease_more_than_j_k = lambda x: x - 1 if x > j_k else x
                basis_indexes_set = map(decrease_more_than_j_k, basis_indexes_set)
                j_u_indexes_set = map(decrease_more_than_j_k, j_u_indexes_set)

    def solve(self, maximize='max'):
        self.matrix_A, self.matrix_c = get_cannonical_form(self.matrix_A, self.matrix_c, maximize)
        self.m, self.n = self.matrix_A.shape
        basis_plan, basis_indexes_set = self.get_basis_plan()
        basis_plan, basis_indexes_set = self.find_optimum_plan(basis_plan, basis_indexes_set)
        return basis_plan, basis_indexes_set


def get_cannonical_form(matrix_A, matrix_c, maximize):
    m, n = matrix_A.shape
    extended_matrix = zeros(m, n)
    for i in range(n):
        extended_matrix[:, i] = matrix_A[:, i]
    extended_matrix_c = zeros(extended_matrix.shape[1], 1)
    for i in range(matrix_c.shape[0]):
        if maximize:
            extended_matrix_c[i, 0] = matrix_c[i]
        else:
            extended_matrix_c[i, 0] = -matrix_c[i]
    return extended_matrix, extended_matrix_c


def get_basis_matrix(basis_indexes_set, matrix_A):
    m, n = matrix_A.shape[0], len(basis_indexes_set)
    assert m == n
    basis_matrix = zeros(m, n)
    for i, basis_index in enumerate(basis_indexes_set):
        basis_matrix[:, i] = matrix_A[:, basis_index]
    return basis_matrix


def get_basis_cost_vector(basis_indexes_set, matrix_c):
    basis_cost_vector = zeros(len(basis_indexes_set), 1)
    for i, basis_index in enumerate(basis_indexes_set):
        basis_cost_vector[i, 0] = matrix_c[basis_index, 0]
    return basis_cost_vector


if __name__ == "__main__":
    A = np.array([[-4., 6., 1., 0., 0.],
                  [1., 1., 0., 1., 0.],
                  [0., 0., 0.1, 0.4, -1.]])
    b = np.array([9., 4., 0.5])
    c = np.array([-1, 2, 0, 0, 0])
    model1 = SimplexMethod(c, A, b)
    a1, a2 = model1.solve()
    print(a1)
    print(a2)
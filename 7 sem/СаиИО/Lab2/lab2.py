import math
import numpy as np
import numpy.linalg as la
from simplex_method import SimplexMethod


def check_is_integer(list_x):
    res = True
    for xi in list_x:
        part = abs(xi - math.floor(xi))
        if 10 ** (-6) <= part <= 1 - 10 ** (-6):
            res = False
            break
    return res


def get_position_x(list_x):
    x_ind = 0
    for i, xi in enumerate(list_x):
        part = abs(xi - math.floor(xi))
        if 10 ** (-6) <= part <= 1 - 10 ** (-6):
            x_ind = i
            continue
    return x_ind


def subtract(x, y):
    return list(set(x) - set(y))


def get_basis_matrix(array, Jb):
    shape = array.shape
    if len(shape) == 1:
        return array[Jb]
    else:
        m = array.shape[0]
        rows = list(range(m))
        return array[rows][:, Jb]


def dual_simplex(A, b, c):
    simplex = SimplexMethod(c, A, b)
    x, J_b = simplex.solve()
    opt_plan = []
    for i in x:
        opt_plan.append(i)
    J_b.sort()
    return opt_plan, J_b


def get_decimal_part(number):
    return number - get_integer_part(number)


def get_integer_part(x):
    if abs(math.floor(x + 1) - x) <= 10 ** (-6):
        return math.floor(x + 1)
    else:
        return math.floor(x)


def build_constraint(A, n, Jn, jk, y):
    constraint = np.zeros(n)
    for i, j in enumerate(Jn):
        constraint[j] = get_decimal_part(y[i])
    constraint[n - 1] = -1
    b_i0 = get_decimal_part(jk)
    print('New constraint', constraint)
    print('New b = ', b_i0)
    return constraint, b_i0


def gomory_cut(A, b, c):
    while True:
        opt_plan, Jb = dual_simplex(A, b, c)
        print('x = ', opt_plan)
        print('J_b = ', Jb)
        is_int = check_is_integer(opt_plan)
        if is_int:
            break
        k_ind_temp = get_position_x(opt_plan)
        jk = opt_plan[k_ind_temp]
        k_ind = Jb.index(k_ind_temp)
        print('jk = ', jk)
        print('k = ', k_ind)
        m, n = A.shape
        J = range(n)
        Jn = subtract(J, Jb)
        Ab = get_basis_matrix(A, Jb)
        Ab_inv = la.inv(Ab)
        print('Ab inv = ', Ab_inv)
        print('Jn = ', Jn)
        Anb = get_basis_matrix(A, Jn)
        print('Anb = ', Anb)
        y = np.dot(Ab_inv, Anb)
        print('y = ', y)
        print('yk = ', y[k_ind])
        n_temp = n + 1
        constraint, b_i0 = build_constraint(A, n_temp, Jn, jk, y[k_ind])

        A = np.append(A, np.zeros((m, 1)), axis=1)
        A = np.append(A, np.array([constraint]), axis=0)
        b = np.append(b, b_i0)
        c = np.append(c, 0)
        print('new A: ', A)
        print('new b: ', b)
        print('new c: ', c)
        print('----------------------')
    return is_int, opt_plan, Jb


def main():
    A = np.array([[-4, 6, 1, 0],
                  [1, 1, 0, 1]])
    m, n = A.shape
    b = np.array([9, 4])
    c = np.array([-1, 2, 0, 0])

    is_int, opt_plan, Jb = gomory_cut(A, b, c)
    print('----------------------')
    print(f'Has integer solutions: {is_int}')
    if is_int:
        print('x = ', opt_plan[:n])


if __name__ == '__main__':
    main()
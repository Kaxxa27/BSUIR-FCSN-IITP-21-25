import numpy

C = numpy.array([[0.2, 0, 0.2, 0, 0],
                 [0, 0.2, 0, 0.2, 0],
                 [0.2, 0, 0.2, 0, 0.2],
                 [0, 0.2, 0, 0.2, 0],
                 [0, 0, 0.2, 0, 0.2]])
D = numpy.array([[2.33, 0.81, 0.67, 0.92, -0.53],
                 [-0.53, 2.33, 0.81, 0.67, 0.92],
                 [0.92, -0.53, 2.33, 0.81, 0.67],
                 [0.67, 0.92, -0.53, 2.33, 0.81],
                 [0.81, 0.67, 0.92, -0.53, 2.33]])
b = numpy.array([4.1, 4.2, 4.2, 4.2, 4.2])
b = b.transpose()
A = 14 * C + D
X = numpy.zeros(len(A))


def swap_columns(a, i, j):
    for k in range(len(a)):
        a[k][i], a[k][j] = a[k][j], a[k][i]


def check_zeros_diag(matrix, b):
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
                if b[i] == 0:
                    print('The system has infinitive amount of solutions')
                else:
                    print('The system has no solutions')
                exit()



def straight_run(matrix, b):
    for nrow, row in enumerate(matrix):  # nrow равен номеру строки row содержит саму строку матрицы
        divider = row[nrow]  # диагональный элемент
        row /= divider  # делим на диагональный элемент.
        b[nrow] /= divider
        bfactor = b[nrow]
        # вычитаем из всех нижележащих строчек
        for lower_row in range(nrow + 1, len(matrix)):
            factor = matrix[lower_row][nrow]  # элемент строки в колонке nrow
            matrix[lower_row] -= factor * row  # вычитаем, чтобы получить ноль в колонке nrow
            b[lower_row] -= factor * bfactor
    return matrix, b


def reverse_run(a, b):  # перебор строк в обратном порядке
    n = len(a)
    for k in reversed(range(0, n)):
        X[k] = (b[k] - sum(a[k][i] * X[i] for i in range(k + 1, n))) / a[k][k]


def gauss(A, b):
    check_zeros_diag(A, b)
    straight_run(A, b)
    reverse_run(A, b)
    for i in range(len(X)):
        print("%.20f" % X[i], end=" ")
    print('\n')


def straight_run_column(matrix, b):
    for nrow in range(len(matrix)): # nrow равен номеру строки
        # argmax возвращает номер строки с максимальным элементом в уменьшенной матрице, которая начинается со строки
        # nrow. Поэтому нужно прибавить nrow к результату
        pivot = nrow + numpy.argmax(abs(matrix[nrow:, nrow]))
        if pivot != nrow:
            matrix[[nrow, pivot]] = matrix[[pivot, nrow]] # swap
            change = b[pivot]
            b[pivot] = b[nrow]
            b[nrow] = change
        row = matrix[nrow]
        divider = row[nrow]  # диагональный элемент
        if abs(divider) < 1e-10:
            # почти нуль на диагонали. Продолжать не имеет смысла, результат счёта неустойчив
            print(f"Matrix is incompatible. Max element in column {nrow}: {divider:.3g}")
            exit()
        row /= divider # делим на диагональный элемент.
        b[nrow] /= divider
        bfactor = b[nrow]
        # теперь надо вычесть приведённую строку из всех нижележащих строчек
        for lower_row in range(nrow + 1, len(matrix)):
            factor = matrix[lower_row][nrow]  # элемент строки в колонке nrow
            matrix[lower_row] -= factor * row  # вычитаем, чтобы получить ноль в колонке nrow
            b[lower_row] -= factor * bfactor


def gauss_maxcolumn (A, b):
    check_zeros_diag(A, b)
    straight_run_column(A,b)
    reverse_run(A,b)
    for i in range(len(X)):
        print("%.18f" % X[i], end=" ")
    print('\n')


def straight_run_max(matrix, b):
    for nrow in range(len(matrix)): # nrow равен номеру строки
        mrow, mcol = max_element(matrix, nrow)
        if mrow != nrow:
            matrix[[nrow, mrow]] = matrix[[mrow, nrow]] # swap
            change = b[mrow]
            b[mrow] = b[nrow]
            b[nrow] = change
        swap_columns(matrix, nrow, mcol)
        row = matrix[nrow]
        divider = row[nrow]  # диагональный элемент
        if abs(divider) < 1e-10:
            # почти нуль на диагонали. Продолжать не имеет смысла, результат счёта неустойчив
            print(f"Matrix is incompatible. Max element in matrix: {divider:.3g}")
            exit()
        row /= divider # делим на диагональный элемент.
        b[nrow] /= divider
        bfactor = b[nrow]
        # теперь надо вычесть приведённую строку из всех нижележащих строчек
        for lower_row in range(nrow + 1, len(matrix)):
            factor = matrix[lower_row][nrow]  # элемент строки в колонке nrow
            matrix[lower_row] -= factor * row  # вычитаем, чтобы получить ноль в колонке nrow
            b[lower_row] -= factor * bfactor


def max_element(A, k):
    maximum = A[k-1][k-1]
    max_index = [k-1, k-1]
    for i in range(k-1, len(A)):
        for j in range(k-1, len(A)):
            if maximum < A[i][j]:
                maximum = A[i][j]
                max_index = [i, j]
    return max_index


def gauss_max(A, b):
    check_zeros_diag(A, b)
    straight_run_max(A, b)
    reverse_run(A, b)
    for i in range(len(X)):
        print("%.18f" % X[i], end=" ")
    print('\n')


if __name__ == '__main__':
    print(A)
    n = numpy.linalg.solve(A, b)
    for i in range(len(n)):
         print("%.20f" % n[i], end=" ")
    print('\n')
    gauss(A, b)
   # gauss_maxcolumn(A,b)
   # gauss_max(A, b)


import numpy as np
import math


def basis_matrix(A: list, b: list) -> list:
    A_b = np.zeros((len(b), A.shape[0]))
    i = 0
    for k in b:
        for j in range(len(A)):
            A_b[j][i] = A[j][k - 1]
        i += 1
    return A_b


def basis_vector(c: list, b: list) -> list:
    i = 0
    c_b = [0 for _ in b]
    for index in b:
        c_b[i] = c[index - 1]
        i += 1
    return c_b


def find_negative(delta: list):
    j = 0
    for item in delta:
        if item < 0:
            return (item, j)
        j += 1


def find_tetta_vector(z: list, b: list, x: list):
    tetta = np.zeros(shape=(1, 3))
    minimum = 1000000000
    index = 0
    for i in range(len(z)):
        if z[i] <= 0:
            tetta[0][i] = math.inf
        else:
            tetta[0][i] = x[0][b[i] - 1] / z[i]
        if tetta[0][i] < minimum:
            minimum = tetta[0][i]
            index = i
    return (tetta, minimum, index)


def update_x(x: list, minimum: int, z: list, b: list, index: int, previous: int) -> list:
    x[0][index - 1] = minimum
    x[0][previous - 1] = 0
    for i in range(len(b)):
        if b[i] == index:
            continue
        x[0][b[i] - 1] -= minimum * z[i]
    return x


def symplex(A, b, c, x):
    while True:
        # 1
        basis_A = basis_matrix(A, b)  # Ab
        basis_inverted_A = np.linalg.inv(basis_A)

        # 2
        basis_c = basis_vector(c, b)  # Cb
        basis_transported_c = np.transpose(basis_c)

        # 3 / вектор потенциалов
        u = np.matmul(basis_transported_c, basis_inverted_A)

        # 4 / вектор оценок
        delta = np.matmul(u, A) - c

        # 5 / ∆ >= 0
        if np.all(delta >= 0):
            print('#' * 50)
            print('Матрица Ab: ', basis_inverted_A, sep='\n')
            print('Вектор Cb: ', basis_transported_c)
            print('Вектор потенциалов: ', u)
            print('Вектор оценок', delta)
            print('Вектор базисных индексов b: ', b)
            print('Оптимальный план x: ', x)
            return (basis_inverted_A, x, b)

        # 6 / найти первую отрицательную компоненту в векторе оценок ∆
        delta_negative, delta_negative_position = find_negative(delta)

        # 7 / Вектор z: Ab^-1 * Aj0
        z = np.array(np.matmul(basis_inverted_A, A[:, delta_negative_position]))

        # 8 / Вектор θ
        # 9 / Найти минимум в θ
        tetta, minimum, index = find_tetta_vector(z, b, x)

        # 10 / условие неограниченности
        if minimum == math.inf:
            print('целевой функционал задачи не ограничен сверху на множестве допустимых планов')
            return

        previous = b[index]  # j*

        # 12 обновить b
        b[index] = delta_negative_position + 1

        # 13 Обновить вектор x
        x = update_x(x, minimum, z, b, index, previous)

        print('#' * 50)
        print('Матрица Ab: ', basis_inverted_A, sep='\n')
        print('Вектор Cb: ', basis_transported_c)
        print('Вектор потенциалов: ', u)
        print('Вектор оценок', delta)
        print('Значение: ', delta_negative)
        print('Позиция: ', delta_negative_position + 1)
        print('Вектор z: ', z)
        print('Вектор θ: ', tetta)
        print('Минимум: ', minimum)
        print('Индекс, на котором достигается минимум: ', index)
        print('Вектор базисных индексов b: ', b)
        print('Допустимый план x: ', x)


if __name__ == "__main__":
    # cx -> max
    # Ax = b
    # x >= 0

    A = np.array([
        [-1, 1, 1, 0, 0],
        [1, 0, 0, 1, 0],
        [0, 1, 0, 0, 1]
    ])

    c = np.array([1, 1, 0, 0, 0])

    x = np.array([[0, 0, 1, 3, 2]])
    b = np.array([3, 4, 5])

    # c, A, (x, B) - входные данные
    # (x, B) - базисный допустимый план задачи
    # ограничен ли сверху целевой функционал задачи на мн планов
    symplex(A, b, c, x)

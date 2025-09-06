import numpy as np


def basis_matrix(A: list, b: list) -> list:
    A_b = np.zeros((len(b), A.shape[0]))
    i = 0
    for k in b:
        for j in range(len(A)):
            A_b[j][i] = A[j][k-1]
        i += 1
    return A_b


def basis_vector(c: list, b: list) -> list:
    i = 0
    c_b = [0 for _ in b]
    for index in b:
            c_b[i] = c[index-1]
            i += 1
    return c_b


def main():
    A = np.array([
        [-2, -1, -4, 1, 0],
        [-2, -2, -2, 0, 1]
    ])
    B = [4, 5]
    c = np.array([-4, -3, -7, 0, 0])
    b = np.array([
        [-1],
        [-1.5]
    ])
    flag = True
    counter = 1
    while True:
        print()
        print('-' * 20 + 'Итерация ' + str(counter) + '-' * 20)
        basis_A = basis_matrix(A, B)  # базисная матрица А
        print('1. Базисная матрица А: ', basis_A, sep='\n')

        basis_inverted_A = np.linalg.inv(basis_A)  # матрица, обратная базисной
        print('2. Матрица, обратная базисной: ', basis_inverted_A, sep='\n')

        basis_c = basis_vector(c, B)  # вектор, состоящий из компонент вектора с с базисными индексами из вектора В
        print('3. Вектор, состоящий из компонент вектора с с базисными индексами из вектора В: ', basis_c, sep='')

        y = np.matmul(basis_c, basis_inverted_A)  # базисный допустимый план двойственной задачи
        print('4. Базисный допустимый план двойственной задачи: ', y, sep='')

        k_b = np.matmul(basis_inverted_A, b)  # компоненты псевдоплана с базисными индексами
        print('5. Компоненты псевдоплана с базисными индексами: ', k_b, sep='\n')
        j = 0
        k = [0, 0, 0, 0, 0]
        # псевдоплан, соответствующий текущему базисному допустимому плану y
        for item in B:
            k[item - 1] = k_b[j][0]
            j += 1
        if all(x >= 0 for x in k):
            print('Оптимальный план задачи найден: ', k)
            break
        print('6. Псевдоплан k: ', k, sep='')

        negative_component = min(k)  # минимальный отрицательный элемент
        print('7. Минимальный отрицательный элемент: ', negative_component, sep='')

        position_of_nefative_component = k.index(negative_component) + 1  # индекс этого элемента
        print('8. Индекс минимального отрицательного элемента псевдоплана: ', position_of_nefative_component, sep='')

        position_of_j = B.index(position_of_nefative_component) + 1  # индекс j
        print('9. Индекс j: ', position_of_j, sep='')

        delta_y = basis_inverted_A[position_of_j - 1]  # находим y - j-ая строка матрицы А
        print('10. j-ая строка матрицы A: ', delta_y, sep='')

        without_intersections = list(set([1, 2, 3, 4, 5]) - set(B))  # убираем все перестановки
        print('11. Разность множеств [1, 2, 3, 4, 5] и B: ', without_intersections, sep='')

        print('12. Элементы вектора мю: ')
        M = []  # массив элементов
        for item in without_intersections:
            m = np.matmul(delta_y, A[:, item - 1])
            print('    ', m)
            if m < 0:
                y = y.transpose()
                item_m = (c[item - 1] - np.matmul(A[:, item - 1].transpose(), y)) / m
                M.append(item_m)
            else:
                print("Задача несовместна!")
                flag = False
                break
        if not flag:
            break
        print('13. Вектор М: ', M, sep='')

        _min = min(M)  # находим минимальный элемент
        print('14. Минимальный элемент вектора М: ', _min, sep='')

        index_of_min = M.index(_min) + 1  # индекс минимального
        print('15. Индекс минимального элемента вектора М: ', index_of_min, sep='')

        B[position_of_j - 1] = index_of_min  # замена элемента в векторе B
        print('16. Вектор В после замены элементов: ', B, sep='')
        counter += 1


if __name__ == "__main__":
    main()

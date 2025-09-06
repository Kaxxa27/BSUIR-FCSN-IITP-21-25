from collections import Counter
from copy import deepcopy

import numpy as np


def balace_cond(a, b, c):
    if (diff := sum(a) - sum(b)) > 0:
        b_new = np.append(b, diff)
        a_new = a
        c_new = np.append(c, [[0] * c.shape[0]], axis=1)
        print(
            'Количество поставщиков больше числа потребителей, следовательно, добавим фиктивного потребителя и обновим матрицу c')
    elif diff < 0:
        b_new = b
        a_new = np.append(a, -diff)
        c_new = np.append(c, [[0] * c.shape[1]], axis=0)
        print(
            'Количество потребителей больше числа поставщиков, следовательно, добавим фиктивного поставщика и обновим матрицу c')
    else:
        a_new = a
        b_new = b
        c_new = c
        print('Изменения не требуются')
    print('Вектор поставщиков: ', a_new, sep='')
    print('Вектор потребителей: ', b_new, sep='')
    print('Матрица, содержащая значения тарифов на перевозку груза: ', c_new, sep='\n')
    return a_new, b_new, c_new


def main():
    a = np.array([100, 300, 300])
    print('Вектор поставщиков: ', a, sep='')
    b = np.array([300, 200, 200])
    print('Вектор потребителей: ', b, sep='')
    c = np.array([[8, 4, 1],
                  [8, 4, 3],
                  [9, 7, 5]])
    print('Матрица, содержащая значения тарифов на перевозку груза: ', c, sep='\n')
    print('При несоответствии количества поставщиков количеству потребителей и наоборот выполняем балансировку')
    a, b, c = balace_cond(a, b, c)
    # Шаг 1. Метод северо-западного угла
    # План перевозок и список базисных позиций
    print('Далее составляем опорный план методом северо-западного угла')
    x = np.zeros((len(a), len(b)))
    B = []
    i, j = 0, 0
    while i < len(a) and j < len(b):
        minimum = min([a[i], b[j]])
        x[i, j] = minimum
        B.append((i, j))
        a[i] -= minimum
        b[j] -= minimum
        if a[i] == 0 and i < len(a) - 1:
            i += 1
        elif b[j] == 0:
            j += 1
    print('Базисный план x: ', x, sep='\n')
    print('Список координат, соответствующий непустым ячейкам в базисном плане: ', B, sep='')
    print('Реализация метода потенциалов')
    n, m = len(a), len(b)
    counter = 0
    while True:
        counter += 1
        print(f'----------Итерация {counter}----------')
        A = np.zeros((m + n, m + n))
        b = np.zeros(m + n)
        for num, (i, j) in enumerate(B):
            A[num][i] = 1
            A[num][m + j] = 1
            b[num] = c[i][j]
        A[-1][0] = 1

        # Находим u и v
        u_v = np.linalg.solve(A, b)
        u = u_v[:m]
        v = u_v[m:]
        print('Находим потенциал u: ', u, sep='')
        print('Находим потенциал v: ', v, sep='')

        print('Проверка условий оптимальности')
        optimal, flag = True, True
        for i in range(m):
            if flag:
                for j in range(n):
                    if u[i] + v[j] > c[i][j]:
                        optimal, flag = False, False
                        B.append((i, j))
                        print('   План не оптимален!')
                        print('   Обновляем вектор координат B: ', B, sep='')
                        break
        if optimal:
            print('   План оптимальный, задача решена')
            print('   План x: ', x, sep='\n')
            break

        print('Если в строке/столбце меньше двух базисных клеток, то выполняем операцию удаления')
        B_copy = deepcopy(B)
        while True:
            i_list = [i for (i, j) in B_copy]
            j_list = [j for (i, j) in B_copy]

            i_counter = Counter(i_list)
            j_counter = Counter(j_list)

            i_to_rm = [i for i in i_counter if i_counter[i] == 1]
            j_to_rm = [j for j in j_counter if j_counter[j] == 1]

            if not i_to_rm and not j_to_rm:
                break
            B_copy = [(i, j) for (i, j) in B_copy if i not in i_to_rm
                      and j not in j_to_rm]
        print('Вектор В после операции удаления: ', B_copy, sep='')

        print('Распределение клеток по + и -')
        plus, minus = [], []
        plus.append(B_copy.pop())

        while B_copy:
            if len(plus) > len(minus):
                for index, (i, j) in enumerate(B_copy):
                    if plus[-1][0] == i or plus[-1][1] == j:
                        minus.append(B_copy.pop(index))
                        break
            else:
                for index, (i, j) in enumerate(B_copy):
                    if minus[-1][0] == i or minus[-1][1] == j:
                        plus.append(B_copy.pop(index))
                        break
        print('   Плюсы: ', plus, sep='')
        print('   Минусы: ', minus, sep='')

        print('Обновление клеток с учетом знаков и Θ')
        theta = min(x[i][j] for i, j in minus)
        for i, j in plus:
            x[i][j] += theta
        for i, j in minus:
            x[i][j] -= theta

        for i, j in minus:
            if x[i][j] == 0:
                B.remove((i, j))
                break
        print('Обновленный план задачи: ', x, sep='\n')
        print('Обновленный вектор координат B: ', B, sep='')


if __name__ == "__main__":
    main()







"""
1. Построить начальную матрицу распределения товаров методом северо-западного угла.
2. Построить систему потенциалов для определения стоимости доставки товаров.
3. Проверить потенциалы на наличие положительных циклов.
4. Построить граф для поиска пути с положительным циклом.
5. Найти путь с положительным циклом и обновить матрицу распределения товаров.

Повторять шаги 2-5, пока не будет найден оптимальный план распределения товаров.
"""
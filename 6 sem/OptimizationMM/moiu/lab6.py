import numpy as np


def get_basis_vector(c, B):
    i = 0
    c_b = [0 for _ in B]
    for index in B:
        c_b[i] = c[index]
        i += 1
    return c_b


def check(delta_x):
    for i in range(len(delta_x)):
        if delta_x[i] < 0:
            return i
    return -1


def main():
    c = np.array([-8, -6, -4, -6])
    A = np.array([[1, 0, 2, 1],
                  [0, 1, -1, 2]])
    x = np.array([2, 3, 0, 0])
    B = np.array([0, 1])
    Bs = np.array([0, 1])
    D = np.array([[2, 1, 1, 0],
                  [1, 1, 0, 0],
                  [1, 0, 1, 0],
                  [0, 0, 0, 0]])
    counter = 0
    print('----Исходные данные задачи---')
    print('Матрица A: ', A, sep='\n')
    print('Матрица D: ', D, sep='\n')
    print('Вектор B: ', B, sep='')
    print('Вектор с: ', c, sep='')
    print('Вектор x: ', x, sep='')
    while True:
        counter += 1
        print(f'----------Итерация {counter}----------')
        A_b = A[:, B]
        A_b_inv = np.linalg.inv(A_b)

        # ШАГ 1
        print('Нахождение промежуточных векторов')
        c_x = c + np.dot(x, D)
        c_b = get_basis_vector(c_x, B)
        c_b = [i * (-1) for i in c_b]
        u_x = np.dot(c_b, A_b_inv)
        delta_x = np.dot(u_x, A) + c_x
        print('Вектор с(x): ', c_x, sep='')
        print('Вектор u(x): ', u_x, sep='')
        print('Вектор ∆(x): ', delta_x, sep='')

        # ШАГ 2
        print("Проверка условие оптимальности текущего правильного опорного плана")
        j0 = check(delta_x)
        if j0 == -1:
            print('Условие оптимальности выполняется => задача решена!')
            print('Оптимальный план задачи: ', x, sep='')
            break
        print('Не все компоненты вектора ∆(x) неотрицательные, переходим на следующий шаг.')

        # ШАГ 3
        print('Выбираем отрицательную компоненту плана ∆(x) и индекс выбранной компоненты запоминаем в переменной j0')
        print('Индекс выбранной компоненты: ', j0, sep='')
        l = np.zeros(len(x))
        l[j0] = 1
        A_b_ext = A[:, Bs]

        H = np.bmat([
            [D[Bs, :][:, Bs], A_b_ext.T],
            [A_b_ext, np.zeros((len(A), len(A)))]
        ])
        H_inv = np.array(np.linalg.inv(H))

        b_starred = np.concatenate((D[Bs, j0], A[:, j0]))
        x_temp = np.dot(-H_inv, b_starred)

        # ШАГ 4
        l[:len(Bs)] = x_temp[:len(Bs)]
        print('Находим  вектор l(По множеству Jb* и j0): ', l, sep='')

        # Шаг 5
        print('Находим Θ для каждого элемента множества расширенной опоры ограничений')
        sigma = np.dot(np.dot(l, D), l)
        theta = {}
        theta[j0] = np.inf if sigma == 0 else np.abs(delta_x[j0]) / sigma

        for j in Bs:
            if l[j] < 0:
                theta[j] = -x[j] / l[j]
            else:
                theta[j] = np.inf

        j_s = min(theta, key=theta.get)
        theta_0 = theta[j_s]
        print('Найденное Θ: ', theta, sep='')
        print('Θ минимальное: ', theta_0, sep='')
        print('Индекс минимума: ', j_s, sep='')

        print('Проверка ограниченности целевой функции')
        if theta_0 == np.inf:
            print('Функция не ограничена множеством допустимых планов')

        # ШАГ 6. Обновляем допустимый план
        print('Обновим допустимый план')
        x = x + theta_0 * l
        print('Обновленный допустимый план: ', x, sep='')
        if j_s == j0:
            Bs = np.append(Bs, j_s)
        elif j_s in Bs and j_s not in B:
            Bs = np.delete(Bs, j_s)
        elif j_s in B:
            third_condition = False
            s = B.index(j_s)

            print('Обновляем опоры ограничений')
            for j_plus in set(Bs).difference(B):
                if (np.dot(A_b_inv, A[:, j_plus]))[s] != 0:
                    third_condition = True
                    B[s] = j_plus
                    Bs = np.delete(Bs, j_s)

            if not third_condition:
                B[s] = j0
                Bs[Bs.index(j_s)] = j0
            print('Обновленные опоры ограничений: ', Bs, sep='')


if __name__ == "__main__":
    main()

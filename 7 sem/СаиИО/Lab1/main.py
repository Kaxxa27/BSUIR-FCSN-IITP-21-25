import numpy as np
import math
from copy import deepcopy


MATRIX_A_INITIAL = np.array([
    [5, 9],
    [9, 5]
])
VECTOR_C = np.array([1, 1])
VECTOR_B = np.array([63, 63])
VECTOR_D_MINUS = np.array([1, 1])
VECTOR_D_PLUS = np.array([6, 6])


def get_solution_with_branch_and_bound_method(
        matrix_a: np.array,
        vector_c: np.array,
        vector_b: np.array,
        vector_d_minus: np.array,
        vector_d_plus: np.array
) -> np.array:
    c: np.array = deepcopy(vector_c)

    a: np.array = deepcopy(matrix_a)
    b: np.array = deepcopy(vector_b)
    d_minus: np.array = deepcopy(vector_d_minus)
    d_plus: np.array = deepcopy(vector_d_plus)
    n: int = len(c)
    m: int = len(a[0])

    #  ШАГ 1: Преобразуем задачу (1)
    for index in range(0, n, 1):
        if c[index] > 0:
            # преобразуем i-тую компоненту в векторе
            c[index] *= -1

            for row in range(0, n, 1):
                # преобразуем в матрице i-тый столбец
                a[row][index] *= -1

            # в каждомй из векторов преобразуем i-тую компоненту
            d_plus[index] *= -1
            d_minus[index] *= -1

            # после поменяем их местами
            (d_plus[index], d_minus[index]) = (d_minus[index], d_plus[index])

    # ШАГ 2: Преобраузем задачу в каноническую форму без учета ограничений неотрицательности
    matrix_from_below = np.identity(n)
    matrix_from_right = np.identity(m + n)

    a = np.concatenate((a, matrix_from_below))
    a = np.concatenate((a, matrix_from_right), axis=1)

    b = np.concatenate((b, d_plus))
    d_minus = np.concatenate((d_minus, np.array([0] * (n + m))))
    c = np.concatenate((c, np.array([0] * (n + m))))

    alpha: float = 0

    # ШАГ 3: Создадим переменные x*, r, S
    lin_programming = tuple[np.array, np.array, np.array, np.array, np.array, float]
    S: list[lin_programming] = []

    x_star: list[float] = []
    r: float = -np.inf

    S.append((c, a, b, d_minus, d_minus, alpha))

    iterations_count: int = 0

    # ШАГ 4: 2 Случая
    # Случай 2
    while S:
        iterations_count += 1
        # извлечем из стека задачу ЛП
        current_c, current_a, current_b, current_d_minus, current_delta, current_alpha = S.pop()

        print(f"\n--- Итерация {iterations_count} ---")
        print(f"c: {current_c}")
        print(f"b: {current_b}")
        print(f"d_minus: {current_d_minus}")
        print(f"delta: {current_delta}")
        print(f"alpha: {current_alpha}")

        alpha_new = current_alpha + np.dot(current_c, current_d_minus)
        b: np.array = current_b - np.dot(current_a, current_d_minus)

        B: list[int] = [n + i for i in range(n + m)]

        # решим задачу (3) двойственным симплекс методом
        x_wave: np.array = dual_simplex_method(current_c, current_a, b, B)

        if x_wave is None:
            print("Нет решения")
            continue
        else:
            print(f"x ̃: {x_wave}")

        # проверка на целочисленность
        is_integer: bool = all(list(map(lambda x: x == int(x), x_wave)))

        # если план x_тильда целочисленный
        if is_integer:
            # х_с_крышечкой
            x_lid = np.array(x_wave) + current_delta
            r_current: float = np.dot(current_c, x_lid) + alpha_new

            if len(x_star) == 0 or r < r_current:
                x_star = deepcopy(x_lid)
                r = r_current
            print(f"Целочисленный план: x ̂ = {x_lid}, r = {r_current}")
        else:
            # если план x_тильда дробный
            first_float: int = -1

            for index in range(0, len(x_wave), 1):
                if int(x_wave[index]) != x_wave[index]:
                    # выбираем дробную компоненту
                    first_float = index
                    break

            r_current: float = math.floor(np.dot(current_c, x_wave) + alpha_new)

            if len(x_star) == 0 or r < r_current:
                b_new_new: np.array = np.copy(b)
                b_new_new[m + first_float] = math.floor(x_wave[first_float])
                d_minus_new: np.array = np.zeros(2 * n + m)
                d_minus_new[first_float] = math.ceil(x_wave[first_float])
                new_delta: np.array = current_delta + d_minus_new

                S.append((current_c, current_a, b_new_new, np.zeros(2 * n + m), current_delta, alpha_new))
                S.append((current_c, current_a, b, d_minus_new, new_delta, alpha_new))

    # Случай 1: Когда стек пустой
    # если в x* нет плана ...
    if len(x_star) == 0:
        print('Задача несовместна')
        return None

    # иначе x* - оптимальный план
    for i in range(n):
        if vector_c[i] >= 0:
            x_star[i] *= -1
    print('Результат: ')
    return x_star[:n]


def dual_simplex_method(c, A, b, B) -> np.array:
    while True:
        len_b = len(B)
        A_b = np.zeros((len_b, len_b))
        for i in range(len_b):
            A_b[:, i] = A[:, B[i]]

        A_b_inv = np.linalg.inv(A_b)

        c_b = [c[i] for i in B]

        y = np.dot(c_b, A_b_inv)
        k = np.zeros(len(c))
        A_b_inv_b = np.dot(A_b_inv, b)
        for i in range(len(B)):
            k[B[i]] = A_b_inv_b[i]
        jk = -1
        is_optimal_plan = True

        for el in k:
            if el < 0:
                jk = el
                is_optimal_plan = False
                break

        if is_optimal_plan:
            return k

        k0 = list(k).index(jk)
        delta_y = A_b[B.index(k0)]

        not_B = list(set(range(len(c))) - set(B))
        mu = np.zeros(len(not_B))
        for j in range(len(mu)):
            mu[j] = np.dot(delta_y, A[:, not_B[j]])

        neg_mu = list()
        for elem in mu:
            if elem < 0:
                neg_mu.append(not_B[list(mu).index(elem)])
        if not neg_mu:
            return None

        sigmas = np.zeros(len(neg_mu))
        for j in range(len(sigmas)):
            i = neg_mu[j]
            sigmas[j] = (c[i] - np.dot(A[:, i], y)) / mu[j]

        sigma0 = min(sigmas)
        j0 = list(sigmas).index(sigma0)

        B[B.index(k0)] = not_B[j0]


def main():
    solution = get_solution_with_branch_and_bound_method(
        MATRIX_A_INITIAL,
        VECTOR_C,
        VECTOR_B,
        VECTOR_D_MINUS,
        VECTOR_D_PLUS
    )

    print(solution)


if __name__ == '__main__':
    main()
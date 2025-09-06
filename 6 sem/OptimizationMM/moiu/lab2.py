import numpy as np
from lab1 import lab1

def main_phase_simplex_method(c, A, b, basis, x):
    m, n = A.shape

    # 1. Строим базисную матрицу AB и находим ее обратную матрицу A_invB;
    AB = A[:, basis]
    A_invB = np.linalg.inv(AB)

    # 2. Формируем вектор cB — вектор компонент вектора c, чьи индексы принадлежат множеству B;
    cB = c[basis]

    # 3. Находим вектор потенциалов u⊺ = c⊺BA_invB;
    u = np.matmul(cB, A_invB)

    while True:
        # 4. Находим вектор оценок ∆⊺ = u⊺A − c⊺;
        Delta = np.matmul(u, A) - c

        # 5. Проверяем условие оптимальности текущего плана x
        if np.all(Delta >= 0):
            print(f"Оптимальный план - {x}")
            return

        # 6. Находим в векторе оценок ∆ первую отрицательную компоненту и ее индекс сохраним в переменной j0;
        j0 = np.argmin(Delta)

        # 7. Вычисляем вектор z = A_invB @ Aj0;
        Aj0 = A[:, j0]
        z = np.matmul(A_invB, Aj0)

        # 8. Находим вектор θ⊺
        theta = [x[basis[indx]] / z[indx] if z[indx] > 0 else np.inf for indx in range(m)]

        # 9. Вычисляем θ0 = min(θi);
        theta0 = np.min(theta)

        # 10. Проверяем условие неограниченности целевого функционала;
        if np.isinf(theta0):
            print("Целевой функционал задачи не ограничен сверху на множестве допустимых планов")
            return

        # 11. Находим первый индекс k, на котором достигается минимум в (2), и сохраним в переменной s;
        s = np.argmin(theta)

        # 12. Обновляем план x и базис;
        for indx in range(m):
            x[basis[indx]] -= theta0 * z[indx]
        x[j0] = theta0
        basis[s] = j0

        # 13. Обновляем матрицу A_invB с помощью Sherman-Morrison формулы;
        A_invB = lab1(A_invB, A[:, j0], s)

        # 2. Формируем вектор cB — вектор компонент вектора c, чьи индексы принадлежат множеству B;
        cB = c[basis]

        # 3. Находим вектор потенциалов u⊺ = c⊺BA_invB;
        u = np.matmul(cB, A_invB)


def main():
    c = np.array([1, 1, 0, 0, 0])
    A = np.array([[-1, 1, 1, 0, 0],
                  [1, 0, 0, 1, 0],
                  [0, 1, 0, 0, 1]])
    b = np.array([1, 3, 2])
    basis = [2, 3, 4]
    x0 = np.array([0, 0, 1, 3, 2])

    main_phase_simplex_method(c, A, b, basis, x0)


if __name__ == "__main__":
    main()
    #Базисный допустимый план задачи
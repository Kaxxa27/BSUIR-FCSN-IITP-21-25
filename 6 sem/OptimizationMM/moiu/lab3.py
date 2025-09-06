import numpy as np


def lab1(RevA, X, Col):
    n = RevA.shape[0]
    l = np.dot(RevA, X)

    if abs(l[Col]) == 0:
        raise ValueError("Матрица необратима")

    one_divide_li = -1.0 / l[Col]
    l[Col] = -1

    l *= one_divide_li
    res = np.zeros((n, n))

    for i in range(n):
        for j in range(n):
            if i == Col:
                res[i][j] = l[i] * RevA[i][j]
            else:
                res[i][j] = RevA[i][j] + l[i] * RevA[Col][j]

    return res


def main_phase_simplex_method(c, A, b, basis, x):
    m, n = A.shape

    # 1. Строим базисную матрицу AB и находим ее обратную матрицу A_invB;

    AB = A[:, basis]
    A_invB = np.linalg.inv(AB)

    # 2. Формируем вектор cB — вектор компонент вектора c, чьи индексы принадлежат множеству B;

    cB = c[basis]

    # 3. Находим вектор потенциалов u⊺ = c⊺BA_invB;

    u = cB @ A_invB

    while True:
        # 4. Находим вектор оценок ∆⊺ = u⊺A − c⊺;

        Delta = u @ A - c

        # 5. Проверяем условие оптимальности текущего плана x

        if np.all(Delta >= 0):
            return x, basis

        # 6. Находим в векторе оценок ∆ первую отрицательную компоненту и ее индекс сохраним в переменной j0;

        j0 = np.argmin(Delta)

        # 7. Вычисляем вектор z = A_invB @ Aj0;

        Aj0 = A[:, j0]
        z = A_invB @ Aj0

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

        # 13. Обновляем матрицу A_invB с помощью Sherman-Morrison формулы((A + uv^T)^-1);

        A_invB = lab1(A_invB, A[:, j0], s)

        # 2. Формируем вектор cB — вектор компонент вектора c, чьи индексы принадлежат множеству B;

        cB = c[basis]

        # 3. Находим вектор потенциалов u⊺ = c⊺BA_invB;

        u = cB @ A_invB


def first_simplex_phase(c, A, b):

    # if b < 0 => * -1 
    # Step 1:
    neg_b_indices = np.where(b < 0)[0]
    if neg_b_indices.size > 0:
        b[neg_b_indices] *= -1
        A[neg_b_indices] *= -1


    """
    Составим вспомогательную задачу линейного программирования матрица A-tilda 
    получается из матрицы A присоединением к ней справа единичной матрицы порядка m.
    """

    # Step 2:
    m, n = A.shape
    Ae = np.hstack([A, np.eye(m)])
    ec = np.hstack([np.zeros(n), -np.ones(m)])


    """
    Построим начальный базисный допустимый план (x-tilda, B) для вспомогательной задачи
    """


    # Step 3:
    B = np.arange(n, n + m)
    xe = np.zeros(n + m)
    xe[B] = np.linalg.solve(Ae[:, B], b)

    """
    Решаем вспомогательную задачу основной фазой симплекс-метода 
    и получаем оптимальный план
    """


    # Step 4:
    xe, B = main_phase_simplex_method(ec, Ae, b, B, xe)

    tmp = xe[n:]


    """
    Проверим условия совместности: если xen+1 = xen+2 = . . . = xen+m = 0, то исходная
    задача совместна; в противном случае, задача (1) не совместна и метод завершает свою работу.
    """


    # Step 5:
    if not np.all(xe[n:] == 0):
        print("Задача несовместна")
        return


    """
    Формируем допустимый план x = xe1 xe2 . . . xen исходной задачи.
    Для него необходимо подобрать множество базисных индексов. 
    С этой целью скорректируем множество B
    """


    # Step 6:
    x = xe[:n]


    # Step 7-11:
    while True:

        if np.all(B < n):
            break

        k = np.argmax(B)
        jk = B[k]
        i = jk - n

        Ae_B = Ae[:, B]
        Ae_B_inv = np.linalg.inv(Ae_B)
        flg = 0
        for j in set(range(n)) - set(B):
            Aj = Ae[:, j]

            '''
            Для каждого индекса j ∈ {1, 2, . . . , n} вычисляем вектор ℓ(j)
            '''


            #Step 7
            lj = Ae_B_inv @ Aj


            """
            Если найдется индекс j ∈ {1, 2, . . . , n} \ B такой, что (ℓ(j))k̸ != 0, 
            то заменим в наборе B значение jk , равное n + i, на j.
            """
            #Step 8
            if lj[k] != 0:
                B[k] = j
                flg = 1
                break

        '''
        Если для любого индекса j ∈ {1, 2, . . . , n} выполняется ℓ(j))=0,
        то i-е основное ограничение основной задачи линейно выражается через остальные
        и его необходимо удалить. В этом случае удалим i-ую строку из матрицы A и
        i-ую компоненту из вектора b. Удалим из B индекс jk = n + i. Кроме этого,
        удалим i-ую строку из матрицы А-tilda. И далее вернемся к шагу 7
        '''

        #Step 9
        if flg == 0:
            B = np.delete(B, k, axis=0)
            A = np.delete(A, i, axis=0)
            b = np.delete(b, i, axis=0)
            Ae = np.delete(Ae, i, axis=0)
            m -= 1

    print(x)
    print(B + 1)
    print(A)
    print(b + 1)


def main():

    c = np.array([1, 0, 0])
    A = np.array([[1, 1, 1], [2, 2, 2]])
    b = np.array([0, 0])

    first_simplex_phase(c, A, b)


if __name__ == "__main__":
    main()
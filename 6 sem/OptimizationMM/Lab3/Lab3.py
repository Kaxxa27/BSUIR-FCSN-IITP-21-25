import numpy as np


def inv_matrix(A, A_inv, x, i):
    A_kr = np.copy(A)
    A_kr[:, i] = x
    l = np.dot(A_inv, x)
    if l[i] == 0:
        return False
    l_v = np.copy(l)
    l_v[i] = -1
    l_kr = - l_v / l[i]
    Q = np.eye(len(x))
    Q[:, i] = l_kr
    return np.dot(Q, A_inv)


def simplex(A, c, x, Jb):
    iter_count = 0
    Jb = [i - 1 for i in Jb]
    last_inv = None

    while True:
        p = len(Jb)
        is_optimal = False
        Ab = np.zeros((p, p))
        c_b = []

        for i, j in enumerate(Jb):
            Ab[:, i] = A[:, j]
            c_b.append(c[j])

        if last_inv is not None:
            Ab_inv = inv_matrix(Ab, last_inv, A[:, j0], s)
        else:
            Ab_inv = np.linalg.inv(Ab)

        last_inv = Ab_inv

        u = np.dot(c_b, Ab_inv)

        delta = np.dot(u, A) - c

        J_notb = [i for i, j in enumerate(delta) if j < 0 and i not in Jb]

        if len(J_notb) == 0:
            is_optimal = True

        if is_optimal:
            opt_pl = list(map(lambda _x: round(float(_x), 3), list(x)))
            max_pr = round(float(np.dot(c, x)), 4)

            print(opt_pl, "- оптимальный план")
            print("Максимальная прибыль : ", max_pr)
            print("Количество итераций : ", iter_count)

            return opt_pl, Jb, max_pr

        j0 = J_notb[0]
        z = np.dot(Ab_inv, A[:, j0])

        theta = []
        for i in range(len(z)):
            if z[i] > 0:
                theta.append(x[Jb[i]] / z[i])
            else:
                theta.append(np.inf)

        min_theta = np.min(theta)

        if min_theta == np.inf:
            print("Целевая функция неограничена сверху!")
            return "Целевая функция неограничена сверху!"

        s = theta.index(min_theta)
        Jb[s] = j0
        x[j0] = min_theta

        # print("Jb", Jb)
        for i in range(len(x)):
            if i not in Jb:
                x[i] = 0

        for i, j in enumerate(Jb):
            if i != s:
                x[j] = x[j] - z[i] * min_theta

        iter_count += 1

def start_phase(A, b):
    m, n = A.shape
    Jb = [n + i for i in range(m)]

    for i in range(m):
        if b[i] < 0:
            b[i] *= -1
            for j in range(n):
                A[i][j] *= -1

    x = [0] * n + b
    c = [0] * n + [-1] * m

    # print(*A)
    # print(x)

    A2 = np.hstack((A, np.eye(m)))
    print("A2: ", A2)
    print("Jb: ", Jb)
    print("c:", c)
    print("x:", x)

    print('=====')
    opt_pl, Jb_ans, max_pr = simplex(A2, c, x, Jb)
    print('=====')

    for j in range(m):
        if x[n + j] != 0:
            print("Задача несовместна!")
            return

    print(Jb_ans, " - базисный план симплекс метода")
    while True:
        i = -1
        bad_j = -1
        Jb_len = len(Jb_ans)
        for j in range(Jb_len):
            if Jb_ans[j] > n - 1:
                i = Jb_ans[j] - n + 1
                bad_j = j
                break

        if i == -1:
            print("A: ", A2)
            print("Задача совместна")
            print(x[: m], " - оптимальный план")
            print([i + 1 for i in Jb_ans], " - базисный план")
            return

        Ab = np.zeros((Jb_len, Jb_len))
        for k in range(Jb_len):
            Ab[:, k] = A2[:, Jb_ans[k]]

        is_end = False
        for j in range(n):
            if j not in Jb_ans:
                if np.linalg.det(Ab) == 0:
                    break
                l_j = np.dot(np.linalg.inv(Ab), A2[:, j])
                print("l_j: ", l_j)
                if l_j[bad_j] != 0:
                    Jb_ans[bad_j] = j
                    is_end = True
                    break

        if is_end:
            continue
        del Jb_ans[bad_j]
        A2 = np.delete(A2, bad_j, axis=0)


def test1():
    A = np.array([
        [1, 1, 1],
        [2, 2, 2],
        [3, 3, 3]])
    b = [0, 0, 0]

    start_phase(A, b)


def test2():
    A = np.array([
        [0.0, 1.0, 4.0, 1.0, 0.0, -8.0, 1.0, 5.0],
        [0.0, -1.0, 0.0, -1.0, 0.0, 0.0, 0.0, 0.0],
        [0.0, 2.0, -1.0, 0.0, -1.0, 3.0, -1.0, 0.0],
        [1.0, 1.0, 1.0, 1.0, 0.0, 3.0, 1.0, 1.0]
    ])
    b = [36.0, -11.0, 10.0, 20.0]

    start_phase(A, b)


def test3():
    A = np.array([
        [1, 1, 1],
        [2, 2, 2]
    ])
    b = [0, 0]

    start_phase(A, b)


def main():
    test1()
    input()
    test2()
    input()
    test3()


if __name__ == '__main__':
    main()

import numpy as np
from copy import deepcopy

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


def main():
    n = int(input("Enter the matrix size: "))

    values_A = []
    for i in range(n):
        print(f"Enter the values({n}) of row {i+1} for matrix A:")
        values_A.append(list(map(int, input().split(" "))))

    A = np.matrix(values_A)
    A_inv = np.linalg.inv(A)
    print(f"A:\n {A}")
    print(f"A^-1:\n {A_inv}")

    values_x = []

    for i in range(n):
        print(f"Enter the values({n}) of row {i+1} for vector x:")
        values_x.append(list(map(int, input().split(" "))))

    x = np.matrix(values_x)
    print(f"x:\n {x}")

    i = int(input(f"Enter the number of column that will be change( 1 <= i <= {n}): "))

    # change the i column with vector X
    A[:, i - 1] = x
    print(f"A(after swap):\n {A}")

    # try to find L
    l = np.dot(A_inv, x)
    print(f"l: {l}")

    # check Li == 0 ?
    if l[i - 1, 0] == 0:
        print("The matrix is irreversible")
    else:
        ℓe = deepcopy(l)

        # change Li in L with -1
        ℓe[i - 1, 0] = -1
        print(f"ℓe: {ℓe}")

        # find ^l
        ℓb = -1/(l[i - 1, 0]) * ℓe
        print(f"ℓb: {ℓb}")

        Q = np.matrix([[1, 0, 0], [0, 1, 0], [0, 0, 1]])
        print(f'Q: {Q}')

        # change i column with ^l
        Q[:, i - 1] = ℓb
        print(f'Q2: {Q}')

        # find (A)^-1
        res = np.dot(Q, A_inv)
        print(f"res: {res}")


if __name__ == '__main__':
    main()

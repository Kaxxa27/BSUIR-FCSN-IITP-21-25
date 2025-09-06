import numpy as np



def main():
    n = int(input("Порядок матрицы: "))
    if n <= 0:
        print("Некорректный ввод")
        return
    
    print("Введите матрицу A")
    A = np.zeros((n, n))

    for i in range(n):
        for j in range(n):
            A[i, j] = float(input(f"A[{i}][{j}]: "))

    if np.linalg.det(A) == 0:
        print("Для введенной матрицы не существует обратной")
        return
    
    A_inv = np.linalg.inv(A)

    print("Введите вектор-столбец x")
    x = np.zeros((n, 1))
    for i in range(n):
        x[i, 0] = float(input(f"x[{i}]: "))

    i = int(input("i = "))
    print('\n')
    
    if not (1 <= i <= n):
        print("Некорректный ввод")
        return
    
    A_bar = A.copy()
    A_bar[:, i - 1] = x[:, 0]

    print("Исходная матрица A")
    print(A)
    print('\n')

    print("Обратная матрица A^-1")
    print(A_inv)
    print('\n')

    print("Вектор-столбец x")
    print(x)
    print('\n')

    print(f"i = {i}")
    print('\n')
    
    print("Матрица A_bar")
    print(A_bar)
    print('\n')

    # step 1
    l = A_inv @ x
    if l[i - 1][0] == 0:
        print("Матрица A_bar необратима")
        return
    
    print("l")
    print(l)
    print("\n")

    # step 2
    l_tilda = l.copy()
    l_tilda[i - 1][0] = -1

    print("l_tilda")
    print(l_tilda)
    print("\n")

    # step 3
    l_cap = (-1 / l[i - 1, 0]) * l_tilda

    print("l_cap")
    print(l_cap)
    print("\n")

    # step 4
    Q = np.eye(n)
    Q[:, i - 1] = l_cap[:, 0]

    print("Q")
    print(Q)
    print("\n")

    # step 5
    A_bar_inv = Q @ A_inv

    print("A_bar_inv")
    print(A_bar_inv)
    print('\n')
    

    print("A_bar_inv полученная для проверки")
    A_bar_inv_test = np.linalg.inv(A_bar)
    print(A_bar_inv_test)
    print('\n')

    print(np.array_equal(A_bar_inv, A_bar_inv_test))

if __name__ == '__main__':
    main()
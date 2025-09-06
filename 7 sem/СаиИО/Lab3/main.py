from copy import deepcopy as dc
import numpy as np
from colorama import Fore, Style


def read_data(num='1') -> tuple:
    file = open('input'+num+'.txt', 'r')
    file.readline()
    A = []
    for line in file:
        A.append([float(i) for i in line.split()])
    p, Q = len(A), len(A[0]) - 1
    return p, Q, A


def get_OPT(p, Q, A) -> tuple:
    B = [dc(A[0])]
    C = [[i for i in range(Q+1)]]
    for k in range(1, p):
        B.append([])
        C.append([])
        for q in range(Q+1):
            cur_max, cur_x = - np.inf, None
            for xk in range(0, q+1):
                if cur_max < B[k-1][q-xk] + A[k][xk]:
                    cur_x = xk
                cur_max = max(cur_max, B[k-1][q-xk] + A[k][xk])
            B[-1].append(cur_max)
            C[-1].append(cur_x)
    return B, C


def print_opt(B, C) -> None:
    print(Fore.MAGENTA + 'B(k, q):' + Style.RESET_ALL)
    print(str(np.array(B)) + "\n")
    print(Fore.MAGENTA + "C:" + Style.RESET_ALL)
    print(str(np.array(C)))


def get_x_values(p, Q, C) -> list:
    answer, x_sum = [C[-1][-1]], C[-1][-1]
    for k in range(p-2, -1, -1):
        answer.append(C[k][Q-x_sum])
        x_sum += answer[-1]
    return answer


def print_answer(C, profit) -> None:
    print("\n" + Fore.GREEN + "Максимальная прибыль =\t" + Style.RESET_ALL + str(profit))
    print(Fore.GREEN + "Оптимальный план C =\t" + Style.RESET_ALL + str(C[::-1]))


if __name__ == '__main__':
    p, Q, A = read_data()
    B, C = get_OPT(p, Q, A)
    print_opt(B, C)
    answer = get_x_values(p, Q, C)
    print_answer(answer, B[-1][-1])
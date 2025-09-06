import numpy as np
from sympy import *

from sturms_row import *
from nonlinear_methods import *

import task
import tests


def separator():
    for i in range(2):
        print('-' * 50)


def main():

    y = task.y

    interval = tests.interval

    intervals = get_1root_intervals(get_sturm_row(y), interval)
    print(f"\nIntervals with 1 root: {[x for x in intervals]}\n")

    for inter in intervals:
        print(f'\n\t\tCurrent interval is: {inter}')

        separator()
        bisect_ans = bisection(y, inter)
        print(f'**** Solving with Bisection method ****\n'
              f'ROOT: {bisect_ans[0]}\n'
              f'Iteration: {bisect_ans[1]}')

        separator()
        secant_ans = secant(y, inter)
        print(f'**** Solving with Secant method ****\n'
              f'ROOT: {secant_ans[0]}\n'
              f'Iteration: {secant_ans[1]}')

        separator()
        newton_ans = newton(y, inter)
        print(f'**** Solving with Newton method ****\n'
              f'ROOT: {newton_ans[0]}\n'
              f'Iteration: {newton_ans[1]}')

    separator()
    correct_root = np.roots([1, task.a, task.b, task.c])
    print(f'**** Solving with Nampy ****\n'
          f'ROOTS: {correct_root}\n')


if __name__ == '__main__':
    main()

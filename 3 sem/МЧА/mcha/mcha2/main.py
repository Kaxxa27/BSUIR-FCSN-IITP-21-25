from tools_func import *
import task
import test
from iteration_method import iteration_method
from seidel_method import seidel_method

A = task.A
b = task.b
e = test.E

print('Default matrix')
print_equation(A, b)

# Real exactly solution
x_real = np.linalg.solve(A, b)
print(f'Real Accurate solution *X*\n{x_real}\n')


# Iteration method
print('-' * 17 * x_real.shape[0])
x_iteration = iteration_method(A, b, e, 1)

print('-' * 17 * x_real.shape[0])
print(f'*** Iteration method result ***\nX = {x_iteration[0].reshape((1, x_iteration[0].shape[0]))}\n '
      f'Done in {x_iteration[1]} iterations')

# Gauss - seidel method
print('-' * 17 * x_real.shape[0])
x_seidel = seidel_method(A, b, e, 1)

print('-' * 17 * x_real.shape[0])
print(f'*** Seidel method result ***\nX = {x_seidel[0].reshape((1, x_seidel[0].shape[0]))}\n'
      f'Done in {x_seidel[1]} iterations')

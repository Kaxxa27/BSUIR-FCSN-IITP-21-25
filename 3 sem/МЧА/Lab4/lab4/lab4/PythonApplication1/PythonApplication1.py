# -*- coding: cp1251 -*-

import numpy as np

n = 4
A = 2 * np.eye(n) - np.eye(n, k=1) - np.eye(n, k=-1)

def Jacobi_Rotation(A,j,k):
    J = np.eye(n)
    if A[j,j] == A[k,k]:
        theta = np.pi / 4
        J[j, j], J[j, k], J[k, j], J[k, k] = np.cos(theta), -np.sin(theta), np.sin(theta), np.cos(theta)   
    else:       
        tau = (A[j,j] - A[k,k]) / (2 * A[j,k]) # ctg(2a)
        tg = np.sign(tau) / (abs(tau) + np.sqrt(1 + tau**2)) # tg(a)
        cos = 1 / np.sqrt(1 + tg**2) # cos(a)
        sin = cos * tg # sin(a)
        J[j, j], J[j, k], J[k, j], J[k, k] = cos, -sin, sin, cos
    S = np.dot(np.transpose(J), np.dot(A,J))
    return S

def Jacobi(A):
    while True:
        for j in range(0,n-1):
            for k in range(j+1,n):
                A = Jacobi_Rotation(A,j,k) # A_{i+1}
                if (np.linalg.norm(A - np.diag(np.diag(A))) < 10**-2):
                    return A
print("Вещественная симметричная матрица A, для которой мы хотим найти собственные числа и векторы:")                
print(A, "\n")  
print("Вычисленная диагональная матрица:")
print(Jacobi(A), "\n")
print("Собственные числа вычисленные с помощью метода Якоби:")
print(np.diag(Jacobi(A)), "\n")
print("Собственные числа вычисленные с помощью numpy:")
print(np.linalg.eigvals(A))


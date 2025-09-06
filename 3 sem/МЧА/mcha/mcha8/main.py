import numpy as np
import math
from diff_calculation import diff_first_via_estimation, diff_second_via_estimation, diff_first, diff_second
from int_calculation import integral_middle_rectangle_via_estimation, integral_trapezoid_via_estimation, integral_simpson_via_estimation
print("Numerical Differentiation and Integration \n")

np.random.seed(42)

L, R, dof_x_diff = 0, 1.5, 0.75
def f(x): return (math.tan(x))**0.5
def F(x): return 2**0.5 * math.atan((math.tan(x)-1)/(2*math.tan(x))**0.5)/2 + 2**0.5*math.log(abs((math.tan(x)+1-(2*math.tan(x))**0.5)/(math.tan(x)+1+(2*math.tan(x))**0.5)), np.exp(x))/4
def fd1(x): return 1/(2*(math.tan(x))**0.5*(math.cos(x)**2))
def fd2(x): return (-((math.tan(x))**2+1)/(math.tan(x))**1.5 +4*(math.tan(x))**0.5)*((((math.tan(x))**2))/4 + 1/4)
def fd3(x): return ((((math.tan(x))**2))/8 + 1/8)*((3*((math.tan(x))**2 + 1)**2/(math.tan(x))**2.5) - 4*(((math.tan(x))**2 + 1)**2/(math.tan(x))**0.5) + 16*(math.tan(x))**1.5)
def fd4(x): return (4*math.sin(x)**6+math.sin(x)**4+5/2*math.sin(x)**2-15/16)/(math.tan(x)**3.5 * math.cos(x)**8)
M2deLR, M4deLR = 0, 0

IntFormatString = "{:.7f}"

DerFormatString = "{:.4f}"

print()


def delta(derappr, eps=0.01):
    return np.ceil(abs(derappr - fd1(dof_x_diff)) * (1 / (eps / 10))) * (eps / 10)


print("First Derivative = " + DerFormatString.format(fd1(dof_x_diff)))
print("ViaEstimation    = " + DerFormatString.format(diff_first_via_estimation(f, dof_x_diff, fd2, fd3)), \
      " | delta = " + DerFormatString.format(delta(diff_first_via_estimation(f, dof_x_diff, fd2, fd3))))
print("ViaTenInMinus5   = " + DerFormatString.format(diff_first(f, dof_x_diff)), \
      " | delta = " + DerFormatString.format(delta(diff_first(f, dof_x_diff))))
print()


def delta(derappr, eps=0.01):
    return abs(derappr - fd1(dof_x_diff))


print("Second Derivative = " + DerFormatString.format(fd2(dof_x_diff)))
print("ViaEstimation     = " + DerFormatString.format(diff_second_via_estimation(f, dof_x_diff, fd4)), \
      " | delta = " + DerFormatString.format(delta(diff_second_via_estimation(f, dof_x_diff, fd4))))
print("ViaTenInMinus5    = " + DerFormatString.format(diff_second(f, dof_x_diff)), \
      " | delta = " + DerFormatString.format(delta(diff_second(f, dof_x_diff))))
print()





print()
int_precised = 1.6893633
print("Integral =            " + IntFormatString.format(int_precised))


def delta(intappr, eps=0.000001):
    return np.ceil(abs(intappr - int_precised) * (1 / (eps / 10))) * (eps / 10)


int_appr = integral_middle_rectangle_via_estimation(f, L, R)
print(("ViaMiddleRectangles = " + IntFormatString + " | delta = " + IntFormatString).format(int_appr, delta(int_appr)))
int_appr = integral_trapezoid_via_estimation(f, L, R)
print(("ViaTrapezoids       = " + IntFormatString + " | delta = " + IntFormatString).format(int_appr, delta(int_appr)))
int_appr = integral_simpson_via_estimation(f, L, R)
print(("ViaSimpson          = " + IntFormatString + " | delta = " + IntFormatString).format(int_appr, delta(int_appr)))
print()

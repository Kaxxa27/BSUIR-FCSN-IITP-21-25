import numpy as np
import matplotlib.pyplot as plt
from cubic_spline import input_func, spline_built


print("Cubic spline interpolation \n")
plot_dots = 10 ** 4

dots, f = input_func()
(x, y) = map(list, zip(*dots))
print("(x,y) =", dots, '\n')

plt.plot(x, y, 'og')
x_plot = np.linspace(min(x), max(x), plot_dots)

y_plot = [f(xdot) for xdot in x_plot]
plt.plot(x_plot, y_plot)

spl, output = spline_built(dots)
y_plot = [spl(xdot) for xdot in x_plot]
plt.plot(x_plot, y_plot)

output()

xdot = 1.0
print(f"          f({xdot}) =", f(xdot))
print(f"Cubic Spline({xdot}) =", spl(xdot))
print(f"      delta({xdot}) =", abs(f(xdot) - spl(xdot)))

plt.show()

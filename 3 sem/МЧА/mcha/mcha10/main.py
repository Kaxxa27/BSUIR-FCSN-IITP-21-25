from matplotlib import pyplot
from adams import evaluate
from test import a, b, Y0, eps


h = 1
h, T, Y_Adams3, Y_exact, error = evaluate(a, b, h, Y0, eps)
pyplot.title('Green - exact; Blue - Adams3')
pyplot.plot(T, Y_exact, 'g--')
pyplot.plot(T, Y_Adams3, 'b')
pyplot.show()

print('H:', h)
print('Max error:', max(error))
print('\t\tValues')
print('T\t\tExact\tADAMS3\tDiff')
for it in range(0, len(T)):
    print(f'{T[it]:.3f}\t{Y_exact[it]:.3f}\t{Y_Adams3[it]:.3f}\t{error[it]}')

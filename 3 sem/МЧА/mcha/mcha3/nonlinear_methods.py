from sympy import symbols, lambdify


def bisection(function, interval: tuple, tol=0.0001):
    x, y = symbols('x y')
    a, b = interval[0], interval[1]
    mid = (a + b) / 2

    f = lambdify(x, function, "numpy")

    if f(a) * f(b) > 0:
        raise Exception(f"Bisection method couldn't solve f(x) = 0, because f(a) * f(b) = {f(a) * f(b)} >= 0\n"
                        f"--> more than 1 root on ({(a, b)}) or no roots)")
    x_delta = tol * 2
    iteration = 1
    while f(mid) > tol or x_delta > tol:
        if f(a) == 0:
            return a, iteration
        elif f(b) == 0:
            return b, iteration
        elif f(mid) == 0:
            return mid, iteration

        if f(a) * f(mid) < 0:
            b = mid
        elif f(b) * f(mid) < 0:
            a = mid

        x_delta = mid
        mid = (a + b) / 2

        x_delta = abs(x_delta - mid)
        iteration += 1

    return mid, iteration


def secant(function, interval: tuple, tol=0.0001):
    x, y = symbols('x y')
    a, b = interval[0], interval[1]
    f = lambdify(x, function, "numpy")
    if f(a) * f(b) > 0:
        raise Exception(f"Secant method couldn't solve f(x) = 0, because f(a) * f(b) = {f(a) * f(b)} >= 0\n"
                        f"--> more than 1 root on ({(a, b)}) or no roots)")

    x_sec = a - (f(a) * (b - a)) / (f(b) - f(a))

    x_delta = 0
    iteration = 1
    while abs(f(x_sec)) > tol or x_delta > tol:
        if f(a) == 0:
            return a, iteration
        elif f(b) == 0:
            return b, iteration
        elif f(x_sec) == 0:
            return x_sec, iteration

        if f(x_sec) * f(a) < 0:
            b = x_sec
        elif f(x_sec) * f(b) < 0:
            a = x_sec

        x_delta = x_sec
        x_sec = a - (f(a) * (b - a)) / (f(b) - f(a))

        x_delta = abs(x_delta - x_sec)
        iteration += 1

    return x_sec, iteration


def newton(function, interval, tol=0.0001):
    x, y = symbols('x y')
    a, b = interval[0], interval[1]
    f = lambdify(x, function, 'numpy')

    if f(a) * f(b) > 0:
        raise Exception(f"Newton method couldn't solve f(x) = 0, because f(a) * f(b) = {f(a) * f(b)} >= 0\n"
                        f"--> more than 1 root on ({(a, b)}) or no roots)")

    x_start = (a + b) / 2
    y_diff = function.diff()
    f_diff = lambdify(x, y_diff, 'numpy')

    x_newton = x_start - f(x_start) / f_diff(x_start)

    x_delta = tol * 2
    iteration = 1
    while abs(f(x_newton)) > tol or x_delta > tol:
        if f(x_newton) == 0:
            return x_newton, iteration

        x_delta = x_newton
        x_newton = x_newton - f(x_newton) / f_diff(x_newton)
        x_delta = abs(x_delta - x_newton)
        iteration += 1

    return x_newton, iteration

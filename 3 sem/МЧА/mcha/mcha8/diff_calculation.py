def diff_first(f, x, d=10 ** (-5)):
    return (f(x + d) - f(x - d)) / (2 * d)


def diff_first_via_estimation(f, x, f_diff_2, f_diff_3, epsilon=0.01):
    M2 = abs(f_diff_2(x))
    df = 2 * epsilon / M2
    M3 = abs(f_diff_3(x))
    ds = (6 * epsilon / M3) ** (1 / 2)
    return diff_first(f, x, min(df, ds))


def diff_second(f, x, d=10 ** (-5)):
    return (f(x + d) - 2 * f(x) + f(x - d)) / (d ** 2)


def diff_second_via_estimation(f, x, f_diff_4, epsilon=0.01):
    M4 = abs(f_diff_4(x))
    d = (12 * epsilon / M4) ** (1 / 2)
    return diff_second(f, x, d)


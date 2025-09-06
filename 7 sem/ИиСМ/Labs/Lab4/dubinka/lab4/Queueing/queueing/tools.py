from math import exp
import matplotlib.pyplot as plt


def is_pos_float(s):
    try:
        float(s)
        return float(s) > 0
    except ValueError:
        return False


def is_pos_int(s):
    try:
        int(s)
        return int(s) > 0
    except ValueError:
        return False


def get_intervals_0_1(p):
    res = [[0, p[0]]]
    amount = p[0]
    for i in range(1, len(p)):
        interval = [amount]
        amount += p[i]
        interval.append(amount)
        res.append(interval)
    return res


def get_mx(x):
    return round(sum(x) / len(x), 2)


def get_dx(x):
    mx = get_mx(x)
    return round(sum([(x_i - mx) ** 2 for x_i in x]) / (len(x) - 1), 2)


def get_p_mx(p):
    return round(sum([(x_i + 1) * p_i for x_i, p_i in enumerate(p)]), 2)


def get_p_mx2(p):
    return round(sum([(x_i + 1)**2 * p_i for x_i, p_i in enumerate(p)]), 2)


def get_p_dx(p):
    return round(get_p_mx2(p) - get_p_mx(p)**2, 2)


def covariance(x, j):
    mx = get_mx(x)
    n = len(x)
    return sum([(x[i] - mx) * (x[i + j] - mx) for i in range(1, n - j)]) / (n - j)


def correlation_plot(x):
    dx = get_dx(x)
    res = []
    interval = [x for x in range(1, 20)]
    for i in interval:
        res.append(covariance(x, i) / dx)
    plt.title('График коэффициента корреляции')
    plt.xlabel('i')
    plt.ylabel('ρ(i)')
    plt.scatter(interval, res, c="red")
    plt.grid()
    plt.show()


def scatter_plot(x):
    plt.title('Диаграмма разброса')
    plt.xlabel('x(i)')
    plt.ylabel('x(i+1)')
    x_points = x[:len(x) - 1]
    y_points = x[1:]
    plt.scatter(x_points, y_points, c=[[1, 0, 0, 0.05]])
    plt.grid()
    plt.show()


def confidence_interval(x):
    k = 1.96 * (get_dx(x) / len(x)) ** 0.5
    mx = get_mx(x)
    return round(mx - k, 3), round(mx + k, 3)


def get_histogram_x_points(a, b, dx):
    res = []
    i = a
    while i < b:
        res.append(round(i, 3))
        i += dx
    res.append(round(b, 3))
    return res


def get_histogram_n(x, intervals):
    res = [0] * (len(intervals) - 1)
    for xi in x:
        for i in range(len(intervals) - 1):
            if intervals[i] <= round(xi, 5) < intervals[i + 1]:
                res[i] += 1
                break
            elif round(xi, 5) == intervals[-1]:
                res[-1] += 1
                break
    return res


def plot_histogram(x, lmbd, dx, intervals, n):
    plt.title("Гистограмма и график плотности вероятности\nэкспоненциального распределения")
    plt.xlabel("x")
    plt.ylabel("~h")
    plt.bar(intervals[1:], [n_i / len(x) / dx for n_i in n], -dx, align='edge')
    plt.xticks([round(x_i, 1) for x_i in intervals])
    plt.grid()
    plt.plot(intervals, [lmbd * exp(-lmbd * x_i) for x_i in intervals], color='red', zorder=1)
    plt.show()


def frequency_plot(x):
    freq = {}
    for x_i in x:
        if freq.get(x_i) is None:
            freq[x_i] = 1
        else:
            freq.update({x_i: freq.get(x_i) + 1})
    n = len(x)
    plt.title("Относительная частота")
    plt.xlabel("x")
    plt.ylabel("~h")
    for x_i, h_i in freq.items():
        plt.plot([x_i]*2, [0, h_i/n])
        plt.scatter(x_i, h_i/n)
    plt.show()

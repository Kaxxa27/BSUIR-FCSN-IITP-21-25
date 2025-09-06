def get_value_at_point(method, x, y0, yder, eps):
    n = 1
    while True:
        old_dots, new_dots = method(x, n, y0, yder), method(x, 2*n, y0, yder)
        if max(abs(new_dots[2*i] - old_dots[i]) for i in range(n+1)) < eps:
            return new_dots[-1], 2*n
        #print(max(abs(newdots[i] - ans(LEFT + i * (RIGHT - LEFT) / (2*n))) for i in range(2*n+1)))
        #if max(abs(newdots[i] - ans(LEFT + i * (RIGHT - LEFT) / (2*n))) for i in range(2*n+1)) < eps:
        #    return 2*n
        else:
            n *= 2


def create_y_dots(method, xdots, y0, yder, eps):
    ydots = []
    maxn = 0
    midn = []
    for x in xdots:
        y, n = get_value_at_point(method, x, y0, yder, eps)
        ydots.append(y)
        maxn = max(maxn, n)
        midn += [n]
    midn = sum(midn) / len(xdots)
    return ydots, midn, maxn

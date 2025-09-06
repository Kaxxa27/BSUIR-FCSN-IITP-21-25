def euler(xdot, N, y0, yder):
    ydots = [y0]
    h = xdot / N
    for i in range(N):
        x = i * h
        y = ydots[-1]
        ydots += [ y + h * yder(x, y) ]
    return ydots


def better_euler(xdot, N, y0, yder):
    ydots = [y0]
    h = xdot / N
    for i in range(N):
        x = i * h
        y = ydots[-1]
        ydots += [y + h * yder(x + h/2, y + h/2 * yder(x, y))]
    return ydots

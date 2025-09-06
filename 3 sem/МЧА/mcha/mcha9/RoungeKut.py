def runge_kut(xdot, N, y0, yder):
    ydots = [y0]
    h = xdot / N
    for i in range(N):
        x = i * h
        y = ydots[-1]
        K1 = h * yder(x, y)
        K2 = h * yder(x + h/2, y + K1/2)
        K3 = h * yder(x + h/2, y + K2/2)
        K4 = h * yder(x + h, y + K3)
        ydots += [y + 1/6 * (K1 + 2*K2 + 2*K3 + K4)]
    return ydots
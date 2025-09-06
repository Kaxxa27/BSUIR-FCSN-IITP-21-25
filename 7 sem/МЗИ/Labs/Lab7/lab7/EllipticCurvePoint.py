class EllipticCurvePoint:
    def __init__(self, x, y, a, b, p):
        self.x = x
        self.y = y
        self.a = a
        self.b = b
        self.p = p

    def __add__(self, other):
        # Операция сложения точек
        if self.x == other.x and self.y == other.y:
            return self.double()

        dy = (other.y - self.y) % self.p
        dx = (other.x - self.x) % self.p

        m = (dy * pow(dx, -1, self.p)) % self.p

        x3 = (m * m - self.x - other.x) % self.p
        y3 = (m * (self.x - x3) - self.y) % self.p

        return EllipticCurvePoint(x3, y3, self.a, self.b, self.p)

    def double(self):
        # Удвоение точки
        dy = (3 * self.x * self.x + self.a) % self.p
        dx = (2 * self.y) % self.p

        m = (dy * pow(dx, -1, self.p)) % self.p

        x2 = (m * m - 2 * self.x) % self.p
        y2 = (m * (self.x - x2) - self.y) % self.p

        return EllipticCurvePoint(x2, y2, self.a, self.b, self.p)

    def multiply(self, k):
        # Умножение точки на скаляр
        result = None
        addend = self

        while k:
            if k & 1:
                result = addend if result is None else result + addend
            addend = addend.double()
            k >>= 1

        return result

    def __str__(self):
        return f"({self.x}, {self.y})"

    def __eq__(self, other):
        return self.x == other.x and self.y == other.y and self.a == other.a and self.b == other.b and self.p == other.p

class ECPoint:
    def __init__(self, x=0, y=0, a=0, b=0, p=0, is_polynomial_basis=False):
        self.x = x
        self.y = y
        self.a = a
        self.b = b
        self.p = p
        self.pol_basis = is_polynomial_basis

    # inverse int b modulo p
    @staticmethod
    def _mod_inverse(b, p):
        x0, x1, y0, y1, n = 1, 0, 0, 1, p
        while n != 0:
            q, b, n = b // n, n, b % n
            x0, x1 = x1, x0 - q * x1
            y0, y1 = y1, y0 - q * y1
        return x0 % p

    def __add__(self, other):
        p_result = ECPoint()
        p_result.a = self.a
        p_result.b = self.b
        p_result.p = self.p

        dx = (other.x - self.x) % self.p
        dy = (other.y - self.y) % self.p
        if self.x == other.x and self.y == other.y:
            l = ((3 * self.x ** 2 + self.a) * ECPoint._mod_inverse(2 * self.y, self.p)) % self.p
        else:
            if self.x == other.x:
                return float('inf')
            dx_inverse = ECPoint._mod_inverse(dx, self.p)
            l = (dy * dx_inverse) % self.p
        p_result.x = (l * l - self.x - other.x) % self.p
        p_result.y = (l * (self.x - p_result.x) - self.y) % self.p

        return p_result

    def __rmul__(self, other):
        p_result = ECPoint(self.x, self.y, self.a, self.b, self.p, self.pol_basis)
        temp = ECPoint(self.x, self.y, self.a, self.b, self.p, self.pol_basis)
        x = other - 1
        while x != 0:
            if x % 2 != 0:
                p_result += temp
                x -= 1
            x //= 2
            temp = temp + temp
        return p_result
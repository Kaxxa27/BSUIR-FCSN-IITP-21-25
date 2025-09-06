import random
from ec import ECPoint


class DSgost:
    def __init__(self, p, a, b, q, p_x, p_y):
        self.p_point = ECPoint(p_x, p_y, a, b, p)
        self.q = q
        self.a = a
        self.b = b
        self.p = p

    def gen_keys(self):
        d = 55441196065363246126355624130324183196576709222340016572108097750006097525544
        q_point = d * self.p_point
        return d, q_point

    def encrypt(self, message, private_key):
        e = message % self.q
        print(f"e = {e}")
        k = random.randint(1, self.q - 1)
        r, s = 0, 0
        while r == 0 or s == 0:
            c_point = k * self.p_point
            r = c_point.x % self.q
            s = (r * private_key + k * e) % self.q
        concatenated_rs = str(r) + str(s)
        return r, s, concatenated_rs

    def verify(self, message, encrypt, public_key):
        if not (0 < encrypt[0] < self.q) or not (0 < encrypt[0] < self.q):
            return False
        e = message % self.q
        if e == 0:
            e = 1
        nu = ECPoint._mod_inverse(e, self.q)
        z1 = (encrypt[1] * nu) % self.q
        z2 = (-encrypt[0] * nu) % self.q
        c_point = z1 * self.p_point + z2 * public_key
        r = c_point.x % self.q
        if r == encrypt[0]:
            return True
        return False
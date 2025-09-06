import math
from .tools import get_intervals_0_1


class Generator:
    a = 630360016
    m = 2147483647

    def __init__(self):
        self.xi = self.m - 10

    def generate_n_0_1(self, n):
        self.next_xi()
        xi = self.xi
        res = []
        for _ in range(n):
            res.append(xi / self.m)
            xi = (self.a * xi) % self.m
        return res

    def generate_exp(self, mu, n, natural=False):
        res = []
        for gamma in self.generate_n_0_1(n):
            number = -mu * math.log(gamma)
            if natural:
                number = round(number)
                if number < 1:
                    number = 1
            res.append(number)
        return res

    def generate_discrete(self, p, n):
        intervals = get_intervals_0_1(p)
        res = []
        for gamma in self.generate_n_0_1(n):
            for i, interval in enumerate(intervals):
                if interval[0] < gamma < interval[1]:
                    res.append(i + 1)
                    break
        return res

    def next_xi(self):
        self.xi -= 1
        if self.xi <= 10:
            self.xi = self.m - 10

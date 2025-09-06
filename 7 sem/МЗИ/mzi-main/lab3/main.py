import random


def modulo(a, b):
    if a >= 0:
        return a % b
    else:
        return (b - abs(a % b)) % b


def generate_key(bits):
    def is_prime(n):
        if n <= 1:
            return False
        for i in range(2, int(n ** 0.5) + 1):
            if n % i == 0:
                return False
        return True

    def generate_prime(bits):
        while True:
            num = random.getrandbits(bits)
            if num % 4 == 3 and is_prime(num):
                return num

    p = generate_prime(bits)
    q = generate_prime(bits)
    open_key = p * q
    close_key = (p, q)

    return open_key, close_key


def number_to_text(numbers):
    result = []
    for item in numbers:
        for i in range(0, len(item), 4):
            number = item[i:i + 4]
            while number[0] == '0':
                number = number[1:]
            result.append(chr(int(number)))
    return result


def extended_gcd(a, b):
    if a == 0:
        return (0, 1)
    else:
        x, y = extended_gcd(b % a, a)
        return (y - (b // a) * x, x)


def find_Yp_Yq(p, q):
    x, y = extended_gcd(p, q)

    if x < 0:
        x += q

    Yp = x
    Yq = (1 - Yp * p) // q

    return Yp, Yq


def encrypted(text, open_key):
    number = ord(text)
    c = (number ** 2) % open_key

    return c


def mod(k, b, m):
    i = 0
    a = 1
    v = []
    while k > 0:
        v.append(k % 2)
        k = (k - v[i]) // 2
        i += 1
    for j in range(i):
        if v[j] == 1:
            a = (a * b) % m
            b = (b * b) % m
        else:
            b = (b * b) % m
    return a


def decrypted(c, open_key, close_key):
    p = close_key[0]
    q = close_key[1]

    #      алгоритм Евклида
    x, y = find_Yp_Yq(*close_key)
    while x * p + y * q != 1:
        x, y = find_Yp_Yq(*close_key)

    #  китайская теорема об остатках
    r = mod((p+1)/4, c, p)
    s = mod((q+1)/4, c, q)
    r1 = (x*p*s + y*q*r) % open_key
    r2 = (open_key - r1)
    r3 = (x * p * s - y * q * r) % open_key
    r4 = (open_key - r3)
    # print(r1, r2, r3, r4)
    for item in (r1, r2, r3, r4):
        if item <= 1200:
            return chr(item)


with open("input.txt", "r", encoding='utf-8') as f:
    text = f.read()

open_key, close_key = generate_key(42)

while close_key[0] == close_key[1]:
    open_key, close_key = generate_key(42)

for item in text:
    encrypted_text = encrypted(item, open_key)
    with open("encrypted.txt", "a", encoding='utf-8') as f:
        f.write(str(encrypted_text))
    decrypted_text = decrypted(encrypted_text, open_key, close_key)
    with open("decrypted.txt", "a", encoding='utf-8') as f:
        f.write(decrypted_text)

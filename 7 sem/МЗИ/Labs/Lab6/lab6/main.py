import random
from gost3411 import GOST3411
from math import isqrt
from pathlib import Path


class Gost3410Signature:
    @staticmethod
    def generate_prime_q():
        # Генерация 256-битного случайного числа и приведение его к форме, подходящей для простого числа q
        q = random.getrandbits(256)
        q = q % (2 ** 256) + 2 ** 254
        return q

    @staticmethod
    def is_prime(n):
        if n <= 1:
            return False
        for i in range(2, isqrt(n) + 1):
            if n % i == 0:
                return False
        return True

    @staticmethod
    def generate_p(q):
        # Генерация числа p на основе q
        return 2 * q - 2

    @staticmethod
    def generate_g(p, q):
        # Поиск генератора g
        for g in range(1, p):
            if pow(g, q, p) == 1:
                return g
        return -1

    @staticmethod
    def hash_message(message):
        # Хеширование сообщения с помощью алгоритма ГОСТ
        message_bytes = message.encode('utf-8')
        hash_gost = GOST3411.hash(message_bytes)
        # Преобразуем hash_gost (list) в bytes
        return bytes(hash_gost)
    @staticmethod
    def generate_signature(message, p, q, g, x):
        k = Gost3410Signature.generate_random_k(q)
        r = pow(g, k, p) % q
        h = int.from_bytes(Gost3410Signature.hash_message(message), byteorder='big')
        s = (k * h + x * r) % q
        return r, s

    @staticmethod
    def verify_signature(message, p, q, g, y, r, s):
        if r >= q or s >= q:
            return False
        h = int.from_bytes(Gost3410Signature.hash_message(message), byteorder='big')
        w = h % q
        u1 = (w * s) % q
        u2 = ((q - r) * w) % q
        v = (pow(g, u1, p) * pow(y, u2, p)) % p % q
        return v == r #подпись корректна, если v=r

    @staticmethod
    def generate_random_k(q):
        # Генерация случайного числа k, меньшего q
        k = random.randint(1, q - 1)
        return k

    @staticmethod
    def main():
        q = Gost3410Signature.generate_prime_q()
        p = Gost3410Signature.generate_p(q)
        g = Gost3410Signature.generate_g(p, q)
        x = 43
        y = pow(g, x, p)

        # Пути к файлам
        message_file_path = Path("input.txt")
        hash_file_path = Path("hash.txt")
        signature_file_path = Path("signature.txt")

        message = message_file_path.read_text(encoding='utf-8')

        # Генерация хеша сообщения и запись в файл
        hash_value = Gost3410Signature.hash_message(message).hex()
        hash_file_path.write_text(hash_value, encoding='utf-8')

        # Генерация подписи и запись в файл
        r, s = Gost3410Signature.generate_signature(message, p, q, g, x)
        signature = f"{r:X}{s:X}"
        signature_file_path.write_text(signature, encoding='utf-8')
        print(f"Generated signature: {signature}")

        # Проверка подписи
        is_valid = Gost3410Signature.verify_signature(message, p, q, g, y, r, s)
        print(f"Signature valid: {is_valid}")


if __name__ == "__main__":
    Gost3410Signature.main()
from EllipticCurvePoint import EllipticCurvePoint
from Crypto.Random import random
from Crypto.Util.number import long_to_bytes, bytes_to_long


class ElGamal:
    @staticmethod
    def generate_random_big_integer(N):
        # Генерация случайного числа, меньшего N
        bytes_len = (N.bit_length() + 7) // 8
        while True:
            r = random.getrandbits(bytes_len * 8) % N
            if r < N:
                return r

    @staticmethod
    def get_point_from_bytes(message_bytes, P):
        # Преобразование байтового сообщения в точку эллиптической кривой
        p_length = (P.p.bit_length() + 7) // 8
        if len(message_bytes) >= p_length - 2:
            raise Exception(f"M({len(message_bytes)}) should be less than p (Max M Length = {p_length - 2} symbols)")

        # Дополнение сообщения байтами для преобразования в координату x
        message = message_bytes + bytes([0xff]) + b'\x00' * (p_length - len(message_bytes) - 1)
        return EllipticCurvePoint(
            x=bytes_to_long(message),
            y=0,
            a=P.a,
            b=P.b,
            p=P.p
        )

    @staticmethod
    def get_bytes_from_point(P):
        # Извлечение сообщения из координаты x точки эллиптической кривой
        message_bytes = long_to_bytes(P.x)
        if 0xff in message_bytes:
            return message_bytes[:message_bytes.index(0xff)]
        return message_bytes

    @staticmethod
    def encrypt(message_bytes, P, Q):
        M = ElGamal.get_point_from_bytes(message_bytes, P)
        k = ElGamal.generate_random_big_integer(P.p)
        C1 = P.multiply(k)
        C2 = M + Q.multiply(k)
        return C1, C2

    @staticmethod
    def decrypt(CValues, d):
        temp = CValues[0].multiply(d)
        temp.y = -temp.y % temp.p
        P = temp + CValues[1]
        return ElGamal.get_bytes_from_point(P)

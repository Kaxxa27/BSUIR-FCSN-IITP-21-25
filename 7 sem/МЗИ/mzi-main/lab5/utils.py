from codecs import getdecoder
from codecs import getencoder


def strxor(a, b):
    """ XOR of two strings """
    mlen = min(len(a), len(b))
    a, b, xor = bytearray(a), bytearray(b), bytearray(mlen)
    for i in range(mlen):
        xor[i] = a[i] ^ b[i]
    return bytes(xor)


_hexdecoder = getdecoder("hex")
_hexencoder = getencoder("hex")


def hexdec(data):
    return _hexdecoder(data)[0]


def hexenc(data):
    return _hexencoder(data)[0].decode("ascii")

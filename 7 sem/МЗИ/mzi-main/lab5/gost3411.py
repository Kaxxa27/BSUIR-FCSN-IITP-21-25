from gost89 import GOST_28147


def toLittle(K) -> object:
    res = ''
    for i in range(len(K) // 8):
        res += K[len(K) - 8:len(K)]
        K = K[0:len(K) - 8]
    return res


def before_coding(text):
    if type(text) == str:
        text = text.encode("utf-8")
    else:
        text = text
    b = hex(int.from_bytes(text, "big"))[2:]
    res1 = ''
    for i in range(len(b)):
        res1+=bin(int(b[i], 16))[2:].zfill(4)
    return res1


def transA(block):
    y = []
    for i in range(4):
        y.append(block[:64])
        block = block[64:]
    return y[1] + y[2] + y[3] + bin(int(y[0], 2) ^ int(y[1], 2))[2:].zfill(64)


def transP(block):
    fi = [0 for i in range(32)]
    for i in range(4):
        for k in range(1, 9):
            fi[i + 1 + 4*(k-1) - 1] = 8*i + k - 1
    Y = []
    for i in range(len(block)//8):
        Y.append(block[:8])
        block = block[8:]
    res = ''
    for i in range(len(Y)):
        res+=Y[fi[i]] #Y[fi[len(Y)-i-1]]
    return res


def genKeys(Hin, m):
    C = [0, 0x00ff00ff00ff00ffff00ff00ff00ff0000ffff00ff0000ffff000000ffff00ff, 0]
    U = Hin
    V = m
    W = bin(int(U, 2) ^ int(V, 2))[2:].zfill(256)
    K = []
    K.append(transP(W))
    for i in range(1, 4):
        U = bin(int(transA(U), 2) ^ C[i-1])[2:].zfill(256)
        V = transA(transA(V))
        W = bin(int(U, 2) ^ int(V, 2))[2:].zfill(256)
        K.append(transP(W))
    K1 = K[0]
    res = ''
    for i in range(len(K1)//4):
        res+=hex(int(K1[:4], 2))[2:][:1]
        K1 = K1[4:]
    return K


def transShifr(Hin, K):
    h = []
    for i in range(len(Hin)//64):
        h.append(Hin[:64])
        Hin = Hin[64:]
    S = ''
    for i in range(4):
        S+=GOST_28147(K[i], h[i], 1)
    K1 = S
    res = ''
    for i in range(len(K1)//4):
        res+=hex(int(K1[:4], 2))[2:][:1]
        K1 = K1[4:]
    return S


def psi(block):
    Y = []
    for i in range(len(block) // 16):
        Y.append(block[:16])
        block = block[16:]
    tmp = bin(int(Y[0], 2) ^ int(Y[1], 2))[2:].zfill(16)
    tmp = bin(int(tmp, 2) ^ int(Y[2], 2))[2:].zfill(16)
    tmp = bin(int(tmp, 2) ^ int(Y[3], 2))[2:].zfill(16)
    tmp = bin(int(tmp, 2) ^ int(Y[12], 2))[2:].zfill(16)
    tmp = bin(int(tmp, 2) ^ int(Y[15], 2))[2:].zfill(16)
    res = ''
    for i in range(1, 16, 1):
        res+=Y[i]
    res = res + tmp
    return res


def transShuffle(Hin, S, m):
    Hout = S
    for i in range(12):
        Hout = psi(Hout)
    Hout = bin(int(Hout, 2) ^ int(m, 2))[2:].zfill(256)
    Hout = psi(Hout)
    Hout = bin(int(Hin, 2) ^ int(Hout, 2))[2:].zfill(256)
    for i in range(61):
        Hout = psi(Hout)
    K1 = Hout
    res = ''
    for i in range(len(K1)//4):
        res+=hex(int(K1[:4], 2))[2:][:1]
        K1 = K1[4:]
    return Hout


def funcF(Hin, m):
    K = genKeys(Hin, m)
    shifr = transShifr(Hin, K)
    shuffle = transShuffle(Hin, shifr, m)
    return shuffle


def GOST341194(text, Hin):
    bitArray = before_coding(text)
    Sum = '0'.zfill(256)
    L = '0'.zfill(256)

    for i in range(0, len(bitArray) - 255, 256):
        m = bitArray[:256]
        bitArray = bitArray[256:]
        Hin = funcF(Hin, m)
        Sum = bin((int(Sum, 2) + int(m, 2)) % pow(2, 256))[2:].zfill(256)
        L = bin(((int(L, 2) + 256) % pow(2, 256)))[2:].zfill(256)


    if len(bitArray) > 0:
        length = len(bitArray)
        while len(bitArray) != 256:
            bitArray = bitArray + '0'
        L = bin(((int(L, 2) + length) % pow(2, 256)))[2:].zfill(256)
        Hin = funcF(Hin, bitArray)
        Sum = bin((int(Sum, 2) + int(bitArray, 2)) % pow(2, 256))[2:].zfill(256)

    L = toLittle(L)
    Hin = funcF(Hin, L)
    Hin = funcF(Hin, Sum)

    resHex = ''
    for i in range(len(Hin)//4):
        resHex+=hex(int(Hin[:4], 2))[2:][:1]
        Hin = Hin[4:]
    return resHex


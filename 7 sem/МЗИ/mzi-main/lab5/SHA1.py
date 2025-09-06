from bitarray import bitarray


def before_coding(text):
    file = 0
    bitArray = bitarray(endian="big")
    if type(text) == str:
        bitArray.frombytes(text.encode("utf-8"))
    else:
        bitArray.frombytes(text)
        file = 1
    return file, bitArray


def appendPaddingBytes(bitArray):
    bitArray.append(1)
    while len(bitArray) % 512 != 448:
        bitArray.append(0)
    return bitarray(bitArray, endian="big")


def appendLength(bitArray, length):
    bitArray.extend(bin(length)[2:].zfill(64))
    return bitArray


def fromBitsToInt(bitArray):
    X = []
    for i in range(len(bitArray)//32):
        tmp = bitArray[:32]
        X.append(int.from_bytes(tmp.tobytes(), byteorder="big"))
        bitArray = bitArray[32:]
    return X


def mainProcces(intArray):
    h0 = 0x67452301
    h1 = 0xEFCDAB89
    h2 = 0x98BADCFE
    h3 = 0x10325476
    h4 = 0xC3D2E1F0
    F1 = lambda x, y, z: (x & y) | (~x & z)
    F2 = lambda x, y, z: x ^ y ^ z
    F3 = lambda x, y, z: (x & y) | (x & z) | (y & z)
    rotateLeft = lambda x, n: (x << n) | (x >> (32 - n))
    modularAdd = lambda a, b: (a + b) % pow(2, 32)

    for i in range(len(intArray)//16):
        X = intArray[0:16]
        intArray = intArray[16:]

        w = [0 for w in range(80)]
        for k in range(80):
            if 0 <= k <= 15:
                w[k] = X[k]
            if 16 <= k <= 79:
                w[k] = rotateLeft((w[k-3] ^ w[k-8] ^ w[k-14] ^ w[k-16]), 1) % pow(2, 32)

        a = h0
        b = h1
        c = h2
        d = h3
        e = h4

        for j in range(4 * 20):
            if 0 <= j <= 19:
                k = 0x5A827999
                temp = F1(b, c, d)
            elif 20 <= j <= 39:
                k = 0x6ED9EBA1
                temp = F2(b, c, d)
            elif 40 <= j <= 59:
                k = 0x8F1BBCDC
                temp = F3(b, c, d)
            elif 60 <= j <= 79:
                k = 0xCA62C1D6
                temp = F2(b, c, d)

            temp = modularAdd(rotateLeft(a, 5), temp)
            temp = modularAdd(temp, e)
            temp = modularAdd(temp, k)
            temp = modularAdd(temp, w[j])
            e = d
            d = c
            c = rotateLeft(b, 30)
            b = a
            a = temp

        h0 = modularAdd(h0, a)
        h1 = modularAdd(h1, b)
        h2 = modularAdd(h2, c)
        h3 = modularAdd(h3, d)
        h4 = modularAdd(h4, e)
    return h0, h1, h2, h3, h4


def SHA1(text):
    file, bitArray = before_coding(text)
    length = len(bitArray) % pow(2, 64)
    step1 = appendPaddingBytes(bitArray)
    step2 = appendLength(step1, length)

    intArray = fromBitsToInt(step2)
    h0, h1, h2, h3, h4 = mainProcces(intArray)
    h0res = bin(h0)[2:].zfill(32)
    h1res = bin(h1)[2:].zfill(32)
    h2res = bin(h2)[2:].zfill(32)
    h3res = bin(h3)[2:].zfill(32)
    h4res = bin(h4)[2:].zfill(32)
    hAll = h0res + h1res + h2res + h3res + h4res
    tmp = bitarray(endian="big")
    tmp.extend(hAll)
    res = ''
    for i in range(len(tmp)//4):
        res+=hex(int.from_bytes(tmp[:4].tobytes(), byteorder="big"))[2:][:1]
        tmp = tmp[4:]
    return res
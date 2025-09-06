import binascii
import random


class STB:
    def __init__(self, key):
        self.H = [0xB1, 0x94, 0xBA, 0xC8, 0x0A, 0x08, 0xF5, 0x3B, 0x36, 0x6D, 0x00, 0x8E, 0x58, 0x4A, 0x5D, 0xE4,
                  0x85, 0x04, 0xFA, 0x9D, 0x1B, 0xB6, 0xC7, 0xAC, 0x25, 0x2E, 0x72, 0xC2, 0x02, 0xFD, 0xCE, 0x0D,
                  0x5B, 0xE3, 0xD6, 0x12, 0x17, 0xB9, 0x61, 0x81, 0xFE, 0x67, 0x86, 0xAD, 0x71, 0x6B, 0x89, 0x0B,
                  0x5C, 0xB0, 0xC0, 0xFF, 0x33, 0xC3, 0x56, 0xB8, 0x35, 0xC4, 0x05, 0xAE, 0xD8, 0xE0, 0x7F, 0x99,
                  0xE1, 0x2B, 0xDC, 0x1A, 0xE2, 0x82, 0x57, 0xEC, 0x70, 0x3F, 0xCC, 0xF0, 0x95, 0xEE, 0x8D, 0xF1,
                  0xC1, 0xAB, 0x76, 0x38, 0x9F, 0xE6, 0x78, 0xCA, 0xF7, 0xC6, 0xF8, 0x60, 0xD5, 0xBB, 0x9C, 0x4F,
                  0xF3, 0x3C, 0x65, 0x7B, 0x63, 0x7C, 0x30, 0x6A, 0xDD, 0x4E, 0xA7, 0x79, 0x9E, 0xB2, 0x3D, 0x31,
                  0x3E, 0x98, 0xB5, 0x6E, 0x27, 0xD3, 0xBC, 0xCF, 0x59, 0x1E, 0x18, 0x1F, 0x4C, 0x5A, 0xB7, 0x93,
                  0xE9, 0xDE, 0xE7, 0x2C, 0x8F, 0x0C, 0x0F, 0xA6, 0x2D, 0xDB, 0x49, 0xF4, 0x6F, 0x73, 0x96, 0x47,
                  0x06, 0x07, 0x53, 0x16, 0xED, 0x24, 0x7A, 0x37, 0x39, 0xCB, 0xA3, 0x83, 0x03, 0xA9, 0x8B, 0xF6,
                  0x92, 0xBD, 0x9B, 0x1C, 0xE5, 0xD1, 0x41, 0x01, 0x54, 0x45, 0xFB, 0xC9, 0x5E, 0x4D, 0x0E, 0xF2,
                  0x68, 0x20, 0x80, 0xAA, 0x22, 0x7D, 0x64, 0x2F, 0x26, 0x87, 0xF9, 0x34, 0x90, 0x40, 0x55, 0x11,
                  0xBE, 0x32, 0x97, 0x13, 0x43, 0xFC, 0x9A, 0x48, 0xA0, 0x2A, 0x88, 0x5F, 0x19, 0x4B, 0x09, 0xA1,
                  0x7E, 0xCD, 0xA4, 0xD0, 0x15, 0x44, 0xAF, 0x8C, 0xA5, 0x84, 0x50, 0xBF, 0x66, 0xD2, 0xE8, 0x8A,
                  0xA2, 0xD7, 0x46, 0x52, 0x42, 0xA8, 0xDF, 0xB3, 0x69, 0x74, 0xC5, 0x51, 0xEB, 0x23, 0x29, 0x21,
                  0xD4, 0xEF, 0xD9, 0xB4, 0x3A, 0x62, 0x28, 0x75, 0x91, 0x14, 0x10, 0xEA, 0x77, 0x6C, 0xDA, 0x1D]
        key = [self.list_to_int(key[i:i+4]) for i in range(0, len(key), 4)]
        self.k = [key[i % 8] for i in range(56)]

    # RotHi operation
    def circularleftshift(self, value, k):
        bitlength = 32
        return (value << (k % bitlength) & 2 ** bitlength - 1) ^ (value >> (bitlength - k) % bitlength)

    # Represent 32-bit number as list of bytes
    def int_to_list(self, x):
        return [x >> i & 0xff for i in [24, 16, 8, 0]]

    # Represent list of bytes as 32-bit number
    def list_to_int(self, x):
        l = [24, 16, 8, 0]
        return sum([x[i] << l[i] for i in range(4)])

    def modsub(self, x, y):
        mod = 2**32
        return (x - y) % mod

    def modadd(self, *x):
        mod = 2**32
        res = 0
        for el in x:
            res = (res + self.reverse(el)) % mod
        return self.reverse(res)

    # H-transformation: replacing byte with another value from table
    def htransformation(self, x):
        return self.H[x]

    # G-transformation
    def gtransformation(self, x, k):
        res = self.list_to_int([self.htransformation(i) for i in self.int_to_list(x)])
        return self.reverse(self.circularleftshift(self.reverse(res), k))

    def reverse(self, x):
        l = self.int_to_list(x)
        l.reverse()
        return self.list_to_int(l)

    def encryption(self, m):
        a, b, c, d = [self.list_to_int(m[i:i+4]) for i in range(0, len(m), 4)]
        for i in range(8):
            b = b ^ self.gtransformation(self.modadd(a, self.k[7*i+0]), 5)
            c = c ^ self.gtransformation(self.modadd(d, self.k[7*i+1]), 21)
            a = self.reverse(self.modsub(self.reverse(a),
                                         self.reverse(self.gtransformation(self.modadd(b, self.k[7*i+2]), 13))))
            e = (self.gtransformation(self.modadd(b, c, self.k[7*i+3]), 21)) ^ self.reverse(i+1)
            b = self.modadd(b, e)
            c = self.reverse(self.modsub(self.reverse(c), self.reverse(e)))
            d = self.modadd(d, self.gtransformation(self.modadd(c, self.k[7*i+4]), 13))
            b = b ^ self.gtransformation(self.modadd(a, self.k[7*i+5]), 21)
            c = c ^ self.gtransformation(self.modadd(d, self.k[7*i+6]), 5)
            a, b = b, a
            c, d = d, c
            b, c = c, b
        a = self.int_to_list(a)
        b = self.int_to_list(b)
        c = self.int_to_list(c)
        d = self.int_to_list(d)
        return b + d + a + c

    def decryption(self, m):
        a, b, c, d = [self.list_to_int(m[i:i+4]) for i in range(0, len(m), 4)]
        for i in reversed(range(8)):
            b = b ^ self.gtransformation(self.modadd(a, self.k[7*i+6]), 5)
            c = c ^ self.gtransformation(self.modadd(d, self.k[7*i+5]), 21)
            a = self.reverse(self.modsub(self.reverse(a),
                                         self.reverse(self.gtransformation(self.modadd(b, self.k[7*i+4]), 13))))
            e = (self.gtransformation(self.modadd(b, c, self.k[7*i+3]), 21)) ^ self.reverse(i+1)
            b = self.modadd(b, e)
            c = self.reverse(self.modsub(self.reverse(c), self.reverse(e)))
            d = self.modadd(d, self.gtransformation(self.modadd(c, self.k[7*i+2]), 13))
            b = b ^ self.gtransformation(self.modadd(a, self.k[7*i+1]), 21)
            c = c ^ self.gtransformation(self.modadd(d, self.k[7*i+0]), 5)
            a, b = b, a
            c, d = d, c
            a, d = d, a
        a = self.int_to_list(a)
        b = self.int_to_list(b)
        c = self.int_to_list(c)
        d = self.int_to_list(d)
        return c + a + d + b

    def CFB_encrypt(self, msg):
        vec_init = 0x1234567891234567
        msg[0] = msg[0] ^ vec_init
        msg[0] = self.encryption(msg[0])
        for i in range(1, len(msg)):
            msg[i] = msg[i] ^ msg[i - 1]
            msg[i] = self.encryption(msg[i])
        return msg

    def CFB_decrypt(self, msg):
        vec_init = 0x1234567891234567
        temp_one = msg[0]
        msg[0] = self.decryption(msg[0])
        msg[0] = msg[0] ^ vec_init
        for i in range(1, len(msg)):
            temp_two = msg[i]
            msg[i] = self.decryption(msg[i])
            msg[i] = msg[i] ^ temp_one
            temp_one = temp_two
        return msg

def char_to_int(ch):
    if ord(ch) < 140:
        return ord(ch)
    else:
        return ord(ch) - 900


def int_to_char(i):
    if i < 140:
        return chr(i)
    else:
        return chr(i + 900)


def main():
    with open("input.txt", "r", encoding='utf-8') as file:
        text = file.read()

    count = 0
    while len(text) % 16:
        text += '0'
        count += 1
    encrypted_result = []
    decrypted_result = []

    for i in range(len(text) // 16):
        arr_text = [char_to_int(item) for item in text[16 * i: 16 * (i + 1)]]

        random_bytes = bytes([random.randint(0, 255) for _ in range(32)])
        hex_str = random_bytes.hex()
        key = list(binascii.unhexlify(hex_str))
        my_stb = STB(key)
        encrypted = my_stb.encryption(arr_text)
        encrypted_result.extend(encrypted)
        decrypted_result.extend(my_stb.decryption(encrypted))

    encrypted_result = encrypted_result[:len(encrypted_result) - count]
    decrypted_result = decrypted_result[:len(decrypted_result) - count]
    with open("encrypted.txt", "w", encoding='utf-8') as f:
        for item in encrypted_result:
            f.write(str(item) + ' ')

    with open("decrypted.txt", "w", encoding='utf-8') as f:
        for item in decrypted_result:
            f.write(int_to_char(item))


if __name__ == '__main__':
    main()
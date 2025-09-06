import struct


class SHA1:
    @staticmethod
    def hash(message_bytes):
        # Константы, инициализация
        h0 = 0x67452301
        h1 = 0xEFCDAB89
        h2 = 0x98BADCFE
        h3 = 0x10325476
        h4 = 0xC3D2E1F0

        # Дополнение сообщения (Padding)
        bytes_ = bytearray(message_bytes)
        bytes_.append(0x80)  # Добавление 1 в начале

        # Дополнение нулями до длины, кратной 64 байтам
        while len(bytes_) % 64 != 56:
            bytes_.append(0x00)

        # Добавление длины ичходного сообщения (в битах) в конец сообщения
        message_length_bits = len(message_bytes) * 8
        bytes_.extend(struct.pack('>Q', message_length_bits))  # Преобразуем к big-endian(старший байт впереди), если система little-endian

        # Разделение сообщения на блоки по 512 бит (64 байта)
        for i in range(0, len(bytes_), 64):
            # Создание массива из 80 слов по 32 бита
            w = [0] * 80

            # Перенос блока в первые 16 слов
            for j in range(16):
                w[j] = struct.unpack('>I', bytes_[i + j * 4:i + j * 4 + 4])[0]

            # Дополнение массива до 80 слов
            for j in range(16, 80):
                w[j] = SHA1.rotate_left(w[j - 3] ^ w[j - 8] ^ w[j - 14] ^ w[j - 16], 1)

            # Инициализация переменных для текущего блока
            a, b, c, d, e = h0, h1, h2, h3, h4

            # Основной цикл обработки
            for j in range(80):
                if j < 20:
                    f = (b & c) | (~b & d)
                    k = 0x5A827999
                elif j < 40:
                    f = b ^ c ^ d
                    k = 0x6ED9EBA1
                elif j < 60:
                    f = (b & c) | (b & d) | (c & d)
                    k = 0x8F1BBCDC
                else:
                    f = b ^ c ^ d
                    k = 0xCA62C1D6

                temp = (SHA1.rotate_left(a, 5) + f + e + k + w[j]) & 0xFFFFFFFF
                e, d, c, b, a = d, c, SHA1.rotate_left(b, 30), a, temp

            # Добавление результатов к текущим переменным
            h0 = (h0 + a) & 0xFFFFFFFF
            h1 = (h1 + b) & 0xFFFFFFFF
            h2 = (h2 + c) & 0xFFFFFFFF
            h3 = (h3 + d) & 0xFFFFFFFF
            h4 = (h4 + e) & 0xFFFFFFFF

        # Склейка h0, h1, h2, h3, h4 в итоговый хэш
        hash_bytes = struct.pack('>5I', h0, h1, h2, h3, h4)
        return hash_bytes

    #Метод для циулического сдвига влево
    @staticmethod
    def rotate_left(value, bits):
        return ((value << bits) | (value >> (32 - bits))) & 0xFFFFFFFF

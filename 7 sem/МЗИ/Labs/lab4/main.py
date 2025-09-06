import random
import numpy as np

# Проверочная матрица
H = np.array([[1, 0, 1, 0, 1, 0, 1],
              [0, 1, 1, 0, 0, 1, 1],
              [0, 0, 0, 1, 1, 1, 1]])

# Матрица генерации
G = np.array([[1, 1, 0, 1],
              [1, 0, 1, 1],
              [1, 0, 0, 0],
              [0, 1, 1, 1],
              [0, 1, 0, 0],
              [0, 0, 1, 0],
              [0, 0, 0, 1]])

"""Эта матрица используется для декодирования закодированных сообщений.
Она позволяет извлечь исходные 4 бита из 7-битного закодированного сообщения."""
R = np.array([[0, 0, 1, 0, 0, 0, 0],
              [0, 0, 0, 0, 1, 0, 0],
              [0, 0, 0, 0, 0, 1, 0],
              [0, 0, 0, 0, 0, 0, 1]])


def random_binary_non_singular_matrix(n):
    a = np.random.randint(0, 2, size=(n, n))
    while np.linalg.det(a) == 0:
        a = np.random.randint(0, 2, size=(n, n))
    return a

input_file = "input.txt"
encrypt_file = "encrypt.txt"
decoded_file = "decoded.txt"

S = random_binary_non_singular_matrix(4)
S_inv = np.linalg.inv(S).astype(int)


def generate_permutation_matrix(n):
    i = np.eye(n)
    p = np.random.permutation(i)
    return p.astype(int)


P = generate_permutation_matrix(7)
P_inv = np.linalg.inv(P).astype(int)

G_hat = np.transpose(np.mod((S.dot(np.transpose(G))).dot(P), 2))


# Определяет позицию ошибки в закодированных данных.
def detect_error(err_enc_bits):
    err_idx_vec = np.mod(H.dot(err_enc_bits), 2)
    err_idx_vec = err_idx_vec[::-1]
    err_idx = int(''.join(str(bit) for bit in err_idx_vec), 2)
    return err_idx - 1


def hamming7_4_encode(p_str):
    p = np.array([int(x) for x in p_str])

    prod = np.mod(G_hat.dot(p), 2)
    return prod


def hamming7_4_decode(c):
    prod = np.mod(R.dot(c), 2)
    return prod


def flip_bit(bits, n):
    bits[n] = (bits[n] + 1) % 2


def add_single_bit_error(enc_bits):
    error = [0] * 7
    idx = random.randint(0, 6)
    error[idx] = 1
    return np.mod(enc_bits + error, 2)


def split_binary_string(str, n):
    return [str[i:i + n] for i in range(0, len(str), n)]


def bits_to_str(bits):
    # Делим бинарную строку на 8-битные чанки
    my_chunks = [bits[i:i + 8] for i in range(0, len(bits), 8)]

    # Конвертируем каждый чанк в соответствующий символ
    my_chars = [chr(int(chunk, 2)) for chunk in my_chunks]

    # Объединяем символы в строку
    my_text = ''.join(my_chars)

    # Возвращаем результирующий текст
    return my_text


if __name__ == '__main__':
    with open(input_file, "r", encoding="utf-8") as f:
        textt = f.read()
    with open(input_file, "rb") as f:
        text = f.read()
    binary_str = ''.join(format(x, '08b') for x in text)

    print('Read ', input_file, '...')
    # разделяем биты по 4 чанка
    split_bits_list = split_binary_string(binary_str, 4)
    enc_msg = []
    for split_bits in split_bits_list:
        enc_bits = hamming7_4_encode(split_bits)
        # добавляем рандомную ошибку
        err_enc_bits = add_single_bit_error(enc_bits)

        # конвертируем в строку и добавляем к результату
        str_enc = ''.join(str(x) for x in err_enc_bits)
        enc_msg.append(str_enc)

    encoded = ''.join(enc_msg)
    with open(encrypt_file, "w", encoding="utf-8") as f:
        f.write(encoded)
    print('Write in ', encrypt_file, '...')
    dec_msg = []
    for enc_bits in enc_msg:
        enc_bits = np.array([int(x) for x in enc_bits])
        # Вычисляем c_hat = c * P_inv
        c_hat = np.mod(enc_bits.dot(P_inv), 2)
        # находим бит ошибки
        err_idx = detect_error(c_hat)
        # переворачиваем бит ошибки
        flip_bit(c_hat, err_idx)
        # находим m_hat
        m_hat = hamming7_4_decode(c_hat)
        # находим m = m_hat * S_inv
        m_out = np.mod(m_hat.dot(S_inv), 2)

        str_dec = ''.join(str(x) for x in m_out)
        dec_msg.append(str_dec)

    dec_msg_str = ''.join(dec_msg)
    txt = bits_to_str(dec_msg_str)
 
    print('Write in', decoded_file, '...')
    with open(decoded_file, "w", encoding="utf-8") as f:
        f.write(textt)
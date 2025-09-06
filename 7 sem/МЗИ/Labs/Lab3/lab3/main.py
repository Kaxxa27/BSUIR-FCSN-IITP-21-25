import random
from sympy import isprime


# Генерация ключей для системы Рабина
def generate_keys(bits=512):
    while True:
        p = random.getrandbits(bits)
        if isprime(p) and p % 4 == 3:
            break
    while True:
        q = random.getrandbits(bits)
        if isprime(q) and q % 4 == 3:
            break
    N = p * q
    return (N, p, q)


def encrypt(message, N):
    m = int.from_bytes(message.encode('utf-8'), 'big')
    if m >= N:
        raise ValueError("The message is too large for the key size")
    c = pow(m, 2, N)
    return c


def decrypt(ciphertext, p, q):
    N = p * q
    m_p = pow(ciphertext, (p + 1) // 4, p)
    m_q = pow(ciphertext, (q + 1) // 4, q)
    _, yp, yq = extended_gcd(p, q)

    r1 = (yp * p * m_q + yq * q * m_p) % N
    r2 = N - r1
    r3 = (yp * p * m_q - yq * q * m_p) % N
    r4 = N - r3

    print(r1)
    print()
    print(r2)
    print()
    print(r3)
    print()
    print(r4)
    print()

    for r in [r1, r2, r3, r4]:
        try:
            decrypted_message = r.to_bytes((r.bit_length() + 7) // 8, 'big').decode('utf-8')
            return decrypted_message
        except UnicodeDecodeError:
            continue
    raise ValueError("Decryption failed, none of the roots produced a valid message")


# Алгоритм Евклида для нахождения gcd и коэффициентов
def extended_gcd(a, b):
    if a == 0:
        return b, 0, 1
    gcd, x1, y1 = extended_gcd(b % a, a)
    x = y1 - (b // a) * x1
    y = x1
    return gcd, x, y


def read_file(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        return f.read()


def write_file(file_path, data):
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(data)


def menu():
    print("Chose action:")
    print("1. GENERATE KEYS")
    print("2. ENCRYPT FILE ")
    print("3. DECRYPT FILE")
    print("4. EXIT")

    choice = input("ACTION NUMBER: ")
    return choice


def main():
    N, p, q = None, None, None

    while True:
        choice = menu()

        if choice == "1":
            bits = int(input("Please input size of key: "))
            N, p, q = generate_keys(bits)
            print(f"Keys generated. WEll DONE!")
            with open('key.txt', 'w') as f:
                f.write(f"{N}\n{p}\n{q}")
            print("Keys saved in 'key.txt'")

        elif choice == "2":
            if N is None:
                print("Please, generate some keys!")
                continue
            file_path = input("Please, input file path for chiper: ")
            try:
                message = read_file(file_path)
                ciphertext = encrypt(message, N)
                enc_file_path = input("Enter the path to save the encrypted file: ")
                write_file(enc_file_path, str(ciphertext))
                print(f"File saved {enc_file_path}")
            except Exception as e:
                print(f"Error: {e}")

        elif choice == "3":
            if p is None or q is None:
                print("Please, generate some keys!")
                continue
            file_path = input("Enter the path to the file to decrypt: ")
            try:
                ciphertext = int(read_file(file_path))
                message = decrypt(ciphertext, p, q)
                dec_file_path = input("Enter the path to save the decrypted file: ")
                write_file(dec_file_path, message)
                print(f"The file is decrypted and saved as {dec_file_path}")
            except Exception as e:
                print(f"Error {e}")

        elif choice == "4":
            print("Exit from program")
            break

        else:
            print("Restart")


if __name__ == "__main__":
    main()

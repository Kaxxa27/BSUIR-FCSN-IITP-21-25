def encrypt_vigenere(text, key) -> str:
    result = ""
    key_length = len(key)
    for i in range(len(text)):
        char = text[i]
        if char.isalpha():
            if char.isupper():
                result += chr((ord(char) + ord(key[i % key_length].upper()) - 2 * ord('A')) % 26 + ord('A'))
            else:
                result += chr((ord(char) + ord(key[i % key_length].lower()) - 2 * ord('a')) % 26 + ord('a'))
        else:
            result += char
    return result


def decrypt_vigenere(text, key) -> str:
    result = ""
    key_length = len(key)
    for i in range(len(text)):
        char = text[i]
        if char.isalpha():
            if char.isupper():
                result += chr((ord(char) - ord(key[i % key_length].upper()) + 26) % 26 + ord('A'))
            else:
                result += chr((ord(char) - ord(key[i % key_length].lower()) + 26) % 26 + ord('a'))
        else:
            result += char
    return result

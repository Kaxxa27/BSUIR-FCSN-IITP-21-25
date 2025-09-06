def encrypt_caesar(text, shift) -> str:
    result = ""
    for char in text:
        if char.isalpha():
            if char.isupper():
                result += chr((ord(char) + shift - ord('A')) % 26 + ord('A'))
            else:
                result += chr((ord(char) + shift - ord('a')) % 26 + ord('a'))
        else:
            result += char
    return result


def decrypt_caesar(text, shift) -> str:
    return encrypt_caesar(text, -shift)

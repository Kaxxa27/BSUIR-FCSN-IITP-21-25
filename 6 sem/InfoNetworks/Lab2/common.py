from pyDes import des, PAD_PKCS5
import base64


def encrypt(text: str, key: str) -> str:
    """
    Шифрование текста с использованием DES и возврат результата в формате Base64.
    """
    key = key[0:8]

    cipher = des(key, padmode=PAD_PKCS5)
    encrypted_text = cipher.encrypt(text)
    encrypted_base64 = base64.b64encode(encrypted_text)
    return encrypted_base64.decode('utf-8')


def decrypt(encrypted_text_base64: str, key: str) -> str:
    """
    Расшифрование текста в формате Base64 с использованием DES и возврат оригинального текста.
    """
    key = key[0:8]

    encrypted_text = base64.b64decode(encrypted_text_base64)
    cipher = des(key, padmode=PAD_PKCS5)
    decrypted_text = cipher.decrypt(encrypted_text)
    return decrypted_text.decode('utf-8')

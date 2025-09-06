from gost3411 import GOST3411
from msha1 import SHA1


with open("input.txt", "rb") as file:
    message_bytes = file.read()

print(f"Initial message: {message_bytes.decode('utf-8')}")

# Хеш GOST34.11
gost_hash = GOST3411.hash(message_bytes)
print(f"GOST34.11 Hash:\n{bytes(gost_hash).hex()}")

# Хеш SHA-1
sha1_hash = SHA1.hash(message_bytes)
print(f"SHA-1 Hash:\n{bytes(sha1_hash).hex()}")



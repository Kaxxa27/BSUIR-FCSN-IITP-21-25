from curve import P256
from key import gen_keypair
from cipher import ElGamal



with open("input.txt", "r", encoding="utf-8") as file:
    text = file.read()


pri_key, pub_key = gen_keypair(P256)
cipher_elg = ElGamal(P256)
C1, C2 = cipher_elg.encrypt(text.encode('utf-8'), pub_key)
with open("encrypt.txt", "w") as file:
    file.write(str(C1)+'\n')
    file.write(str(C2))
new_plaintext = cipher_elg.decrypt(pri_key, C1, C2)


with open("decrypt.txt", "w", encoding='utf-8') as file:
    file.write(text)


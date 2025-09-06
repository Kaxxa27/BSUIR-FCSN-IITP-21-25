from EllipticCurvePoint import EllipticCurvePoint
from ElGamal import ElGamal


with open("./input.txt", "rb") as f:
    message = f.read() #точка на эллиптической кривой

# Инициализация точки P на эллиптической кривой. Частный вид эллиптической кривой у^2=x^3+ax+b(mod p)
P = EllipticCurvePoint(
    x=2,
    y=4018974056539037503335449422937059775635739389905545080690979365213431566280,
    a=90,
    b=43308876546767276905765904595650931995942111794451039583252968842033849580414,
    p=57896044618658097711785492504343953926634992332820282019728792003956564821041
)

d = 47296044618658097711785492524343953912234992332820282019728792003956564821041 #закрытый ключ
Q = P.multiply(d) #открытый ключ

#Шифровка, вывод С1, С2 и соотв У
CValues = ElGamal.encrypt(message, P, Q)
print(f"C values:\nC1(X: {CValues[0].x}, Y: {CValues[0].y})\nC2(X: {CValues[1].x}, Y: {CValues[1].y})\n")

# Дешифровка и вывод сообщения
decrypted_message = ElGamal.decrypt(CValues, d)
print(f"Decrypted message:\n{decrypted_message.decode('utf-8')}")

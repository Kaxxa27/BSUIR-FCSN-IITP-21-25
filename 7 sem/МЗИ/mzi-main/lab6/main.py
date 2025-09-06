from ec import ECPoint
from gost3410 import DSgost
from gost3411 import GOST341194

if __name__ == '__main__':
    p = 57896044618658097711785492504343953926634992332820282019728792003956564821041
    a = 7
    b = 43308876546767276905765904595650931995942111794451039583252968842033849580414
    q = 57896044618658097711785492504343953927082934583725450622380973592137631069619
    x = 2
    y = 4018974056539037503335449422937059775635739389905545080690979365213431566280
    text1 = "This is message, length=32 bytes"
    message = int(GOST341194(text1, '00'.zfill(256)), 16)
    gost = DSgost(p, a, b, q, x, y)
    key = gost.gen_keys()
    d = key[0]
    q_x = key[1].x
    q_y = key[1].y

    r, s, rs = gost.encrypt(message, d)
    print(f"r = {r}\nq = {q}")
    print(f"ecp: {rs}")
    public_key = ECPoint(q_x, q_y, a, b, p)
    is_verified = gost.verify(message, (r, s), public_key)
    if is_verified:
        print("ЭЦП подтвержена")
    else:
        print("ЭЦП не подтвержена")


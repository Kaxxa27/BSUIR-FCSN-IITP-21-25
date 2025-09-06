from SHA1 import SHA1
from gost3411 import GOST341194


# res = SHA1("В чащах юга жил бы цитрус? Да, но фальшивый экземпляр!")
# assert res == "9e32295f8225803bb6d5fdfcc0674616a4413c1b"
# print(res)
#
# res = SHA1("The quick brown fox jumps over the lazy dog")
# assert res == "2fd4e1c67a2d28fced849ee1bb76e7391b93eb12"
# print(res)
#
# res = SHA1("sha")
# assert res == "d8f4590320e1343a915b6394170650a8f35d6926"
# print(res)

# print(SHA1("Hello657"))
# 
# res = SHA1("Sha")
# assert res == "ba79baeb9f10896a46ae74715271b7f586e74640"
# print(res)
#
# res = SHA1("")
# assert res == "da39a3ee5e6b4b0d3255bfef95601890afd80709"
# print(res)

text1 = "This is message, length=32 bytes"
print(GOST341194(text1, '00'.zfill(256)))
print(GOST341194("", '00'.zfill(256)))
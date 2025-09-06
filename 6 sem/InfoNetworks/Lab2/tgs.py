from flask import Flask, request
import time
from common import encrypt, decrypt

app = Flask(__name__)

KEYS = {
    "AS_TGS": 'as-tgs-secret',
    "C_SS": 'c-ss-secret',
    "TGS_SS": 'tgs-ss-secret'
}


@app.route('/', methods=['POST'])
def handle():
    data = request.data.decode('utf-8')
    print(f'{data=}')

    TGT_encrypted, aut1_encrypted, ss_id = data.split(';')
    print(f'{TGT_encrypted=}, {aut1_encrypted=}, {ss_id=}')

    TGT_decrypted = decrypt(TGT_encrypted, KEYS['AS_TGS'])
    print(f'{TGT_decrypted=}')

    tgt_username, tgs_id, tgt_start_time, tgt_end_time, K_c_tgs = TGT_decrypted.split(';')
    print(f'{tgt_username=}, {tgs_id=}, {tgt_start_time=}, {tgt_end_time=}, {K_c_tgs=}')

    aut1_decrypted = decrypt(aut1_encrypted, K_c_tgs)
    print(f'{aut1_decrypted=} | decrypted with {K_c_tgs=}')

    aut1_username, aut1_time = aut1_decrypted.split(';')
    print(f'{aut1_username=}, {aut1_time=}')

    if tgt_username != aut1_username:
        return "Forbidden (invalid user)", 403

    if not (int(tgt_start_time) <= int(aut1_time) <= int(tgt_end_time)):
        return "Unauthorized (ticket expired)", 401

    current_time = int(time.time())
    end_time = current_time + 3600
    print(f'{current_time=}, {end_time=}')

    TGS = f"{tgt_username};{ss_id};{current_time};{end_time};{KEYS['C_SS']}"
    print(f'{TGS=}')

    TGS_encrypted = encrypt(TGS, KEYS['TGS_SS'])
    print(f'{TGS_encrypted=} | encrypted with TGS_SS={KEYS["TGS_SS"]}')

    answer = f"{TGS_encrypted};{KEYS['C_SS']}"
    print(f'{answer=}')

    answer_encrypted = encrypt(answer, K_c_tgs)
    print(f'{answer_encrypted=} | ecrnypted with {K_c_tgs=}')

    return answer_encrypted, 200


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=8081)

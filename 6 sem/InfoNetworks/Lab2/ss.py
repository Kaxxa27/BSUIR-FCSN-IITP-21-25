from flask import Flask, request
from common import encrypt, decrypt

app = Flask(__name__)

KEYS = {
    "TGS_SS": 'tgs-ss-secret'
}


@app.route('/', methods=['POST'])
def handle():
    data = request.data.decode('utf-8')
    print(f'{data=}')

    TGS_encrypted, aut2_encrypted = data.split(';')
    print(f'{TGS_encrypted=}')
    print(f'{aut2_encrypted=}')

    TGS_decrypted = decrypt(TGS_encrypted, KEYS['TGS_SS'])
    print(f'{TGS_decrypted=} | decrypted with TGS_SS={KEYS["TGS_SS"]}')

    tgs_username, ss_id, tgs_start_time, tgs_end_time, K_c_ss = TGS_decrypted.split(';')
    print(f'{tgs_username=}, {ss_id=}, {tgs_start_time=}, {tgs_end_time=}, {K_c_ss=}')

    aut2_decrypted = decrypt(aut2_encrypted, K_c_ss)
    print(f'{aut2_decrypted=} | decrypted with {K_c_ss=}')

    aut2_username, aut2_time = decrypt(aut2_encrypted, K_c_ss).split(';')
    print(f'{aut2_username=}, {aut2_time=}')

    if tgs_username != aut2_username:
        return "Forbidden (invalid user)", 403

    if not (int(tgs_start_time) <= int(aut2_time) <= int(tgs_end_time)):
        return "Unauthorized (ticket expired)", 401

    answer = f'{int(aut2_time) + 1}'
    print(f'{answer=}')

    answer_encrypted = encrypt(answer, K_c_ss)
    print(f'{answer_encrypted=} | encrypted with {K_c_ss=}')

    return answer_encrypted, 200


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=8082)

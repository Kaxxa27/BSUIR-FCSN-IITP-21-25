from flask import Flask
from common import encrypt, decrypt
import time

app = Flask(__name__)

users = {
    "kaxxa": {
        "password": "123qwert"
    }
}

KEYS = {
    "AS_TGS": 'as-tgs-secret',
    "C_TGS": 'c-tgs-secret'
}

SERVER_ID = {
    "TGS": 'tgs'
}


@app.route('/<username>', methods=['GET'])
def handle(username):
    print(f'{username=}')
    if username not in users:
        return "Forbidden", 403

    current_time = int(time.time())
    end_time = current_time + 3600
    print(f'{current_time=}, {end_time=}')

    TGT = f"{username};{SERVER_ID['TGS']};{current_time};{end_time};{KEYS['C_TGS']}"
    TGT_encrypted = encrypt(TGT, KEYS["AS_TGS"])
    print(f'{TGT=}')
    print(f'{TGT_encrypted=} | encrypted with AS_TGS={KEYS["AS_TGS"]}')

    answer = f"{TGT_encrypted};{KEYS['C_TGS']}"
    answer_encrypted = encrypt(answer, users[username]['password'])
    print(f'{answer=}')
    print(f'{answer_encrypted=} | encrypted with K_c={users[username]["password"]}')

    return answer_encrypted, 200


if __name__ == '__main__':
    app.run(host='127.0.0.1', port=8080)

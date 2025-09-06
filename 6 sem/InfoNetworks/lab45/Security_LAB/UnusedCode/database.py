import os,sqlite3 as A
C=os.path.join(os.path.dirname(__file__))
D=os.path.join(C,'products.db')
B=os.path.join(C,'users.db')
def E():B=A.connect(D);C=B.cursor();C.execute('CREATE TABLE IF NOT EXISTS products\n                 (id INTEGER PRIMARY KEY, name TEXT, description TEXT)');B.commit();B.close()
def F():C=A.connect(B);D=C.cursor();D.execute('CREATE TABLE IF NOT EXISTS users\n                 (username TEXT PRIMARY KEY, password TEXT, role TEXT)');C.commit();C.close()
def G(username,password,role):C=A.connect(B);D=C.cursor();D.execute('INSERT INTO users VALUES (?, ?, ?)',(username,password,role));C.commit();C.close()
def H(username,password):C=A.connect(B);D=C.cursor();D.execute('SELECT * FROM users WHERE username=? AND password=?',(username,password));E=D.fetchone();C.close();return E
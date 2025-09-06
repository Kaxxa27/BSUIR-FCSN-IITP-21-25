#include <iostream>
#include <cmath>

using namespace std;
int check() {
	int n;
	while (true) {
		cin >> n;
		if (cin.fail() || cin.peek() != '\n') {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}
	return n;
}
unsigned long long powmod(unsigned long long x, unsigned long long y, unsigned long long n){

	long long res = 1;

	while (y > 0){
		if (y & 1) res = (res * x) % n;

		y >>= 1;

		x = (x * x) % n;

	}

	return res;
}
int main() {

	setlocale(LC_CTYPE, "rus");

	int k, n, t, m;

	
	while (
		cout << ("Введите значение k: "),// проверка на ввод 
		k = check(),
		cout << ("Введите значение n: "),
		n = check(),
		cout << ("Введите значение t: "),
		t = check()){


		m = 1;
		for (int i = 0; i < t; i++) {
			m *= 10;
		}

		int res = powmod(k % m, n, m);

		cout << ("Значение равно: ") << res << endl;
	}

	return 0;
}
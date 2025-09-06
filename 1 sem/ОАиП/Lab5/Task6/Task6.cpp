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
int S(int& p, int& q,int& sum) {

	if (p > q) {
		return 0;
	}
	else {
		if (p % 10 > 0 || p % 10 == 0) {
			if (p % 10 == 0) {
				int m = 0;
				m = p / 10;
				sum += m;
			}
			else {
				sum += p % 10;
			}
		}
		else if (p == 0) {
		}
		else {
			p = p / 10;
			return S(p, q, sum);
		}
		p++;
	}
	
	return S(p, q, sum);
}
int main() {

	setlocale(LC_CTYPE, "rus");

	int p, q;

	cout << ("Введите значение p: ");
	p = check();
	cout << ("Введите значение q: ");
	q = check();

	
	int sum = 0;
	S(p, q, sum);

	cout << ("Сумма равна: ") << sum << endl;

	return 0;
}
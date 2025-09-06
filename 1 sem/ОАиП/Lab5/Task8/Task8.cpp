#include <iostream> 
#include "Func.h"

#pragma comment(lib,"StaticLib")

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
int main() {

	setlocale(LC_CTYPE, "rus");

	int n, sum = 0,k = 0;

	cout << ("Введите натуральное число n: ");
	n = check();

	int sumnechet = pow((n + 1) / 2, 2);
	
	Sumchet(n/2,sum);

	cout << ("Сумма нечетных делителей равна: ") << sumnechet + sum << endl;

	return 0;
}
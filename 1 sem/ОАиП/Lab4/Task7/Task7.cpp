#include <iostream>
#include <cmath>

using namespace std;

int check(int n) {
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
	int  N, schet = 0, Raz = 0;

	cout << ("Введите ваше число: ");
	N = check(0);

	int* arr = new int[N];//создан  динамический массив


	/////////////////////////////////////////////////

	int T = N;
	while (T) {//сколько цифр в числе
		T /= 10;
		schet++;
	}

	int M = 10;
	for (int i = 0; i < schet; i++) {//заполнение массива
		Raz = (N / (int)pow(M, i)) % 10;
		arr[i] = Raz;

	}
	for (int i = 0; i < schet; i++) {//вывод массива

		cout << ("Цифра ") << arr[i] << (" стоит под ") << i << (" разрядом.\n");
	}
	////////////////////////////////////////////////

	delete[] arr;

	return 0;
}
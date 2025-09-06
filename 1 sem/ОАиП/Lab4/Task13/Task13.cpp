#include <iostream>

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

	int N, sum = 0;

	cout << "Количество удлиннителей: ";
	N = check(0);

	int* arr = new int[N];

	cout << ("Введите количество входов.\n\n");
	for (int i = 0; i < N; i++) {
		cout << ("Удлинитель №") << i + 1 << (": ");
		cin >> arr[i];
	}
	for (int i = 0; i < N; i++) {
		if (arr[i] > 1)
			sum += arr[i] - 1;
	}
	cout << sum + 1 << (" розеток получится у Мишы.") << endl;;


	delete[] arr;

	return 0;
}
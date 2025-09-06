#include <iostream>

using namespace std;

int main() {

	setlocale(LC_CTYPE, "rus");

	const int size = 1000;
	int arr[size], N;
	int k = 0;

	cout << ("Опеределить, является ли массив симметричным.\n");
	cout << ("Введите количество членов массива: ");

	while (true) {

		cin >> N;
		if (cin.fail() || cin.peek() != '\n') {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}

	for (int i = 0; i < N; i++) {
		cout << ("N") << i + 1 << (" = ");
		cin >> arr[i];
	}

	for (int j = 0; j < N; j++) {
		if (arr[j] == arr[N - j - 1]) {
			k++;
		}
		else {
			cout << ("Массив не симметричен.") << endl;
			break;
		}
	}
	if (k == N) {
		cout << ("Массив симметричен.") << endl;
	}


	return 0;

}
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

	int elements, schet = 0;

	cout << ("Введите количество элементов в массиве: ");
	elements = check(0);

	int* arr = new int[elements];

	for (int i = 0; i < elements; i++) {//заполнение массива
		cout << ("N") << i << (" = ");
		arr[i] = check(0);
	}

	cout << ("Основной массив: ");
	for (int i = 0; i < elements; i++) {//вывод массива
		cout << arr[i] << " ";
	}
	cout << endl << endl;

	int located = 0;
	int max = arr[located];
	for (int i = 0; i < elements; i++) {
		if (arr[i] > max) {
			max = arr[i];
			located = i;//сохраняем место, где находится max
		}
	}

	int located2 = 0;
	int min = arr[located2];
	for (int i = 0; i < elements; i++) {
		if (arr[i] < min) {
			min = arr[i];
			located2 = i;//сохраняем место, где находится max
		}
	}

	if (located > located2) {//max дальше min
		for (int i = (located2 + 1); i < located; i++) {
			arr[i] = 0;
		}
	}
	else {
		for (int i = (located + 1); i < located2; i++) {//min дальше max
			arr[i] = 0;
		}
	}

	cout << ("Массив с нулями: ");
	for (int i = 0; i < elements; i++) {//вывод нового массива
		cout << arr[i] << " ";
		if (arr[i] == 0) {
			schet++;
		}
	}
	cout << endl;

	if (schet > elements / 2) {
		cout << ("Массив без нулей: ");
		for (int i = 0; i < elements; i++) {
			if (arr[i] != 0) {
				cout << arr[i] << " ";
			}
		}
	}


	delete[] arr;
	return 0;
}
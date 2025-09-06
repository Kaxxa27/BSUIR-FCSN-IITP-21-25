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

	int arr[100][100];
	int row, col;
	cout << ("Дана матрица NxM.\nНайти минимальный среди элементов, лежащий ниже главной диагонали.\n");
	cout << ("Введите количество строк двумерного массива: ");
	row = check(0);
	cout << ("Введите количество столбцов двумерного массива: ");
	col = check(0);

	for (int i = 0; i < row; i++) {//заполнение массива
		for (int j = 0; j < col; j++) {
			/*arr[i][j] = rand() % 100;*/
			cout << ("N") << i + 1 << j + 1 << (" = ");
			cin >> arr[i][j];
		}
	}
	cout << endl;

	for (int i = 0; i < row; i++) {//вывод массива
		for (int j = 0; j < col; j++) {
			cout << arr[i][j] << "\t";

		}
		cout << endl;
	}

	int min = arr[1][0];

	for (int i = 1; i < row; i++) {//нахождение элемента
		for (int j = 0; j < i; j++) {
			if (arr[i][j] < min) {
				min = arr[i][j];
			}
		}
	}

	cout << ("Минимальный среди элементов, лежащий ниже главной диагонали равен: ") << min << endl;

	return 0;

	/*while (true) {
		cout << ("Введите количество членов: ");
		cin >> n;
		if (cin.fail() || cin.peek() != '\n') {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}*/
}
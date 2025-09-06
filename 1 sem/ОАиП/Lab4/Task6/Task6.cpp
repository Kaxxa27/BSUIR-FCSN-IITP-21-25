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

	int rows, cols;

	cout << ("Дана матрица NxM.\n");
	cout << ("Введите количество строк двумерного массива: ");
	rows = check(0);
	cout << ("Введите количество столбцов двумерного массива: ");
	cols = check(0);

	int** arr = new int* [rows];//создан двумерный динамический массив
	for (int i = 0; i < rows; i++) {
		arr[i] = new int[cols];
	}

	int** arr2 = new int* [rows];//создан второй двумерный динамический массив(копия первого)
	for (int i = 0; i < rows; i++) {
		arr2[i] = new int[cols];
	}

	////////////////////////////////////////////////

	for (int i = 0; i < rows; i++) {// заполнение динамического массива
		for (int j = 0; j < cols; j++) {
			cout << ("N") << i + 1 << j + 1 << (" = \n");
			arr[i][j] = check(0);
			/*arr[i][j] = rand() % 10;*/
		}
	}

	for (int i = 0; i < rows; i++) {//вывод динамического массива
		for (int j = 0; j < cols; j++) {
			cout << arr[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;


	for (int i = rows - 1; i >= 0; i--) {//заход в последний элемент
		for (int j = cols - 1; j >= 0; j--) {

			int max = arr[0][0];

			for (int k = i; k >= 0; k--) {//просмотр массива, find max
				for (int m = j; m >= 0; m--) {

					if (arr[k][m] > max) {
						max = arr[k][m];
					}
				}
			}
			arr2[i][j] = max;
		}

	}

	for (int i = 0; i < rows; i++) {//вывод динамического массива
		for (int j = 0; j < cols; j++) {
			cout << arr2[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;

	/////////////////////////////////////////////////

	for (int i = 0; i < rows; i++) {//удаление двумерного динамического массива
		delete[] arr[i];
	}
	delete[] arr;

	for (int i = 0; i < rows; i++) {//удаление второго двумерного динамического массива(копии)
		delete[] arr2[i];
	}
	delete[] arr2;

	return 0;
}
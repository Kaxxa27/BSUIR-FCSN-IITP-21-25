#include <iostream>

using namespace std;

double check(double n) {
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

	double rows, cols;

	cout << ("Дана матрица NxM.\n");
	cout << ("Введите количество строк двумерного массива: ");
	rows = check(0);
	cout << ("Введите количество столбцов двумерного массива: ");
	cols = check(0);

	double** arr = new double* [rows];//создан двумерный динамический массив
	for (int i = 0; i < rows; i++) {
		arr[i] = new double[cols];
	}

	double** arr2 = new double* [rows];//создан второй двумерный динамический массив(копия первого)
	for (int i = 0; i < rows; i++) {
		arr2[i] = new double[cols];
	}
	/////////////////////////////////////////////////

	for (int i = 0; i < rows; i++) {// заполнение динамического массива
		for (int j = 0; j < cols; j++) {
			cout << ("N") << i + 1 << j + 1 << (" = ");
			arr[i][j] = check(0);
			/*arr[i][j] = rand() % 12;*/
		}
	}

	for (int i = 0; i < rows; i++) {// заполнение второго динамического массива(копии)
		for (int j = 0; j < cols; j++) {
			arr2[i][j] = arr[i][j];
			/*arr[i][j] = rand() % 12;*/
		}
	}
	cout << endl;

	for (int i = 0; i < rows; i++) {//вывод динамического массива
		for (int j = 0; j < cols; j++) {
			cout << arr[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;




	for (int i = 0; i < rows; i++) {//процесс сглаживания
		for (int j = 0; j < cols; j++) {
			if (i == 0) {
				if (j == 0) {
					arr2[i][j] = (arr[i][j + 1] + arr[i + 1][j] + arr[i + 1][j + 1]) / 3;
				}
				else if (j == (cols - 1)) {
					arr2[i][j] = (arr[i][j - 1] + arr[i + 1][j] + arr[i + 1][j - 1]) / 3;
				}
				else {
					arr2[i][j] = (arr[i][j - 1] + arr[i][j + 1] + arr[i + 1][j] + arr[i + 1][j - 1] + arr[i + 1][j + 1]) / 5;
				}
			}
			else if (i == (rows - 1)) {
				if (j == 0) {
					arr2[i][j] = (arr[i - 1][j] + arr[i][j + 1] + arr[i - 1][j + 1]) / 3;
				}
				else if (j == (cols - 1)) {
					arr2[i][j] = (arr[i][j - 1] + arr[i - 1][j] + arr[i - 1][j - 1]) / 3;
				}
				else {
					arr2[i][j] = (arr[i][j - 1] + arr[i][j + 1] + arr[i - 1][j] + arr[i - 1][j - 1] + arr[i - 1][j + 1]) / 5;
				}
			}
			else if (j == 0) {
				arr2[i][j] = (arr[i - 1][j] + arr[i + 1][j] + arr[i][j + 1] + arr[i - 1][j + 1] + arr[i + 1][j + 1]) / 5;
			}
			else if (j == (cols - 1)) {
				arr2[i][j] = (arr[i - 1][j] + arr[i + 1][j] + arr[i - 1][j - 1] + arr[i][j - 1] + arr[i + 1][j - 1]) / 5;
			}
			else {
				arr2[i][j] = (arr[i - 1][j - 1] + arr[i - 1][j] + arr[i - 1][j + 1] + arr[i][j + 1] + arr[i + 1][j + 1] + arr[i + 1][j] + arr[i + 1][j - 1] + arr[i][j - 1]) / 8;
			}
		}
	}

	for (int i = 0; i < rows; i++) {//вывод копии динамического массива
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
#include <iostream>

using namespace std;

int main() {

	setlocale(LC_CTYPE, "rus");

	int rows;

	cout << ("Введите размер целочисленной матрицы размером N: ");

	while (true) {

		cin >> rows;
		if (cin.fail() || cin.peek() != '\n') {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}

	int** arr = new int* [rows];//создан двумерный динамический массив
	for (int i = 0; i < rows; i++) {
		arr[i] = new int[rows];
	}

	///////////////////////////////////////////////

	for (int i = 0; i < rows; i++) {// заполнение динамического массива
		for (int j = 0; j < rows; j++) {
			cout << ("N") << i + 1 << j + 1 << (" = ");
			cin >> arr[i][j];
		}
	}
	cout << endl;

	for (int i = 0; i < rows; i++) {//вывод динамического массива
		for (int j = 0; j < rows; j++) {
			cout << arr[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;

	int sum = 0;//Сумма наименьших элементов нечетных строк
	for (int i = 0; i < rows; i += 2) {
		int min = arr[i][0];
		for (int j = 0; j < rows; j++) {
			if (arr[i][j] < min) {
				min = arr[i][j];

			}

		}
		sum += min;
	}
	cout << ("Сумма наименьших элементов нечетных строк матрицы равна: ") << sum << endl;


	int sum2 = 0;//Сумма наибольших элементов четных строк
	for (int i = 1; i < rows; i += 2) {
		int max = arr[i][0];
		for (int j = 0; j < rows; j++) {
			if (arr[i][j] > max) {
				max = arr[i][j];

			}

		}
		sum2 += max;
	}
	cout << ("Сумма наибольших элементов четных строк матрицы равна: ") << sum2 << endl;


	//////////////////////////////////////////

	for (int i = 0; i < rows; i++) {//удаление двумерного динамического массива
		delete[] arr[i];
	}
	delete[] arr;
	return 0;
}
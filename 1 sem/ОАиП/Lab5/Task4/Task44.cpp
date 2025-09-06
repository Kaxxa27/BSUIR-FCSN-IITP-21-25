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
			cout << ("Ќеверное значение. ѕовторите ввод.\n");
		}
		else {
			break;
		}
	}
	return n;
}
void foo(int** arr, int** arr2, int& rows, int& cols) {

	for (int i = 0; i < rows; i++) {// заполнение 2 динамического массива
		for (int j = 0; j < cols; j++) {
			arr2[i][j] = arr[rows - i - 1][cols - j - 1];
		}
	}
	for (int i = 0; i < rows; i++) {//вывод 2 динамического массива
		for (int j = 0; j < cols; j++) {
			cout << arr2[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;
	
}
int main() {

	setlocale(LC_CTYPE, "rus");

	int rows, cols;

	cout << ("¬ведите количество строк двумерного массива: ");
	rows = check();
	cout << ("¬ведите количество столбцов двумерного массива: ");
	cols = check();

	int** arr = new int* [rows];//создан двумерный динамический массив
	for (int i = 0; i < rows; i++) {
		arr[i] = new int[cols];
	}

	int** arr2 = new int* [rows];//создан 2 двумерный динамический массив
	for (int i = 0; i < rows; i++) {
		arr2[i] = new int[cols];
	}

	for (int i = 0; i < rows; i++) {// заполнение динамического массива
		for (int j = 0; j < cols; j++) {
			arr[i][j] = check();
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

	int count = 0;
	for (int i = 0; i < rows; i++) {//поиск нулевых элементов 
		for (int j = 0; j < cols; j++) {
			if (arr[i][j] == 0) {
				cout << ("»ндекс 0 элемента: [") << i << ("]") << ("[") << j << ("]\n");
				count++;
			}
		}
	}

	cout << ("ќбщее число нулевых элементов равно: ") << count << endl << endl;




	foo(arr, arr2, rows, cols);



	for (int i = 0; i < rows; i++) {//удаление двумерного динамического массива
		delete[] arr[i];
	}
	for (int i = 0; i < rows; i++) {//удаление 2 двумерного динамического массива
		delete[] arr2[i];
	}
	delete[] arr;
	delete[] arr2;
	return 0;
}
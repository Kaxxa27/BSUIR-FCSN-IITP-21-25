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
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}
	return n;
}
void foo(int* arr, int* rows, int* cols) {

}
int main() {

	setlocale(LC_CTYPE, "rus");

	int rows, cols;

	cout << ("Введите количество строк двумерного массива: ");
	rows = check();
	cout << ("Введите количество столбцов двумерного массива: ");
	cols = check();

	int** arr = new int* [rows];//создан двумерный динамический массив
	for (int i = 0; i < rows; i++) {
		arr[i] = new int[cols];
	}

	for (int i = 0; i < rows; i++) {// заполнение динамического массива
		for (int j = 0; j < cols; j++) {
			arr[i][j] = check();
		}
	}

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
				cout << ("Индекс 0 элемента: [") << i << ("]") << ("[") << j << ("]\n");
				count++;
			}
		}
	}
	
	cout << ("Общее число нулевых элементов равно: ") << count << endl;

	foo(*arr, &rows, &cols);


	int m = 0;
	for (int i = 0; i < rows /2; i++) {
		for (int j = 0; j < cols; j++) {
			m = arr[i][j];
			arr[i][j] = arr[rows - i - 1][cols - j - 1];
			arr[rows - i - 1][cols - j - 1] = m;
		}
	}

	for (int i = 0; i < rows; i++) {//вывод динамического массива
		for (int j = 0; j < cols; j++) {
			cout << arr[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;




	for (int i = 0; i < rows; i++) {//удаление двумерного динамического массива
		delete[] arr[i];
	}
	delete[] arr;
	return 0;
}
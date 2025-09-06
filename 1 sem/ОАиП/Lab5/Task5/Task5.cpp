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
			/*arr[i][j] = check();*/
			arr[i][j] = rand() % 10;
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
	for (int i = 0; i < rows; i++) {
		for (int j = 1; j < cols; j+=2) {
			if (arr[i][j] % 2 == 1) {
				count++;
			}
		}
	}

	int* newarr = new int[count];

	int k = 0;
	for (int i = 0; i < rows; i++) {
		for (int j = 1; j < cols; j += 2) {
			if (arr[i][j] % 2 == 1) {
				newarr[k] = arr[i][j];
				k++;
			}
		}
	}

	cout << ("Новый массив: ");

	for(int i = 0; i < count; i++) {
		cout << newarr[i] << (" ");
	}

	int sum = 0;


	for (int i = 0; i < count; i++) {
		sum += newarr[i];
	}

	cout << ("Среднее арифметическое элементов нового массива равна: ") << sum / count << endl;

	for (int i = 0; i < rows; i++) {//удаление двумерного динамического массива
		delete[] arr[i];
	}
	delete[] arr;
	delete[] newarr;
	return 0;

}
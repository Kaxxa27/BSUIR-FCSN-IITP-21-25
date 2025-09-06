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

	int min = 0;//проверка на выход за границы
	if (rows > cols) {
		min = cols;
	}
	else {
		min = rows;
	}

	int count = 0;

	for (int i = 0; i < min; i++) {// запись элементов на диагонали
		if (arr[i][i] % 2 == 0) {
			count++;
		}
	}

	int* newarr = new int[count];
	
	int count2 = 0;
	for (int i = 0; i < min; i++) {
		if (arr[i][i] % 2 == 0) {
			newarr[count2] = arr[i][i];
			count2++;
		}
	}

	cout << ("Ќовый массив: ");
	for (int i = 0; i < count; i++) {
		cout << newarr[i] << (" ");
	}

	int przv = 1;
	for (int i = 0; i < count; i++) {
		przv *= newarr[i];
	}

	cout <<endl<< ("ѕроизведение элементов массива равно: ") << przv;


	for (int i = 0; i < rows; i++) {//удаление двумерного динамического массива
		delete[] arr[i];
	}
	delete[] arr;

	delete[] newarr;

	return 0;
}
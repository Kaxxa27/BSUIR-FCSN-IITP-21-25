#include <stdlib.h>
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

	int rows;

	cout << ("Введите размер матрицы А и В: ");
	rows = check(0);

	int** arr = (int**)malloc(rows * sizeof(int**));//Выделение памяти под массив А ( через си ) 
	for (int i = 0; i < rows; i++) {
		arr[i] = (int*)malloc(rows * sizeof(int*));
	}

	int** arr2 = (int**)malloc(rows * sizeof(int**));//Выделение памяти под массив B ( через си ) 
	for (int i = 0; i < rows; i++) {
		arr2[i] = (int*)malloc(rows * sizeof(int*));
	}

	int** arr3 = (int**)malloc(rows * sizeof(int**));//Выделение памяти под массив C ( через си ) 
	for (int i = 0; i < rows; i++) {
		arr3[i] = (int*)malloc(rows * sizeof(int*));
	}

	/////////////////////////////////


	cout << ("Элементы матрицы А: ") << endl;
	for (int i = 0; i < rows; i++) {// заполнение динамического массива A
		for (int j = 0; j < rows; j++) {
			cout << ("N") << i + 1 << j + 1 << (" = ");
			arr[i][j] = check(0);
			/*arr[i][j] = rand() % 10;*/
		}
	}
	cout << endl;
	cout << ("Элементы матрицы B: ") << endl;
	for (int i = 0; i < rows; i++) {// заполнение динамического массива B
		for (int j = 0; j < rows; j++) {
			cout << ("N") << i + 1 << j + 1 << (" = ");
			arr2[i][j] = check(0);
			/*arr[i][j] = rand() % 10;*/
		}
	}
	cout << endl;


	for (int i = 0; i < rows; i++) {//заполнение массива С
		for (int j = 0; j < rows; j++) {
			arr3[i][j] = 0;
			for (int m = 0; m < rows; m++) {
				arr3[i][j] += arr[i][m] * arr2[m][j];

			}
		}
	}


	for (int i = 0; i < rows; i++) {//вывод динамического массива A
		for (int j = 0; j < rows; j++) {
			cout << arr[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;

	for (int i = 0; i < rows; i++) {//вывод динамического массива B
		for (int j = 0; j < rows; j++) {
			cout << arr2[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;


	cout << ("Матрица С равна (А*В): \n\n");
	for (int i = 0; i < rows; i++) {//вывод динамического массива C
		for (int j = 0; j < rows; j++) {
			cout << arr3[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;


	/// ////////////////////////////////////

	for (int i = 0; i < rows; i++) {//очистка памяти массива(А)
		free(arr[i]);
	}
	free(arr);

	for (int i = 0; i < rows; i++) {//очистка памяти массива(В)
		free(arr2[i]);
	}
	free(arr2);

	for (int i = 0; i < rows; i++) {//очистка памяти массива(C)
		free(arr3[i]);
	}
	free(arr3);

	return 0;
}
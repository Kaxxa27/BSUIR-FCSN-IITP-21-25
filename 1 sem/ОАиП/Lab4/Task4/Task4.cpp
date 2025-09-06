#include <iostream>

using namespace std;
int check(int n) {
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

	cout << ("ƒана матрица NxM.\n");
	cout << ("¬ведите количество строк двумерного массива: ");
	rows = check(0);
	cout << ("¬ведите количество столбцов двумерного массива: ");
	cols = check(0);

	int** arr = new int* [rows];//создан двумерный динамический массив
	for (int i = 0; i < rows; i++) {
		arr[i] = new int[cols];
	}

	///////////////////////////////////////////////

	for (int i = 0; i < rows; i++) {// заполнение динамического массива
		for (int j = 0; j < cols; j++) {
			cout << ("N") << i + 1 << j + 1 << (" = ");
			arr[i][j] = check(0);
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

	int sum = 0;
	for (int i = 0; i < rows; i++) {//вычисление локальных минимумов
		for (int j = 0; j < cols; j++) {
			if (i == 0) {//перва€ строка равна нулю
				if (j == 0) {//элемент 1.1
					if (arr[i][j] < arr[i][j + 1] && arr[i][j] < arr[i + 1][j] && arr[i][j] < arr[i + 1][j + 1]) {
						sum++;
					}
				}
				if (j == (cols - 1)) {
					if (arr[i][j] < arr[i][j - 1] && arr[i][j] < arr[i + 1][j] && arr[i][j] < arr[i + 1][j - 1]) {
						sum++;
					}
				}
				if (arr[i][j] < arr[i][j - 1] && arr[i][j] < arr[i][j + 1] && arr[i][j] < arr[i + 1][j] && arr[i][j] < arr[i + 1][j - 1] && arr[i][j] < arr[i + 1][j + 1]) {
					sum++;
				}
			}
			else if (i == (rows - 1)) {
				if (j == 0) {
					if (arr[i][j] < arr[i - 1][j] && arr[i][j] < arr[i][j + 1] && arr[i][j] < arr[i - 1][j + 1]) {
						sum++;
					}
				}
				if (j == (cols - 1)) {
					if (arr[i][j] < arr[i][j - 1] && arr[i][j] < arr[i - 1][j] && arr[i][j] < arr[i - 1][j - 1]) {
						sum++;
					}
				}
				if (arr[i][j] < arr[i][j - 1] && arr[i][j] < arr[i][j + 1] && arr[i][j] < arr[i - 1][j] && arr[i][j] < arr[i - 1][j - 1] && arr[i][j] < arr[i - 1][j + 1]) {
					sum++;
				}
			}
			else if (j == 0) {
				if (arr[i][j] < arr[i - 1][j] && arr[i][j] < arr[i + 1][j] && arr[i][j] < arr[i][j + 1] && arr[i][j] < arr[i - 1][j + 1] && arr[i][j] < arr[i + 1][j + 1]) {
					sum++;
				}
			}
			else if (j == (cols - 1)) {
				if (arr[i][j] < arr[i - 1][j] && arr[i][j] < arr[i + 1][j] && arr[i][j] < arr[i - 1][j - 1] && arr[i][j] < arr[i][j - 1] && arr[i][j] < arr[i + 1][j - 1]) {
					sum++;
				}
			}
			else {
				if (arr[i][j] < arr[i - 1][j - 1] && arr[i][j] < arr[i - 1][j] && arr[i][j] < arr[i - 1][j + 1] && arr[i][j] < arr[i][j + 1] && arr[i][j] < arr[i + 1][j + 1] && arr[i][j] < arr[i + 1][j] && arr[i][j] < arr[i + 1][j - 1] && arr[i][j] < arr[i][j - 1]) {
					sum++;
				}
			}

		}
	}

	cout << (" оличество локальных минимумов равно: ") << sum << endl;








	///////////////////////////////////////////////

	for (int i = 0; i < rows; i++) {//удаление двумерного динамического массива
		delete[] arr[i];
	}
	delete[] arr;
	return 0;

	//int** arr = new int* [rows];//создан двумерный динамический массив
	//for (int i = 0; i < rows; i++) {
	//	arr[i] = new int[cols];
	//}

	/////////////////////////////////////////////////
	/////////////////////////////////////////////////

	//for (int i = 0; i < rows; i++) {//удаление двумерного динамического массива
	//	delete[] arr[i];
	//}
	//delete[] arr;


}
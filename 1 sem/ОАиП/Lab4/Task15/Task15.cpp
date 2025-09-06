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

	int n, m = 0, p = 1, cols = 1, rows = 2, p2 = 2, num = 2;


	cout << ("Введите ваше число: ");
	n = check(0);

	double** arr = new double* [n];//создан двумерный динамический массив
	for (int i = 0; i < n; i++) {
		arr[i] = new double[n];
	}

	for (int i = 0; i < n; i++) {// заполнение динамического массива
		for (int j = 0; j < n; j++) {
			arr[i][j] = 0;
		}
	}

	arr[0][0] = 1;

	while (true) {
		for (int i = 0; i < cols; i++) {
			arr[m][p] = num;
			num++;
			m++;
		}
		for (int k = 0; k < rows; k++) {
			arr[m][p] = num;
			num++;
			p--;
		}
		if ((m == (n - 1) && p == -1)) {
			break;
		}
		m = 0;
		p = p2;
		p2++;
		cols++;
		rows++;
	}

	int save1 = 0, save2 = 0;
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			if (arr[i][j] == n) {
				save1 = i;
				save2 = j;
			}
			/*cout << arr[i][j] << '\t';*/
		}
		/*	cout << endl;*/
	}
	cout << endl;

	for (int i = 0; i < rows; i++) {//вывод динамического массива
		for (int j = 0; j < cols; j++) {
			cout << arr[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;
	cout << ("Ваше число находится в ") << save1 + 1 << (" строке и ") << save2 + 1 << (" столбце.\n");


	for (int i = 0; i < n; i++) {//удаление двумерного динамического массива
		delete[] arr[i];
	}
	delete[] arr;
	return 0;

}
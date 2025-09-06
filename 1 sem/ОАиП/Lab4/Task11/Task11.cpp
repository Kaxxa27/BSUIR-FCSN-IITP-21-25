#include <iostream>

using namespace std;


//int summ(int* sum ,int* n,int*sum1,int*** arr,int*m) {
//	int i = 0;//рекурсия на проход по главным диагоналям
//
//	if (*m == *n) {
//		return *sum;
//	}
//	for (; i < (*n); i++) {
//		
//		(*sum) += arr[*m][i][i];
//	}
//	if ((*sum) > (*sum1)) {
//		(*sum1) = (*sum);
//		
//	}
//	*sum = 0;
//	
//	(*m)++;
//	return summ( sum, n, sum1, arr, m);
//}
//int summ2(int* sum2, int* n, int* sum3, int*** arr, int* k) {
//	int i = *n-1,j = 0;//рекурсия на проход по побочным диагоналям
//
//	if (*k == *n) {
//		return *sum2;
//	}
//	for (; i >= 0, j < *n; i--,j++) {
//
//		(*sum2) += arr[*k][i][j];
//	}
//	if ((*sum2) > (*sum3)) {
//		(*sum3) = (*sum2);
//
//	}
//	*sum2 = 0;
//
//	(*k)++;
//	return summ2(sum2, n, sum3, arr, k);
//}
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

	int n, sum = 0, sum1 = 0, m = 0;
	int k = 0, sum2 = 0, sum3 = 0;

	cout << ("Дан трехмерный массив.\n");
	cout << ("Введите размер массива (n,n,n) : ");

	n = check(0);

	int*** arr = new int** [n];//выделение памяти под трехмерный массив
	for (int i = 0; i < n; i++) {
		arr[i] = new int* [n];
		for (int j = 0; j < n; j++) {
			arr[i][j] = new int[n];
		}
	}
	//////////////////////////////////


	for (int i = 0; i < n; i++) {//заполнение трехмерного массива
		for (int j = 0; j < n; j++) {
			for (int k = 0; k < n; k++) {
				cout << ("N") << i + 1 << j + 1 << k + 1 << (" = \n");
				arr[i][j][k] = rand() % 10;
			}
		}
	}
	cout << endl;


	for (int i = 0; i < n; i++) {//вывод трехмерного массива
		for (int j = 0; j < n; j++) {
			for (int k = 0; k < n; k++) {
				cout << arr[i][j][k] << "\t";
			}
			cout << endl;
		}
		cout << endl;
	}
	cout << endl;

	for (int i = 0; i < n; i++) {//первая диагональ куба
		sum += arr[i][i][i];
	}
	cout << ("Сумма первой диагонали куба равна: ") << sum << endl;

	for (int i = 0, j = n - 1; i < n; i++, j--) {//вторая диагональ куба
		sum1 += arr[i][i][j];
	}
	cout << ("Сумма второй диагонали куба равна: ") << sum1 << endl;

	for (int i = 0, j = n - 1; i < n; i++, j--) {//третья диагональ куба
		sum2 += arr[j][i][i];
	}
	cout << ("Сумма третьей диагонали куба равна: ") << sum2 << endl;

	for (int i = 0, j = n - 1; i < n; i++, j--) {//четвертая диагональ куба
		sum3 += arr[j][i][j];
	}
	cout << ("Сумма чертвертой диагонали куба равна: ") << sum3 << endl;

	if (sum >= sum1) {
		sum1 = sum;
	}
	if (sum2 >= sum3) {
		sum3 = sum2;
	}
	if (sum1 >= sum3) {
		sum3 = sum1;
	}
	cout << ("Наибольшая сумма из чисел диагоналей равна: ") << sum3 << endl;

	/*
	summ(&sum, &n, &sum1, arr, &m);
	summ2(&sum2, &n, &sum3, arr, &k);
	if (sum1 > sum3) {
		sum3 = sum1;
	}
	cout << ("Наибольшая сумма из чисел диагоналей равна: ") << sum3 << endl;*/

	///////////////////////////////////

	for (int i = 0; i < n; i++) {//очистка от трехмерного массива
		for (int j = 0; j < n; j++) {
			delete[] arr[i][j];
		}
		delete[] arr[i];
	}
	delete[] arr;
	return 0;


}
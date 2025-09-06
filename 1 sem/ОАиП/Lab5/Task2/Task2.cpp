#include <iostream>
#include <cmath>
#include <Windows.h>

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
//int odna_tret(int* arr, int* n, int* count, int*i) {
//	if (*i >= *n / 3) {
//		return 0;
//	}
//	if (cos(pow(arr[*i], 2)) > 0 && arr[*i] < 0) {
//			(*count)++;
//	}
//	(*i)++;
//
//	return odna_tret(arr, n, count,i);
//}
//int dwe_treti(int* arr, int* n, int* count, int* i) {
//	if (*i >= *n) {
//		return 0;
//	}
//	if (cos(pow(arr[*i], 2)) > 0 && arr[*i] < 0) {
//		(*count)++;
//	}
//	(*i)++;
//
//	return dwe_treti(arr, n, count, i);
//}
int main() {
	
	
	setlocale(LC_CTYPE, "rus");

	int n, count = 0, i = 0;

	HINSTANCE load;
	load = LoadLibrary(L"DynamicLib.dll");
	typedef int (*odna_tret)(int* arr, int* n, int* count, int* i);
	odna_tret odna_tret1;
	odna_tret1 = (odna_tret)GetProcAddress(load, "odna_tret");

	HINSTANCE load2;
	load2 = LoadLibrary(L"DynamicLib.dll");
	typedef int (*dwe_treti)(int* arr, int* n, int* count, int* i);
	odna_tret dwe_treti1;
	dwe_treti1 = (dwe_treti)GetProcAddress(load, "dwe_treti");

	cout << ("Введите размер одномерного массива: ");
	n = check();

	int* arr = new int[n];

	for (int i = 0; i < n; i++) {
		arr[i] = check();
	}
	cout << endl << ("Массив: ");
	for (int i = 0; i < n; i++) {
		cout << arr[i] << (" ");
	}
	cout << endl << endl;

	

	odna_tret1(arr, &n, &count, &i);
	dwe_treti1(arr, &n, &count, &i);


	cout << ("Количество элементов массива, для которых выполняется условие (cos(B[i]^2) > 0) и (B[i] < 0) равно: ");
	cout << count << endl;

	delete[] arr;
	FreeLibrary(load);
	FreeLibrary(load2);
	return 0;
}
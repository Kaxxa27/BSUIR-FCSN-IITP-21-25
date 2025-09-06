#include <iostream>
#include <cmath>
#define M_PI 3.14159265358979323846
#define E 2.71828182845904523536
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
void foo(double* arr, int*n) {
	for (int i = 0; i < *n; i++) {
		arr[i] = pow(E, (-0.5 * i - 2 * M_PI)) - atan(i + 0.1);
	}
	for (int i = 0; i < *n; i++) {
		cout << arr[i] << endl;
	}
	cout << endl;
	for (int i = 0; i < *n; i++) {
		if (arr[i] < 0) {
			arr[i] = arr[i] * 0.5;
		}
		else {
			arr[i] = 0.1;
		}
		cout << arr[i] << endl;
	}
}
int main(){

	setlocale(LC_CTYPE, "rus");

	int n,a;
	
	cout << ("Введите размер массива: ");
	n = check();

	double* arr = new double[n];

	foo(arr, &n);

	delete[] arr;
	return 0;
}
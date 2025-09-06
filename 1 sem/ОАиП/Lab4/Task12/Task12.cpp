#include <iostream>
#include <stdlib.h>

using namespace std;

void del(int* n, int* arr, int* arr2, int* arr3, int* count1) {
	for (int i = 0; i < *n; i++) {
		arr2[i] = 0;
	}
	int j = 0;
	for (int i = 0; i < *n; i++) {
		if (!arr2[arr[i]]) // если не повторяется
		{
			arr3[j++] = arr[i]; //сохранить результат
			arr2[arr[i]] = 1;
			(*count1)++;
		}
	}

	for (int i = 0; i < *count1; i++) {
		cout << arr3[i] << " ";
	}
	cout << endl << ("Размер нового массива равен: ") << *count1 << endl;

}
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

	int n, count = 0, count1 = 0, countnew = 0;

	cout << ("Дан одномерный массив.\n");
	cout << ("Введите размер массива (n) : ");

	n = check(0);

	int* arr = (int*)malloc(n * sizeof(int*));

	int* arr2 = (int*)malloc(n * sizeof(int*));

	int* arr3 = (int*)malloc(n * sizeof(int*));

	for (int i = 0; i < n; i++) {
		arr[i] = rand() % 10;
		count++;
	}

	for (int i = 0; i < n; i++) {
		cout << arr[i] << " ";
	}
	cout << endl;
	cout << ("Размер массива: ") << count << endl;





	del(&n, arr, arr2, arr3, &count1);


	free(arr);
	free(arr2);
	free(arr3);
	return 0;
}
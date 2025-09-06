#include <iostream>

using namespace std;


int main() {

	setlocale(LC_CTYPE, "rus");

	int N, M;

	cout << ("Дано поле для игры в сапёра размерами NxM.\n");
	cout << ("Введите N: ");
	cin >> N;
	cout << ("Введите M: ");
	cin >> M;

	char** arr = (char**)malloc(N * sizeof(char**));//Выделение памяти под массив ( через си ) 
	for (int i = 0; i < N; i++) {
		arr[i] = (char*)malloc(M * sizeof(char*));
	}

	for (int i = 0; i < N; i++) {// заполнение динамического массива A
		for (int j = 0; j < M; j++) {
			cout << ("Введите значение клетки  ") << i + 1 << j + 1 << (" : ");
			cin >> arr[i][j];

		}
	}


	for (int i = 0; i < N; i++) {//вычисление локальных минимумов
		for (int j = 0; j < M; j++) {
			int k = 0;
			if (i == 0) {//первая строка равна нулю
				if (j == 0) {//элемент 1.1
					if (arr[i][j] == '*') {

					}
					else {
						if (arr[i][j + 1] == '*') {
							k++;
						}
						if (arr[i + 1][j + 1] == '*') {
							k++;
						}
						if (arr[i + 1][j] == '*') {
							k++;
						}
						arr[i][j] = (char)((int)arr[i][j] + 2 + k);
					}
				}
				else if (j == (M - 1)) {
					if (arr[i][j] == '*') {

					}
					else {
						if (arr[i][j - 1] == '*') {
							k++;
						}
						if (arr[i + 1][j] == '*') {
							k++;
						}
						if (arr[i + 1][j - 1] == '*') {
							k++;
						}
						arr[i][j] = (char)((int)arr[i][j] + 2 + k);
					}

				}
				else {
					if (arr[i][j] == '*') {

					}
					else {
						if (arr[i][j - 1] == '*') {
							k++;
						}
						if (arr[i][j + 1] == '*') {
							k++;
						}
						if (arr[i + 1][j] == '*') {
							k++;
						}
						if (arr[i + 1][j - 1] == '*') {
							k++;
						}
						if (arr[i + 1][j + 1] == '*') {
							k++;
						}
						arr[i][j] = (char)((int)arr[i][j] + 2 + k);
					}

				}
			}
			else if (i == (N - 1)) {
				if (j == 0) {
					if (arr[i][j] == '*') {

					}
					else {
						if (arr[i - 1][j] == '*') {
							k++;
						}
						if (arr[i][j + 1] == '*') {
							k++;
						}
						if (arr[i - 1][j + 1] == '*') {
							k++;
						}
						arr[i][j] = (char)((int)arr[i][j] + 2 + k);
					}

				}
				else if (j == (M - 1)) {
					if (arr[i][j] == '*') {

					}
					else {
						if (arr[i][j - 1] == '*') {
							k++;
						}
						if (arr[i - 1][j] == '*') {
							k++;
						}
						if (arr[i - 1][j - 1] == '*') {
							k++;
						}
						arr[i][j] = (char)((int)arr[i][j] + 2 + k);
					}

				}
				else {
					if (arr[i][j] == '*') {

					}
					else {
						if (arr[i][j - 1] == '*') {
							k++;
						}
						if (arr[i][j + 1] == '*') {
							k++;
						}
						if (arr[i - 1][j] == '*') {
							k++;
						}
						if (arr[i - 1][j - 1] == '*') {
							k++;
						}
						if (arr[i - 1][j + 1] == '*') {
							k++;
						}
						arr[i][j] = (char)((int)arr[i][j] + 2 + k);
					}
				}

			}
			else if (j == 0) {
				if (arr[i][j] == '*') {

				}
				else {
					if (arr[i - 1][j] == '*') {
						k++;
					}
					if (arr[i + 1][j] == '*') {
						k++;
					}
					if (arr[i][j + 1] == '*') {
						k++;
					}
					if (arr[i - 1][j + 1] == '*') {
						k++;
					}
					if (arr[i + 1][j + 1] == '*') {
						k++;
					}
					arr[i][j] = (char)((int)arr[i][j] + 2 + k);
				}
			}
			else if (j == (M - 1)) {
				if (arr[i][j] == '*') {

				}
				else {
					if (arr[i - 1][j] == '*') {
						k++;
					}
					if (arr[i + 1][j] == '*') {
						k++;
					}
					if (arr[i - 1][j - 1] == '*') {
						k++;
					}
					if (arr[i][j - 1] == '*') {
						k++;
					}
					if (arr[i + 1][j - 1] == '*') {
						k++;
					}
					arr[i][j] = (char)((int)arr[i][j] + 2 + k);
				}
			}
			else {
				if (arr[i][j] == '*') {

				}
				else {
					if (arr[i - 1][j - 1] == '*') {
						k++;
					}
					if (arr[i - 1][j] == '*') {
						k++;
					}
					if (arr[i - 1][j + 1] == '*') {
						k++;
					}
					if (arr[i][j + 1] == '*') {
						k++;
					}
					if (arr[i + 1][j + 1] == '*') {
						k++;
					}
					if (arr[i + 1][j] == '*') {
						k++;
					}
					if (arr[i + 1][j - 1] == '*') {
						k++;
					}
					if (arr[i][j - 1] == '*') {
						k++;
					}
					arr[i][j] = (char)((int)arr[i][j] + 2 + k);
				}
			}
		}
	}







	for (int i = 0; i < N; i++) {//вывод динамического массива 
		for (int j = 0; j < M; j++) {
			cout << arr[i][j] << "\t";
		}
		cout << endl;
	}
	cout << endl;


	for (int i = 0; i < N; i++) {//очистка памяти массива
		free(arr[i]);
	}
	free(arr);
	return 0;
}

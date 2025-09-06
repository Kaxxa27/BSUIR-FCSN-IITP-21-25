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
void nechet(int** arr, int n) {
	int nsqr = n * n;
	int i = 0, j = n / 2;



	for (int k = 1; k <= nsqr; ++k)
	{
		arr[i][j] = k;
		i--;
		j++;
		if (k % n == 0)
		{
			i += 2;
			--j;
		}
		else
		{
			if (j == n)
			{
				j -= n;
			}
			else if (i < 0)
			{
				i += n;
			}
		}
	}
}

int main()
{
	setlocale(LC_CTYPE, "rus");

	int n;

	cout << ("Введите размерность квадрата: ");
	n = check(0);

	unsigned int start_time = clock();
	int** arr = (int**)calloc(2 * (n - 1), sizeof(int**));//для изменения размерности
	for (int i = 0; i < 2 * (n - 1); i++) {
		arr[i] = (int*)calloc(n, sizeof(int*));
	}

	////////////////////////////////////////////////////
	if (!(n % 2 == 0)) {// нечетная размерность
		nechet(arr, n);
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				cout << arr[i][j];
			}

		}
	}
	///////////////////////////////////////////////////

	else if (n % 4 == 0) { // для четной размерности
		int rows = n / 2 - 1, cols = 0, chislo = 0;
		bool r = true;

		while (chislo < n * n) {
			if (r) {
				r = false;
				int i = rows;
				int j = cols;
				chislo++;
				arr[i][j] = chislo;
				for (int k = 0; k < n / 2 - 1; k++) {
					i--;
					j++;
					chislo++;
					arr[i][j] = chislo;
				}
				j++;
				chislo++;
				arr[i][j] = chislo;
				for (int k = 0; k < n / 2 - 1; k++) {
					i++;
					j++;
					chislo++;
					arr[i][j] = chislo;
				}
				i++;
				chislo++;
				arr[i][j] = chislo;
				for (int k = 0; k < n / 2 - 1; k++) {
					i++;
					j--;
					chislo++;
					arr[i][j] = chislo;
				}
				j--;
				chislo++;
				arr[i][j] = chislo;
				for (int k = 0; k < n / 2 - 1; k++) {
					i--;
					j--;
					chislo++;
					arr[i][j] = chislo;
				}
				rows = i + 1;
				cols = n - 1;
			}
			else {
				r = true;
				int i = rows;
				int j = cols;
				chislo++;
				arr[i][j] = chislo;
				for (int k = 0; k < n / 2 - 1; k++) {
					i--;
					j--;
					chislo++;
					arr[i][j] = chislo;
				}
				j--;
				chislo++;
				arr[i][j] = chislo;
				for (int k = 0; k < n / 2 - 1; k++) {
					i++;
					j--;
					chislo++;
					arr[i][j] = chislo;
				}
				i++;
				chislo++;
				arr[i][j] = chislo;
				for (int k = 0; k < n / 2 - 1; k++) {
					i++;
					j++;
					chislo++;
					arr[i][j] = chislo;
				}
				j++;
				chislo++;
				arr[i][j] = chislo;
				for (int k = 0; k < n / 2 - 1; k++) {
					i--;
					j++;
					chislo++;
					arr[i][j] = chislo;
				}
				rows = i + 1;
				cols = 0;
			}
		}

		for (int i = 0; i < 2 * (n - 1); i++) {
			for (int j = 0; j < n; j++) {
				if (!(i >= (n / 2 - 1) && i <= (n / 2 - 2 + n)) && arr[i][j] != 0) {
					if (i < n / 2) {
						arr[i][j] = arr[i][j] ^ arr[i + n][j];
						arr[i + n][j] = arr[i][j] ^ arr[i + n][j];
						arr[i][j] = arr[i][j] ^ arr[i + n][j];
					}
					else {
						arr[i][j] = arr[i][j] ^ arr[i - n][j];
						arr[i - n][j] = arr[i][j] ^ arr[i - n][j];
						arr[i][j] = arr[i][j] ^ arr[i - n][j];
					}
				}
			}
		}

		for (int i = n / 2 - 1; i < n / 2 - 1 + n; i++) {
			for (int j = 0; j < n; j++) {
				cout << arr[i][j];
			}

		}

	}

	//////////////////////////////////////////////

	else { // для размерности четно-нечетного 

		int** arr2 = (int**)malloc((n / 2) * sizeof(int**));
		for (int i = 0; i < n / 2; i++) {
			arr2[i] = (int*)malloc((n / 2) * sizeof(int*));
		}
		nechet(arr2, n / 2);

		for (int i = 0; i < n / 2; i++) {
			for (int j = 0; j < n / 2; j++) {
				arr[i][j] = arr2[i][j];
			}
		}

		for (int i = n / 2, ii = 0; i < n; i++, ii++) {
			for (int j = n / 2, jj = 0; j < n; j++, jj++) {
				arr[i][j] = arr2[ii][jj] + (n / 2) * (n / 2);
			}
		}

		for (int i = 0, ii = 0; i < n / 2; i++, ii++) {
			for (int j = n / 2, jj = 0; j < n; j++, jj++) {
				arr[i][j] = arr2[ii][jj] + 2 * (n / 2) * (n / 2);
			}
		}

		for (int i = n / 2, ii = 0; i < n; i++, ii++) {
			for (int j = 0, jj = 0; j < n / 2; j++, jj++) {
				arr[i][j] = arr2[ii][jj] + 3 * (n / 2) * (n / 2);
			}
		}

		arr[0][0] = arr[0][0] ^ arr[n / 2][0];
		arr[n / 2][0] = arr[0][0] ^ arr[n / 2][0];
		arr[0][0] = arr[0][0] ^ arr[n / 2][0];

		arr[n / 2 - 1][0] = arr[n / 2 - 1][0] ^ arr[n - 1][0];
		arr[n - 1][0] = arr[n / 2 - 1][0] ^ arr[n - 1][0];
		arr[n / 2 - 1][0] = arr[n / 2 - 1][0] ^ arr[n - 1][0];

		for (int i = 1; i < n / 2 - 1; i++) {
			arr[i][1] = arr[i][1] ^ arr[i + n / 2][1];
			arr[i + n / 2][1] = arr[i][1] ^ arr[i + n / 2][1];
			arr[i][1] = arr[i][1] ^ arr[i + n / 2][1];
		}

		for (int j = n / 2 - ((n / 2 - 3) / 2); j < n / 2 + ((n / 2 - 3) / 2); j++) {
			for (int i = 0; i < n / 2; i++) {
				arr[i][j] = arr[i][j] ^ arr[i + n / 2][j];
				arr[i + n / 2][j] = arr[i][j] ^ arr[i + n / 2][j];
				arr[i][j] = arr[i][j] ^ arr[i + n / 2][j];
			}
		}

		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				cout << arr[i][j];
			}

		}


		for (int i = 0; i < n / 2; i++) {//удаление измененного массива
			free(arr2[i]);
		}
		free(arr2);
	}
	////////////////////////////////////////////////////////////

	for (int i = 0; i < n; i++) {//удаление основного массива
		free(arr[i]);
	}
	free(arr);

	unsigned int end_time = clock();

	unsigned int search_time = end_time - start_time;

	cout << endl << search_time << endl;

	return 0;
}
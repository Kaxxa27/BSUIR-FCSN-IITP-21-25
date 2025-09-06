#include <iostream>
#include<iomanip>
#define size 5

using namespace std;

int main()
{
	cout << fixed << setprecision(10);	//исходные данные
	double C[size][size] = {
		{0.2, .0, 0.2, .0, 0.2},
		{.0, 0.2, .0, 0.2, .0},
		{0.2, .0, 0.2, .0, 0.2},
		{.0, 0.2, .0, 0.2, .0},
		{.0, .0, 0.2, .0, 0.2},
	};
	double  D[size][size] = {
		{2.33, 0.81, 0.67, 0.92, -0.53},
		{-0.53, 2.33, 0.81, 0.67, 0.92},
		{0.92, -0.53, 2.33, 0.81, 0.67},
		{0.67, 0.92, -0.53, 2.33, 0.81},
		{0.81, 0.67, 0.92, -0.53, 2.33},
	};
	double b[size] = { 4.2, 4.2, 4.2, 4.2, 4.2 };

	double res[size] = { .0 };
	double k = 7., tmp = .0;

	//матрица А
	double A[size][size] = { 0 };

	//первая часть
	//Метод Гаусса

	//вычисление матрицы А
	for (int i = 0; i < size; i++) {
		for (int j = 0; j < size; j++) {
			A[i][j] = k * C[i][j] + D[i][j];
		}
	}

	cout << "The original matrix:\n";

	//вывод матрицы А
	for (int i = 0; i < size; i++) {
		for (int j = 0; j < size; j++) {
			cout << A[i][j] << " ";
		}
		cout << "\n";
	}


	cout << "\nThe transformed matrix:\n";

	//приведение к треугольному виду
	for (int i = 0; i < size; i++) {
		for (int j = i + 1; j < size; j++) {
			tmp = A[j][i] / A[i][i];
			b[j] -= tmp * b[i];
			A[j][i] = 0;
 			for (int l = i + 1; l < size; l++) {
				A[j][l] -= tmp * A[i][l];
			}
		}
	}

	//вывод матрицы А
	for (int i = 0; i < size; i++) {
		for (int j = 0; j < size; j++) {
			cout << A[i][j] << " ";
		}
		cout << "|" << b[i];
		cout << "\n";
	}

	//поиск решения
	for (int i = size - 1; i >= 0; i--) {
		for (int j = i; j < size; j++) {
			if (j == size - 1) break;
			else b[i] -= A[i][j + 1] * res[j + 1];
		}
		res[i] = b[i] / A[i][i];
	}



	cout << "\nRoots of the solution:";

	//вывод ответа
	for (int j = 0; j < size; j++) {
		cout << "\n" << res[j];
	}


	//вторая часть 
	//Метод Гаусса с выбором главного элемента по столбцу

	//вычисление матрицы А
	for (int i = 0; i < size; i++) {
		for (int j = 0; j < size; j++) {
			A[i][j] = k * C[i][j] + D[i][j];
		}
	}

	for (int i = 0; i < size; i++) b[i] = 4.2;

	//поиск главного по столбцу элемента
	for (int i = 0; i < size; i++) {
		int max_index = i;
		double max = A[i][i];
		for (int j = i + 1; j < size; j++) {
			if (fabs(max) < fabs(A[j][i])) {
				max_index = j;
				max = A[j][i];
			}
		}

		//перестановка строк местами
		if (i != max_index) {
			double temp_root = b[i];
			b[i] = b[max_index];
			b[max_index] = temp_root;
			for (int j = i; j < size; j++) {
				double temp = A[i][j];
				A[i][j] = A[max_index][j];
				A[max_index][j] = temp;
			}
		}

		//приведение к треугольному виду
		for (int j = i + 1; j < size; j++) {
			tmp = A[j][i] / A[i][i];
			b[j] -= tmp * b[i];
			A[j][i] = 0;
			for (int l = i + 1; l < size; l++) {
				A[j][l] -= tmp * A[i][l];
			}
		}
	}

	cout << "\n\n\nThe transformed matrix:\n";

	//вывод матрицы А
	for (int i = 0; i < size; i++) {
		for (int j = 0; j < size; j++) {
			cout << A[i][j] << " ";
		}

		cout << "|" << b[i];
		cout << "\n";
	}

	//поиск решения
	for (int i = size - 1; i >= 0; i--) {
		for (int j = i; j < size; j++) {
			if (j == size - 1) break;
			else b[i] -= A[i][j + 1] * res[j + 1];
		}
		res[i] = b[i] / A[i][i];
	}

	cout << "\nRoots of the solution:";

	//вывод ответа
	for (int j = 0; j < size; j++) {
		cout << "\n" << res[j];
	}

	//третья часть
	//Метод Гаусса с выбором главного элемента по всей матрице

		//вычисление матрицы А
	for (int i = 0; i < size; i++) {
		for (int j = 0; j < size; j++) {
			A[i][j] = k * C[i][j] + D[i][j];
		}
	}

	for (int i = 0; i < size; i++) b[i] = 4.2;

	//поиск главного элемента по всей матрице
	for (int i = 0; i < size; i++) {
		int max_index = i;
		double max = A[i][i];
		for (int j = i; j < size; j++) {
			for (int k = i; k < size; k++) {
				if (fabs(max) < fabs(A[j][k])) {
					max_index = j;
					max = A[j][k];
				}
			}
		}

		//перестановка строк местами
		if (i != max_index) {
			double temp_root = b[i];
			b[i] = b[max_index];
			b[max_index] = temp_root;
			for (int j = i; j < size; j++) {
				double temp = A[i][j];
				A[i][j] = A[max_index][j];
				A[max_index][j] = temp;
			}
		}

		//приведение к треугольному виду
		for (int j = i + 1; j < size; j++) {
			tmp = A[j][i] / A[i][i];
			b[j] -= tmp * b[i];
			A[j][i] = 0;
			for (int l = i + 1; l < size; l++) {
				A[j][l] -= tmp * A[i][l];
			}
		}
	}

	cout << "\n\n\nThe transformed matrix:\n";

	//вывод матрицы А
	for (int i = 0; i < size; i++) {
		for (int j = 0; j < size; j++) {
			cout << A[i][j] << " ";
		}

		cout << "|" << b[i];
		cout << "\n";
	}

	//поиск решения
	for (int i = size - 1; i >= 0; i--) {
		for (int j = i; j < size; j++) {
			if (j == size - 1) break;
			else b[i] -= A[i][j + 1] * res[j + 1];
		}
		res[i] = b[i] / A[i][i];
	}

	cout << "\nRoots of the solution:";

	//вывод ответа
	for (int j = 0; j < size; j++) {
		cout << "\n" << res[j];
	}
}




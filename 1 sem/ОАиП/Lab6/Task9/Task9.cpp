#include <iostream>
#include <windows.h> 
#include <cmath>

using namespace std;
int SumChicel(char* str, int* str2, double& SUM) {
	int k = 0;
	double sum = 0.0;

	int del = 0;
	int DEL = 0;//конечный делитель/умножительXD
	int j = 1;

	for (int i = 0; i < strlen(str); i++) {
		if (str[i] == '\0') {
			break;
		}
		if (str[i] != '-') {
			if (isdigit(str[i])) {
				if (isdigit(str[i + 1])) {
					sum = (sum + (str[i] - 48)) * 10;
				}
				else if (str[i + 1] == 'e') {
					sum += str[i] - 48;
					if (str[i + 2] == '-') {
						for (;; j++) {
							if (isdigit(str[i + 2 + j])) {
								if (isdigit(str[i + 3 + j])) {
									del = (del + (str[i + 2 + j] - 48)) * 10;
								}
								else {

									DEL += (str[i + 2 + j] - 48) + del;
									del = 0;
									i += j + 3;//4
									j = 1;
									break;
								}
							}

						}
						sum *= pow(10, -DEL);
						DEL = 0;
						SUM += sum;
						sum = 0;
					}
					else if (str[i + 2] != '-' && str[i + 2] != isdigit(str[i + 2]) && str[i + 2] == '+') {
					}
					else {
						for (;; j++) {

							if (isdigit(str[i + 2 + k])) {
								if (isdigit(str[i + 3 + k])) {
									del = (del + (str[i + 2 + k] - 48)) * 10;
									k++;
								}
								else {
									DEL += (str[i + 2 + k] - 48) + del;
									del = 0;
									i += k + 2;
									k = 0;
									break;
								}
							}
						}
						sum *= pow(10, DEL);
						DEL = 0;
						SUM += sum;
						sum = 0;
					}
				}
				else {
					SUM += (str[i] - 48) + sum;
					sum = 0;
				}
			}
		}
		else {

			if (isdigit(str[i + 1])) {
				if (isdigit(str[i + 2])) {
					sum = -(sum + (str[i + 1] - 48)) * 10;
				}
				else if (str[i + 2] == 'e') {
					sum += (str[i + 1] - 48);
					if (str[i + 3] == '-') {
						for (;; j++) {
							if (isdigit(str[i + 3 + j])) {
								if (isdigit(str[i + 4 + j])) {
									del = (del + (str[i + 3 + j] - 48)) * 10;
								}
								else {
									DEL += (str[i + 3 + j] - 48) + del;
									del = 0;
									i += j + 3;
									j = 1;
									break;
								}
							}
						}
						sum *= -pow(10, -DEL);
						DEL = 0;
						SUM += sum;
						sum = 0;
					}
					else if (str[i + 3] != '-' && str[i + 3] != isdigit(str[i + 3]) && str[i + 3] == '+') {
					}
					else {
						for (;; j++) {

							if (isdigit(str[i + 3 + k])) {
								if (isdigit(str[i + 4 + k])) {
									del = (del + (str[i + 3 + k] - 48)) * 10;
								}
								else {
									DEL += (str[i + 3 + k] - 48) + del;
									del = 0;
									i += k + 2;
									k = 0;
									break;
								}
							}
						}
						sum *= -pow(10, DEL);
						DEL = 0;
						SUM += sum;
						sum = 0;
					}
				}
				else {
					SUM -= (str[i + 1] - 48) + sum;
					sum = 0;
					i++;
				}
			}


		}
	}
	return SUM;
}
int main() {

	setlocale(LC_CTYPE, "rus");

	SetConsoleCP(1251);

	char str[100];
	int str2[100];
	double SUM = 0.0;//конечна€ сумма

	cout << ("¬ведите строку: ");
	cin.getline(str, 100);

	SumChicel(str,str2,SUM);
	
	cout << SUM << endl;

	return 0;
}
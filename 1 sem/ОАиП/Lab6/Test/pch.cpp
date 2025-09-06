//
// pch.cpp
//

#include "pch.h"
#include <string>

int  polindrom(char** str2, char** str, bool& k, bool& p, bool& q, int& count, int& count1, int& count2) {

	for (int i = 0; i < 3; i++) {
		int dl;
		if (i == 0) {
			dl = strlen(str[i]) - count;
		}
		if (i == 1) {
			dl = strlen(str[i]) - count1;
		}
		if (i == 2) {
			dl = strlen(str[i]) - count2;
		}
		for (int j = 0; j < dl / 2; j++) {

			if (str2[i][j] != str2[i][dl - j - 1]) {
				if (i == 0) {
					k = false;
				}
				else if (i == 1) {
					p = false;
				}
				else if (i == 2) {
					q = false;
				}
				break;
			}
		}

	}
	return k, q, p;
}
char text(char* str, char* str2) {
	for (int i = 0, k = 0; i < 101; i++, k++) {

		if (str[i] == 'c') {
			if (str[i + 1] == 'e') {
				str2[k] = 's';
			}
			else if (str[i + 1] == 'i') {
				str2[k] = 's';
			}
			else if (str[i + 1] == 'y') {
				str2[k] = 's';
			}
			else {
				str2[k] = 'k';
			}
			if (str2[k] == str[i + 1]) {
				str2[k] = str[i + 1];
				i++;
			}
		}
		else if (str[i] == 'q' && str[i + 1] == 'u') {
			str2[k] = 'k';
			str2[k + 1] = 'v';
			k++;
			i++;
		}
		else if (str[i] == 'q') {
			str2[k] = 'k';
		}
		else if (str[i] == 'x') {
			str2[k] = 'k';
			str2[k + 1] = 's';
			k++;
		}
		else if (str[i] == 'w') {
			str2[k] = 'v';
		}
		else if (str[i] == 'p' && str[i + 1] == 'h') {
			str2[k] = 'f';
			i++;
		}
		else if (str[i] == 'y' && str[i + 1] == 'o' && str[i + 2] == 'u') {
			str2[k] = 'u';
			i += 2;
		}
		else if (str[i] == 'o' && str[i + 1] == 'o') {
			str2[k] = 'u';
			i++;
		}
		else if (str[i] == 'e' && str[i + 1] == 'e') {
			str2[k] = 'i';
			i++;
		}
		else if ((str[i] == 't' || str[i] == 'T') && str[i + 1] == 'h') {
			if (str[i] == 't') {
				str2[k] = 'z';
				i++;
			}
			else if (str[i] == 'T') {
				str2[k] = 'Z';
				i++;
			}

		}
		else if (str[i] == 'b' && str[i + 1] == 'b') {
			str2[k] = 'b';
			i++;
		}
		else if (str[i] == 'c' && str[i + 1] == 'c') {
			str2[k] = 'c';
			i++;
		}
		else if (str[i] == 'd' && str[i + 1] == 'd') {
			str2[k] = 'd';
			i++;
		}
		else if (str[i] == 'f' && str[i + 1] == 'f') {
			str2[k] = 'f';
			i++;
		}
		else if (str[i] == 'g' && str[i + 1] == 'g') {
			str2[k] = 'g';
			i++;
		}
		else if (str[i] == 'h' && str[i + 1] == 'h') {
			str2[k] = 'h';
			i++;
		}
		else if (str[i] == 'j' && str[i + 1] == 'j') {
			str2[k] = 'j';
			i++;
		}
		else if (str[i] == 'k' && str[i + 1] == 'k') {
			str2[k] = 'k';
			i++;
		}
		else if (str[i] == 'l' && str[i + 1] == 'l') {
			str2[k] = 'l';
			i++;
		}
		else if (str[i] == 'm' && str[i + 1] == 'm') {
			str2[k] = 'm';
			i++;
		}
		else if (str[i] == 'n' && str[i + 1] == 'n') {
			str2[k] = 'n';
			i++;
		}
		else if (str[i] == 'p' && str[i + 1] == 'p') {
			str2[k] = 'p';
			i++;
		}
		else if (str[i] == 'q' && str[i + 1] == 'q') {
			str2[k] = 'q';
			i++;
		}
		else if (str[i] == 'r' && str[i + 1] == 'r') {
			str2[k] = 'r';
			i++;
		}
		else if (str[i] == 's' && str[i + 1] == 's') {
			str2[k] = 't';
			i++;
		}
		else if (str[i] == 'v' && str[i + 1] == 'v') {
			str2[k] = 'v';
			i++;
		}
		else if (str[i] == 'w' && str[i + 1] == 'w') {
			str2[k] = 'w';
			i++;
		}
		else if (str[i] == 'x' && str[i + 1] == 'x') {
			str2[k] = 'x';
			i++;
		}
		else if (str[i] == 'z' && str[i + 1] == 'z') {
			str2[k] = 'z';
			i++;
		}
		else {
			str2[k] = str[i];
		}
	}
	return 0;
}
int DlinaPolindroma(char* str, int& chetchik, int& count, bool& m) {
	for (int i = 0, j = strlen(str) - 1; i < (strlen(str) / 2); i++) {

		if (str[i] == str[i + 1]) {
			chetchik++;
		}
		if (str[i] == str[j - i]) {
		}
		else {
			m = 0;
			break;
		}
	}
	if (chetchik == strlen(str) / 2) {
	
		return -1;
	}
	else {
		if (m == 1) {
			count = strlen(str) - 1;
		
			return count;
		}
		else if (m == 0) {
		
			return strlen(str);
		}
	}
}
double SumChicel(char* str, int* str2, double& SUM) {
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
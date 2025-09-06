#include <iostream>
#include <cmath>


using namespace std;

void i17(char* str2, int n,int& j) {
	switch (n) {
	case 10: {
		str2[j] = 'A';
		break;
	}
	case 11: {
		str2[j] = 'B';
		break;
	}
	case 12: {
		str2[j] = 'C';
		break;
	}
	case 13: {
		str2[j] = 'D';
		break;
	}
	case 14: {
		str2[j] = 'E';
		break;
	}
	case 15: {
		str2[j] = 'F';
		break;
	}
	case 16: {
		str2[j] = 'G';
		break;
	}
	default: {
		
		str2[j] = char(n+48);
		break;
	}
	}
}
void revers(char str[],char strnew[]) {
	int i = 0;
	for (; i < strlen(str); i++){
		strnew[i] = str[strlen(str) -1- i];
	}
	strnew[i] = '\0';
}
int main() {

	setlocale(LC_CTYPE, "rus");

	char str[100];

	cout << ("Введите ваше число: ");

	cin.getline(str,99);

	int count = 0;
	for ( int i = 0;  i < strlen(str); i++){
		if (str[i] == '.') {
			count = i;
		}
	}

	double sum10 = 0;
	int k = 1;
	if (count) {
		
		for (int i = 0; i < strlen(str); i++) {
			if (str[i] == '.') {
			}
			else {
				sum10 += (str[i] - 48) * pow(9, count - k);
				k++;
			}
		}
	}
	else {
		for (int i = 0; i < strlen(str); i++) {
			sum10 += (str[i] - 48) * pow(9, strlen(str)-k);
			k++;
		}
	}

	cout <<("Ваше число в 10-ой СС: ") << sum10 << endl;

	char str2[100]{'1'};
	char newstr2[100];

	char str3[100]{ '1' };
	char newstr3[100];

	int sum17i = sum10;
	double sum17d = sum10 - sum17i;

	

	if (sum17d == 0) {
		for (int j = 0;; j++) {

			 int n = sum17i % 17;

			i17(str2, n, j);
			sum17i /= 17;
			if (sum17i / 17 == 0) {

				j++;
				
				i17(str2,sum17i, j);
				break;
			}	
		}
		revers(str2, newstr2);
		cout << newstr2 << endl;
	}
	else {
		for (int j = 0;; j++) {

			int n = sum17i % 17;

			i17(str2, n, j);
			sum17i /= 17;
			if (sum17i / 17 == 0) {
				j++;
				i17(str2, sum17i, j);
				break;
			}
		}

		for (int j = 0; j < 3; j++) {

			int n = sum17d * 17;

			i17(str3, n, j);
			sum17d *= 17;
			sum17d -= n;
		
		}


		revers(str2, newstr2);

		int num = 0;
		for (; num < strlen(newstr2); num++) {
			newstr3[num] = newstr2[num];
		}
		newstr3[num] = '.';
		num++;
		for (int i = 0; i < strlen(str3); num++, i++) {
			newstr3[num] = str3[i];
		}
		newstr3[num] = '\0';
		cout << newstr3 << endl;
	}

	return 0;
}
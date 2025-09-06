#include <iostream>
#include <cmath>

using namespace std;

void ss17(int& N, char n) {
	switch (n) {
	case 'A': {
		N = 10;
		break;
	}
	case'B': {
		N = 11;
		break;
	}
	case 'C': {
		N = 12;
		break;
	}
	case 'D': {
		N = 13;
		break;
	}
	case 'E': {
		N = 14;
		break;
	}
	case 'F': {
		N = 15;
		break;
	}
	case 'G': {
		N = 16;
		break;
	}
	default: {

		N = n-48;
		break;
	}
	}
}
void i17(char* str2, int n, int& j) {
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

		str2[j] = char(n + 48);
		break;
	}
	}
}
void summa(int& N1, int& N2,int& Nost, char num1[], char num2[], char num3[],char SUM[], int& i, int& pr) {
	
	int sum = 0;
	if (pr == 0) {//1>2 => 2-3
		ss17(N1, num1[i]);
		ss17(N2, num2[i]);
		sum = N1 + N2 + Nost;
		if (sum > 17) {
			Nost = sum / 17;
			i17(SUM, sum % 17, i);
			
		}
		else if (sum == 17) {
			i17(SUM, sum % 17, i);
		}
		else {
			Nost = sum / 17;
			i17(SUM, sum, i);
		}
	}
	else if(pr == 1) {//1<2 => 1-3
		ss17(N1, num1[i]);
		ss17(N2, num3[i]);
		sum = N1 + N2 + Nost;
		if (sum > 17) {
			Nost = sum / 17;
			i17(SUM, sum % 17, i);
		
		}
		else if (sum == 17) {
			i17(SUM, sum % 17, i);
		}
		else {
			Nost = sum / 17;
			i17(SUM, sum, i);
		}
	}
	else if (pr = 2) {
		ss17(N1, num3[i]);
		ss17(N2, num2[i]);
		sum = N1 + N2 + Nost;
		if (sum > 17) {
			Nost = sum / 17;
			i17(SUM, sum % 17, i);
			
		}
		else if (sum == 17) {
			
			i17(SUM, sum % 17, i);
		}
		else {
			Nost = sum / 17;
			i17(SUM, sum, i);
		}
	}
}
void deduction(int& N1, int& N2, int& Nost, char num1[], char num2[], char num3[], char SUM[], int& i, int& pr, int& ost) {
	
	int ded = 0;
	
	if (pr == 0) {//1>2 => 2-3
		if (num1[0] - 48 > num2[0]-48) {
			ss17(N1, num1[i]);
			ss17(N2, num2[i]);
			if (N1 < N2) {
			
				ded = N1 - ost + 17 - N2;
				ost = 1;
				i17(SUM, ded, i);
			}
			else {
				ded = N1 - ost - N2;
				i17(SUM, ded, i);
				ost = 0;
			}
		}
		else {

			ss17(N1, num1[i]);
			ss17(N2, num2[i]);

			if (N2 < N1) {
				ded = N2 - ost + 17 - N1;
				ost = 1;
				i17(SUM, ded, i);
			}
			else {
				ded = N2 - ost - N1;
				i17(SUM, ded, i);
				ost = 0;
			}

		}
	}
	else if (pr == 1) {//1<2 => 1-3
		if (num1[0] - 48 > num3[0] - 48) {
			ss17(N1, num1[i]);
			ss17(N2, num3[i]);
			if (N1 < N2) {
				ded = N1 - ost + 17 - N2;
				ost = 1;
				i17(SUM, ded, i);
			}
			else {
				ded = N1 - ost - N2;
				i17(SUM, ded, i);
				ost = 0;
			}
		}
		else {
			ss17(N1, num1[i]);
			ss17(N2, num3[i]);

			if (N2 < N1) {
				ded = N2 - ost + 17 - N1;
				ost = 1;
				i17(SUM, ded, i);
			}
			else {
				ded = N2 - ost - N1;
				i17(SUM, ded, i);
				ost = 0;
			}

		}
	}
	else if (pr = 2) {
		if (num3[0] - 48 > num2[0] - 48) {
			ss17(N1, num3[i]);
			ss17(N2, num2[i]);
			if (N1 < N2) {
				ded = N1 - ost + 17 - N2;
				ost = 1;
				i17(SUM, ded, i);
			}
			else {
				ded = N1 - ost - N2;
				i17(SUM, ded, i);
				ost = 0;
			}
		}
		else {
			ss17(N1, num3[i]);
			ss17(N2, num2[i]);

			if (N2 < N1) {
				ded = N2 - ost + 17 - N1;
				ost = 1;
				i17(SUM, ded, i);
			}
			else {
				ded = N2 - ost - N1;
				i17(SUM, ded, i);
				ost = 0;
			}
		}
	}
}

int main() {

	setlocale(LC_CTYPE, "rus");

	char num1[10];
	char num2[10];
	char num3[10];// выравнивание
	char SUM[10];

	char ch[17] = { '0','1','2','3','4', '5','6' ,'7','8' ,'9','A', 'B','C','D','E','F','G' };
	char in[17] = { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16 };

	int N1 = 0;
	int N2 = 0;
	int Nost = 0;//остаток
	int ost = 0;

	cout << ("¬ведите ваше первое число(17): ");
	cin.getline(num1, 10);

	cout << ("¬ведите ваше второе число(17): ");
	cin.getline(num2, 10);

	int L = 0;
	int pr = 0;// 1 > 2
	if (strlen(num1) != strlen(num2)) {
		if (strlen(num1) > strlen(num2)) {
			pr = 1;
			L = strlen(num1) - strlen(num2);
			for (int i = 0; i < L; i++) {
				num3[i] = '0';
			}

			int i = L;
			for (int j = 0; i < L + strlen(num1); i++, j++) {
				num3[i] = num2[j];
			}
			num3[i] = '\0';

			for (int i = strlen(num1) - 1; i >= 0; i--) {
				summa(N1, N2, Nost, num1, num2, num3, SUM, i, pr);
			}

			SUM[strlen(num1)] = '\0';
			cout << ("Sum: ") << SUM << endl;

			for (int i = strlen(num1) - 1; i >= 0; i--) {
				deduction(N1, N2, Nost, num1, num2, num3, SUM, i, pr, ost);
			}

			SUM[strlen(num1)] = '\0';

			if (num1[0] - 48 > num3[0] - 48) {
				cout << ("Ded: ") << SUM << endl;
			}
			else {
				cout << ("Ded: -") << SUM << endl;
			}
		}
		else {
			pr = 2;
			L = strlen(num2) - strlen(num1);
			for (int i = 0; i < L; i++) {
				num3[i] = '0';
			}
			int i = L;
			for (int j = 0; i < L + strlen(num1); i++, j++) {
				num3[i] = num1[j];
			}
			num3[i] = '\0';

			for (int i = strlen(num2) - 1; i >= 0; i--) {
				summa(N1, N2, Nost, num1, num2, num3, SUM, i, pr);
			}

			SUM[strlen(num2)] = '\0';
			cout << ("Sum: ") << SUM << endl;

			for (int i = strlen(num2) - 1; i >= 0; i--) {
				deduction(N1, N2, Nost, num1, num2, num3, SUM, i, pr, ost);
			}

			SUM[strlen(num2)] = '\0';
			

			if (num3[0] - 48 > num2[0] - 48) {
				cout << ("Ded: ") << SUM << endl;
			}
			else {
				cout << ("Ded: -") << SUM << endl;
			}
		}

	}
	else {

		for (int i = strlen(num1) - 1; i >= 0; i--) {
			summa(N1, N2, Nost, num1, num2, num3, SUM, i, pr);
		}
		SUM[strlen(num1)] = '\0';
		cout << ("Sum: ") << SUM << endl;

		for (int i = strlen(num1) - 1; i >= 0; i--) {
			deduction(N1, N2, Nost, num1, num2, num3, SUM, i, pr, ost);
		}
		SUM[strlen(num1)] = '\0';
		
		if (num1[0] - 48 > num2[0] - 48) {
			cout << ("Ded: ") << SUM << endl;
		}
		else {
			cout << ("Ded: -") << SUM << endl;
		}
	}
	return 0;
}
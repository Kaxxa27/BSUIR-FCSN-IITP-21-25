#include <iostream>
#include <cmath>

using namespace std;

void revers(char str2[], char str[]) {
	int i = 0;
	for (; i < strlen(str2); i++) {
		str[i] = str2[strlen(str2) - 1 - i];
	}
	str[i] = '\0';
}

void dop_num(int num, char str2[],char str[]) {
	if (num < 0) {
		num += 1;
		str2[7] = '1';
		for (int i = 0; i < 7; i++) {
			str2[i] = char(abs(num) % 2 + 48);
			num /= 2;
		}
		revers(str2, str);
		for (int i = 1; i < 8; i++) {
			if (str[i] == '0') {
				str[i] = '1';
			}
			else {
				str[i] = '0';
			}
		}

		cout << ("„исло в дополнительном коде: ") << str << endl;

	}
	else {
		for (int i = 0; i < 8; i++) {
			str2[i] = char(abs(num) % 2 + 48);
			num /= 2;
		}
		revers(str2, str);
		cout << ("„исло в дополнительном коде: ") << str << endl;

	}
}

int odin_Func(char str2[], char str1[], char SUM[], int& i) {
	if (str1[i - 1] == '0' && str2[i - 1] == '0') {
		SUM[i - 1] = '1';
		i--;
		return 0;
	}
	else if (i < 0) {
		return 0;
	}
	else if ((str1[i - 1] == '1' && str2[i - 1] == '0') || (str1[i - 1] == '0' && str2[i - 1] == '1')) {
		SUM[i - 1] = '0';
		i--;
		return odin_Func(str2, str1, SUM, i);
	}
	else if (str1[i - 1] == '1' && str2[i - 1] == '1') {
		SUM[i - 1] = '1';
		i--;
		return odin_Func(str2, str1, SUM, i);
	}
}

int main() {

	setlocale(LC_CTYPE, "rus");

	int num1, num2;

	char str1[9]{};//первое двоичное доп число
	char str2[9]{};//второе двоичное доп число
	char str3[9]{};
	char SUM[10]{};//конечное сложение 
	char odin[10]{};//сохран€ющее 1

	cout << ("¬ведите ваше первое число: ");
	cin >> num1;

	cout << ("¬ведите ваше второе число: ");
	cin >> num2;

	dop_num(num1, str3, str1);

	dop_num(num2, str3, str2);

	for (int i = 7; i >= 0; i--) {
		if (str1[i] == '0' && str2[i] == '0') {
			SUM[i] = '0';
		}
		else if ((str1[i] == '1' && str2[i] == '0') || (str1[i] == '0' && str2[i] == '1')) {
			SUM[i] = '1';
		}
		else if (str1[i] == '1' && str2[i] == '1') {
			SUM[i] = '0';
			odin_Func(str1, str2, SUM, i);
		}
	}
	cout << ("—умма в доп. коде: ") << SUM << endl;

	int sum = 0;
	for (int i = 7, k = 0; i >=0; i--,k++) {
		sum += (SUM[i] - 48) * pow(2,k);
	}

	cout << ("ќтвет в естественной форме: ") << num1 + num2 << endl;

	return 0;

}
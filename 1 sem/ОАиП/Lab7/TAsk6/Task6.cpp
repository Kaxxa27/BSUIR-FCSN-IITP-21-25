#include <iostream>
#include <cmath>

using namespace std;

void revers(char str[], char strnew[]) {
	int i = 0;
	for (; i < strlen(str); i++) {
		strnew[i] = str[strlen(str) - 1 - i];
	}
	strnew[i] = '\0';
}
int summa(char str[], int& sum) {
	for (int i = 0; i < strlen(str); i++) {
		sum += str[i] - 48;
	}
	return sum;
}
void transl(int& num, char str1[], char newstr1[]) {
	
	int num1 = num + 1;
	int max = 0;
	int pr = 0;

	int i = 0;
	for (; i < 100; i++) {

		pr = num1 % (int)pow(10, i + 1);
		max = (num1 % (int)pow(10, i + 1) - max) / pow(10, i);

		str1[i] = char(max + 48);

		if (pr == num1) {
			break;
		}
	}
	str1[i + 1] = '\0';
	revers(str1, newstr1);

	/*cout << ("J + 1:") << newstr1 << endl;*/

}
void perevod(char str1[], char newstr1[], int& num) {
	
	int max = 0;
	int pr = 0;

	int i = 0;
	for (; i < 100; i++) {

		pr = num % (int)pow(10, i + 1);
		max = (num % (int)pow(10, i + 1) - max) / pow(10, i);

		str1[i] = char(max + 48);

		if (pr == num) {
			break;
		}
	}
	str1[i + 1] = '\0';
	revers(str1, newstr1);

	/*cout << ("J:")<<newstr1 << endl;*/
}

int main() {

	setlocale(LC_CTYPE, "rus");

	int n = 0;
	int t = 0;
	char str[100];
	char str1[100];
	char newstr1[100];

	cout << ("Введите количество наборов: ");
	cin >> t;

	while (t) {

		cout << ("Введите число n: ");
		cin >> n;

		int num = 0;

		int count = 0;
		int sum = 0;
		int sum1 = 0;

		for (int j = 1; j <= n; ++j) {
			perevod(str, str1, j);
			transl(j, str, newstr1);
			if (summa(newstr1, sum1) < summa(str1, sum)) {
				count++;
			}
			sum1 = 0;
			sum = 0;
		}

		cout << ("Кол-во чисел равн: ") << count << endl;//88005553 2,30 мин
		t--;
	}
	return 0;
}
//
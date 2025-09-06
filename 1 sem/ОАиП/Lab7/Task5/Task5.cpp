#include<iostream>
#include <string>


using namespace std;

int main() {

	setlocale(LC_CTYPE, "rus");

	int n;
	int i = 15;// last elements 

	cout << ("Справка:\n");
	cout << ("arab 5.000 => roman T\n");
	cout << ("arab 10.000 => roman O\n");
	cout << ("arab 1.000.000 => roman U\n");

	cout << ("Введите ваше число: ");
	cin >> n;

	string roman_num[] = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M","T","O","U"};
	int arab_num[] = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000,5000,10000,1000000 };

	cout << ("Риmское число: ");
	while (i >= 0) {
		if (n - arab_num[i] >= 0) {
			cout << roman_num[i];
			n -= arab_num[i];
		}
		else {
			i--;
		}
	}
	cout << endl;
	return 0;
}
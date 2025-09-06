#include<iostream>
#include<string>

using namespace std;
string i10(unsigned int n) {
	string s = "", c;
	if (n >= 10) {
		int n1;
		n1 = n + (n / 2 - 4) * 2;
		while (n1) {
			c = (n1 % 2) + '0';
			s = c + s;
			n1 /= 2;
		}
	}
	else {
		while (n) {
			c = (n % 2) + '0';
			s = c + s;
			n /= 2;
		}
	}

	return s;
}

int main() {

	setlocale(LC_CTYPE, "rus");

	cout << "¬ведите ваше число: ";
	unsigned int n;
	cin >> n;
	string s = i10(n);
	cout << s;
}

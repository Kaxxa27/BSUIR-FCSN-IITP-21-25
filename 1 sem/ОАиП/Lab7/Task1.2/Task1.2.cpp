#include <iostream>
#include <cmath>

using namespace std;

int i17(char n) {
	
	if (n == '0') {
		return 0;
	}
	else if (n == '1') {
		return 1;
	}
	else if (n == '2') {
		return 2;
	}
	else if (n == '3') {
		return 3;
	}
	else if (n == '4') {
		return 4;
	}
	else if (n == '5') {
		return 5;
	}
	else if (n == '6') {
		return 6;
	}
	else if (n == '7') {
		return 7;
	}
	else if (n == '8') {
		return 8;
	}
}
int i10() {
	char n;
	cin >> n;
	int len = cin.rdbuf()->in_avail();
	int num = i17(n);

	if (len != 1) {
		return num * pow(9, len - 1) + i10();
	}
	else {
		return num * pow(9, len - 1);
	}
}

char ch17(int ost) {
	if (ost == 10) {
		return 'A';
	}
	else if (ost == 11) {
		return 'B';
	}
	else if (ost == 12) {
		return 'C';
	}
	else if (ost == 13) {
		return 'D';
	}
	else if (ost == 14) {
		return 'E';
	}
	else if (ost == 15) {
		return 'F';
	}
	else if (ost == 16) {
		return 'G';
	}
	else {
		return char(ost+48);
	}
}
void semnad17(int num10) {

	
	int part, mnj, ost;
	part = num10 / 17;
	mnj = part * 17;
	ost = num10 - mnj;

	if (part == 0) {
		cout << ch17(ost);
		return;
	}

	semnad17(num10 /= 17);

	cout << ch17(ost);
}

int main() {

	setlocale(LC_CTYPE, "rus");

	int num10 = i10();

	cout << ("Ваше число в 10-ой СС: ") << num10 << endl;

	cout << ("Ваше число в 17-ой СС: ");
	
	semnad17(num10);

	cout << endl;

	return 0;
}
#include <iostream>
#include <windows.h> 

using namespace std;

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
	return k,q,p;
}
int main() {

	setlocale(LC_CTYPE, "rus");

	SetConsoleCP(1251);

	bool k = true;
	bool p = true;
	bool q = true;

	char** str = new char*[3];
	char** str2 = new char*[3];
	
	for (int i = 0; i < 3; i++) {
		str[i] = new char[100];
	}
	for (int i = 0; i < 3; i++) {
		str2[i] = new char[100];
	}
	for (int i = 0; i < 3; i++) {
		cin.getline(str[i], 100);
	}

	int h = 0;
	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < strlen(str[i]); j++) {
			if (str[i][j] != ' ') {
				str2[i][h] = str[i][j];
				h++;
			}
		}
		h = 0;
	}

	int count = 0;
	int count1 = 0;
	int count2 = 0;

	for (int i = 0; i < 3; i++) {
		for (int j = 0; j < strlen(str[i]); j++) {
			if (i == 0 && str[i][j] == ' ') {
				count++;
			}
			if (i == 1 && str[i][j] == ' ') {
				count1++;
			}
			if (i == 2 && str[i][j] == ' ') {
				count2++;
			}
		}
	}

	polindrom(str2, str, k, p, q, count, count1, count2);

	cout << endl;

	if (k) {
		cout << ("String №1 is polindrom.") << endl;
	}
	else {
		cout << ("String №1 is not polindrom.") << endl;
	}
	if (p) {
		cout << ("String №2 is polindrom.") << endl;
	}
	else {
		cout << ("String №2 is not polindrom.") << endl;
	}
	if (q) {
		cout << ("String №3 is polindrom.") << endl;
	}
	else {
		cout << ("String №3 is not polindrom.") << endl;
	}

	return 0;
}
//XOR в строках повторить, узнать
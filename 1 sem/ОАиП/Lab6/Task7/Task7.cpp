#include <iostream>
#include <windows.h> 
#include <cmath>


using namespace std;
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
		cout << ("Максимального непалиндрома не существует: ") << -1 << endl;
		return -1;
	}
	else {
		if (m == 1) {
			count = strlen(str) - 1;
			cout << ("Длина максимального непалиндрома равна: ") << count << endl;
			return count;
		}
		else if (m == 0) {
			cout << ("Длина максимального непалиндрома равна: ") << strlen(str) << endl;
			return strlen(str);
		}
	}
}
int main() {

	setlocale(LC_CTYPE, "rus");

	SetConsoleCP(1251);

	char str[106];


	cout << ("Введите строку: ");
	
	cin.getline(str, 106);


	int chetchik = 0;
	int count = -1;
	bool m = 1;
	
	DlinaPolindroma(str, chetchik, count, m);

	return 0;
}
#include <iostream>
#include <windows.h> 
#include <cmath>
#include <string>

using namespace std;

unsigned long long fact(int n) {
	if (n == 0) {
		return 1;
	}
	return n * fact(n - 1);
}
int main() {

	setlocale(LC_CTYPE, "rus");

	SetConsoleCP(1251);

	string str;

	cout << ("Введите слово: ");

	cin >> str;

	int count = str.length();

	long long F =  fact(count);

	int kol = 1;
	for (int i = 0; i < count; i++) {
		for (int j = i + 1; j < count; j++) {		
			if (str[i] == str[j]) {
				kol++;
				i++;
			
			}
		}
		F = F / fact(kol);
		kol = 1;
	}
		
	cout << ("Кол-во анаграмм равно: ") << F << endl;

	return 0;
}
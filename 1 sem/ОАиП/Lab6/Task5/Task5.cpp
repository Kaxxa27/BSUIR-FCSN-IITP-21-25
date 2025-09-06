#include <iostream>
#include <windows.h> 
#include <cmath>
#include <string>

using namespace std;

int check() {
	int n;
	while (true) {
		cin >> n;
		if (cin.fail() || cin.peek() != '\n') {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}
	return n;
}
int main() {

	setlocale(LC_CTYPE, "rus");

	SetConsoleCP(1251);

	int t, L = 0, m = 0;

	cout << ("Введите кол-во наборов входных данных: ");
	t = check();

	int count = 0;

	while (t > 0) {

		cout << ("Введите длину исходной строки: ");
		L = check();

		cout << ("Введите m (деление с остатком): ");
		m = check();

		string S1;
		string S2;

		cout << ("Введите первую строку (S1):");
		cin >> S1;

		cout << ("Введите вторую строку (S2):");
		cin >> S2;
	
		int i = 0;
		
		if (S1[i] == '\0' || S2[i] == '\0') {
			break;
		}
		else {

			int dl = S1.length();

			for (int k = S1.length()-1; k >=0; k--){
				dl--;
				if (S1[k] == S2[0]) {
						break;
				}
			}
			dl = S1.length() - dl;

			for (int k = 0,j = dl-1; j < S1.length(); j++,k++) {
				if (S1[j] != S2[k]) {
					break;
				}
				count++;
			}
		}
			
		L = L - S1.length() - S2.length() + count;

		int KolPer = 0;
		if (L < 0) {
			cout << ("Кол-во строк удовл. усл. равно: ") << 0 << endl;
		}
		else if (L == 0) {
			cout << ("Кол-во строк удовл. усл. равно: ") << 1 << endl;
		}
		else {

			KolPer = (pow(26, L) * 2);
			KolPer %= m;
			cout << ("Кол-во строк удовл. усл. равно: ") << KolPer << endl;
		}
		t--;
	}
	return 0;
}
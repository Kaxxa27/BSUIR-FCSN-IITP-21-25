#include<iostream>
#include <string>

using namespace std;

string prov(int num) {
	string ans;
	while (num != 0) {
		ans = char(48 + num % 3) + ans;
		num /= 3;
	}
	while (ans.length() < 5) {
		ans = '0' + ans;
	}
	return ans;
}

int prov1(string num) {
	int ans = 0;
	for (int i = 0; i < 5; i++)
		ans += (num[i] - 48) * pow(3, 4 - i);

	return ans;
}

int main() {

	setlocale(LC_CTYPE, "rus");

	srand(time(0));
	//int p = rand() % 240;
	cout << ("Введитие отравлениую бочку: ");
	int p = 0;
	cin >> p;
	cout << "Бочка отравлена под номером : " << p << "\n";
	string poison = prov(p);

	string* bar = new string[240];
	for (int i = 0; i < 240; i++)
		bar[i] = prov(i);

	char slaves[5];
	for (int i = 0; i < 5; i++)
		slaves[i] = '0';

	for (int i = 0; i < 240; i++) {//1
		for (int j = 0; j < 5; j++) {
			if (bar[i][j] == '0' || bar[i][j] == '2')
				continue;
			if (bar[i] == poison)
				slaves[j] = '1';
		}
	}

	for (int i = 0; i < 240; i++) {//2
		for (int j = 0; j < 5; j++) {
			if (bar[i][j] == '0' || bar[i][j] == '1')
				continue;
			if (bar[i] == poison)
				slaves[j] = '2';
		}
	}


	string mbpois;
	for (int i = 0; i < 5; i++)
		mbpois += slaves[i];

	int ot = prov1(mbpois);

	cout << "Отравленная бочка: " << ot << endl;
}
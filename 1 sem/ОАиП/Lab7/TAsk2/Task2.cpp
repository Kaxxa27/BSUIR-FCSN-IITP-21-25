#include <iostream>
#include <cmath>
#include <string> 

using namespace std;

void revers(char str2[], char str[]) {
	int i = 0;
	for (; i < strlen(str2); i++) {
		str[i] = str2[strlen(str2) - 1 - i];
	}
	str[i] = '\0';
}
int main() {

	setlocale(LC_CTYPE, "rus");

	char str[9];
	char str2[9]{};
	int num;

	cout << ("¬ведите ваше число: ");

	cin >> num;
	
	if (num<0) {
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

	return 0; 
}




#include <iostream>
#include <windows.h> 
#include <cmath>

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

	char str[100];
	char str2[199];


	int q;
	cout << ("Введите кол-во входных данных: ");
	q = check();
	
	while (q > 0) {

		cin.ignore(10000, '\n');

		cout << ("Введите строку: ");
		cin.getline(str, 100);

		cout << ("Длина вашей строки равна: ") << strlen(str) << endl;

		cout << ("Укажите мество, куда вы хотите поставить фишку от 1 до ") << strlen(str) << (" : ");

		int place;
		while (true) {
			place = check();
			if (place > strlen(str) || place < 1) {
				cout << ("Ваше значение больше чем ") << strlen(str) << ('.') << endl;
				cout << ("Повторите ввод.") << endl;
			}
			else {
				break;
			}

		}

		str2[0] = str[place - 1];
		int right;
		int R = 0;
		int k = 1;
		int i = 0;
		int proverka = place - 1;

		while (true) {//right
			if (proverka != strlen(str)) {
				cout << ("Вы можете пододвинуть фишку вправо, будете? (1/0): ");
				while (true) {
					right = check();
					if (right == 1 || right == 0) {
						break;
					}
					else {
						cout << ("Повторите ввод.(1/0)") << endl;
					}
				}
				if (right == 1) {
					R++;
					for (; i < R;) {
						str2[k] = str[place + i];
						proverka++;
						i++;
					}
					k++;
				}
				else {
					break;
				}
			}
			else {
				cout << ("Упс, вы не можете двигаться вправо.\n");
				break;
			}
		}

		int left;

		place = place + i - 1;
		i = 0;
		R = 0;
		k--;
		proverka = place;

		while (true) {//left
			if (proverka != 0) {
				cout << ("Вы можете пододвинуть фишку влево, будете? (1/0): ");
				while (true) {
					left = check();
					if (left == 1 || left == 0) {
						break;
					}
					else {
						cout << ("Повторите ввод.(1/0)") << endl;
					}
				}
				if (left == 1) {
					R++;
					for (; i < R;) {
						str2[k] = str[place - 2 - i];
						proverka--;
						i++;
					}
					k++;
				}
				else {
					break;
				}
			}
			else {
				cout << ("Упс, вы не можете двигаться влево.\n");
				break;
			}
		}
		if (i > 1) {
			str2[k - 1] = '\0';
		}
		else {
			str2[k + 1] = '\0';
		}

		cout << ("Новая строка равна: ") << str2 << endl;

		q--;
	}

	return 0;
}
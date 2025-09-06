#include <iostream>
#include <windows.h> 

using namespace std;
string text(char* str, char* str2) {

	bool pro = true;
	while (pro) {
		for (int i = 0, k = 0; i < 101; i++, k++) {

			if (str[i] == 'c') {
				if (str[i + 1] == 'e') {
					str2[k] = 's';
				}
				else if (str[i + 1] == 'i') {
					str2[k] = 's';
				}
				else if (str[i + 1] == 'y') {
					str2[k] = 's';
				}
				else {
					str2[k] = 'k';
				}
				if (str2[k] == str[i + 1]) {
					str2[k] = str[i + 1];
					i++;
				}
			}
			else if (str[i] == 'q' && str[i + 1] == 'u') {
				str2[k] = 'k';
				str2[k + 1] = 'v';
				k++;
				i++;
			}
			else if (str[i] == 'q') {
				str2[k] = 'k';
			}
			else if (str[i] == 'x') {
				str2[k] = 'k';
				str2[k + 1] = 's';
				k++;
			}
			else if (str[i] == 'w') {
				str2[k] = 'v';
			}
			else if (str[i] == 'p' && str[i + 1] == 'h') {
				str2[k] = 'f';
				i++;
			}
			else if (str[i] == 'y' && str[i + 1] == 'o' && str[i + 2] == 'u') {
				str2[k] = 'u';
				i += 2;
			}
			else if (str[i] == 'o' && str[i + 1] == 'o') {
				str2[k] = 'u';
				i++;
			}
			else if (str[i] == 'e' && str[i + 1] == 'e') {
				str2[k] = 'i';
				i++;
			}
			else if ((str[i] == 't' || str[i] == 'T') && str[i + 1] == 'h') {
				if (str[i] == 't') {
					str2[k] = 'z';
					i++;
				}
				else if (str[i] == 'T') {
					str2[k] = 'Z';
					i++;
				}

			}
			else if (str[i] == 'b' && str[i + 1] == 'b') {
				str2[k] = 'b';
				i++;
			}
			else if (str[i] == 'c' && str[i + 1] == 'c') {
				str2[k] = 'c';
				i++;
			}
			else if (str[i] == 'd' && str[i + 1] == 'd') {
				str2[k] = 'd';
				i++;
			}
			else if (str[i] == 'f' && str[i + 1] == 'f') {
				str2[k] = 'f';
				i++;
			}
			else if (str[i] == 'g' && str[i + 1] == 'g') {
				str2[k] = 'g';
				i++;
			}
			else if (str[i] == 'h' && str[i + 1] == 'h') {
				str2[k] = 'h';
				i++;
			}
			else if (str[i] == 'j' && str[i + 1] == 'j') {
				str2[k] = 'j';
				i++;
			}
			else if (str[i] == 'k' && str[i + 1] == 'k') {
				str2[k] = 'k';
				i++;
			}
			else if (str[i] == 'l' && str[i + 1] == 'l') {
				str2[k] = 'l';
				i++;
			}
			else if (str[i] == 'm' && str[i + 1] == 'm') {
				str2[k] = 'm';
				i++;
			}
			else if (str[i] == 'n' && str[i + 1] == 'n') {
				str2[k] = 'n';
				i++;
			}
			else if (str[i] == 'p' && str[i + 1] == 'p') {
				str2[k] = 'p';
				i++;
			}
			else if (str[i] == 'q' && str[i + 1] == 'q') {
				str2[k] = 'q';
				i++;
			}
			else if (str[i] == 'r' && str[i + 1] == 'r') {
				str2[k] = 'r';
				i++;
			}
			else if (str[i] == 's' && str[i + 1] == 's') {
				str2[k] = 't';
				i++;
			}
			else if (str[i] == 'v' && str[i + 1] == 'v') {
				str2[k] = 'v';
				i++;
			}
			else if (str[i] == 'w' && str[i + 1] == 'w') {
				str2[k] = 'w';
				i++;
			}
			else if (str[i] == 'x' && str[i + 1] == 'x') {
				str2[k] = 'x';
				i++;
			}
			else if (str[i] == 'z' && str[i + 1] == 'z') {
				str2[k] = 'z';
				i++;
			}
			else {
				str2[k] = str[i];
			}
		}
		return str2;
	}
}
int main() {

	setlocale(LC_CTYPE, "rus");

	SetConsoleCP(1251);


	char* str = new char[101];
	char* str2 = new char[101];

	str[101] = {};
	str2[101] = {};

	cin.getline(str, 101);

	cout << str << endl;

	text(str, str2);

	cout << str2 << endl;

	delete[] str;
	delete[] str2;
	return 0 ;
}
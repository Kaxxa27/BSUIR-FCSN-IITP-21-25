#include <iostream>

using namespace std;


int main() {

	setlocale(LC_CTYPE, "rus");

	char str[81];

	char strChet[81];


	cin.getline(str, 81);

	cout << str << endl;

	int j = 0;
	for (int i = 0; i < 81; i++){
		if (str[i] == '\0') {
			break;
		}
		else {
			if (isdigit(str[i])) {
				if (str[i] % 2 == 0) {
					strChet[j] = str[i];
					strChet[j + 1] = ' ';
					j += 2;
				}
			}
		}
	}
	strChet[j] = '\0';

	cout << strChet << endl;

	return 0;
}

//int n = 0;
//int N = 1;
//
//char* arrstr = (char*)malloc(sizeof(char));
//
//char str = getchar();
//
//while (str != '\n') {
//	arrstr[n++] = str;
//
//	if (n >= N) {
//		N *= 2;
//		arrstr = (char*)realloc(arrstr, N * sizeof(char));
//	}
//
//	str = getchar();
//}
//
//arrstr[n] = '\0';
//
//for (int i = 0; i < n; i++) {
//
//	cout << arrstr[i];
//
//}

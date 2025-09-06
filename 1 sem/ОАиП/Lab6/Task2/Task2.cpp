#include <iostream>

using namespace std;


int main() {

	setlocale(LC_CTYPE, "rus");

	char* str = new char[100];

	cin.getline(str, 100);
	
	//max
	int k = 0;
	int max = 0;
	int count = 0;
	int  i = 0;

	for (; i < 100; i++) {

		if (*(str + i) != ' ') {
			count++;
		}
		else {
			if (max < count) {
				max = count;
				k = i - max;
			}
			count = 0;
		}
		if (*(str + i) == '\0') {
			if (max < count) {
				max = count;
				count = 0;
				k = i - max;

			}
			break;
		}
	}

	cout << ("Max word: ");
	for (int j = k; j < (k + max); j++) {
		cout << *(str + j);
	}


	//min
	int k2 = 0;
	int max2 = 100;
	int count2 = 0;
	int  i2 = 0;

	for (; i2 < 100; i2++) {

		if (*(str + i2) != ' ') {
			count2++;
		}
		else {
			if (max2 > count2) {
				max2 = count2;
				k2 = i2 - max2;
			}
			count2 = 0;
		}
		if (*(str + i2) == '\0') {
			if (max2 > count2) {
				max2 = count2;
				count2 = 0;
				k2 = i2 - max2;

			}
			break;
		}
	}

	cout << endl << ("Min word: ");
	for (int j = k2; j < (k2 + max2); j++) {
		cout << *(str + j);
	}
	

	return 0;
}

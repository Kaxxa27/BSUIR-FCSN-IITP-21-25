#include<iostream>

using namespace std;

void delnull(char arr[], char arrnew[],int& count, int& chet) {

	for (int i = 0; i < count; i++) {
		if (arr[i] == '0') {
			chet++;
		}
		else {
			break;
		}
	}
	int j = 0;
	for (; j < count; j++) {
		arrnew[j] = arr[chet];
		chet++;
	}
	arrnew[j] = '\0';

}
void revers(char str[], char strnew[]) {
	int i = 0;
	for (; i < strlen(str); i++) {
		strnew[i] = str[strlen(str) - 1 - i];
	}
	strnew[i] = '\0';
}
void i3(int n, int& count, char s[]) {
	int i = 0;
	while (n > 0) {
		s[i] = char(n % 3 + 48);
		count++;
		n /= 3;
		i++;
	}
	s[i] = '\0';
}
void zamena(char arr[], int& count, bool& fall) {
	bool pr = true;
	while (pr) {
		pr = false;
		for (int i = 0; i < count; ++i) {//10 => 03
			if (arr[i] == '1' && arr[i + 1] == '0') {
				arr[i] = '0';
				arr[i + 1] = '3';
				i++;
				pr = true;
				fall = true;
			}
			else if (arr[i] == '2' && arr[i + 1] == '0') {//20 => 13
				arr[i] = '1';
				arr[i + 1] = '3';
				i++;
				pr = true;
				fall = true;
			}
			else if (arr[i] == '3' && arr[i + 1] == '0') {//30 => 23
				arr[i] = '2';
				arr[i + 1] = '3';
				i++;
				pr = true;
				fall = true;
			}
			else {
				arr[i] = arr[i];
			}
		}
	}
}
int main() {

	setlocale(LC_CTYPE, "rus");

	int n;
	int count = 0;
	int  chet = 0;
	bool fall = false;
	char s[100];
	char arrnew[100];


	cout << "¬ведите ваше число:";
	cin >> n;
	n = abs(n);

	i3(n, count, s);
	zamena(s, count, fall);
	delnull(s, arrnew, count, chet);

	if(!fall){
		revers(arrnew, s);
		cout << s << endl;
	}
	else {
		cout << arrnew << endl;
	}
	return 0;
}
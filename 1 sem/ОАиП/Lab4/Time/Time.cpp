#include <iostream>

using namespace std;

int main() {

	int n = 1;
	unsigned int start_time = clock();
	for (int i = 0; i < 10; i++) {

		cout << n;

	}
	unsigned int end_time = clock();
	unsigned int search_time = end_time - start_time;

	cout << endl << search_time << endl;
}
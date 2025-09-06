
#include "Func.h"
#include <cmath>

int Sumchet(int n, int& sum) {

	if (n == 0) {
		return 0;
	}
	else {
		sum += pow((n + 1) / 2, 2);
	}

	return  Sumchet(n / 2, sum);
}

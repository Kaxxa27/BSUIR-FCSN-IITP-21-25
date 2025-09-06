//
// pch.cpp
//

#include "pch.h"


	int odna_tret(int* arr, int n, int& count, int i) {
		if (i >= n / 3) {
			return 0;
		}
		if (cos(pow(arr[i], 2)) > 0 && arr[i] < 0) {
			(count)++;
		}
		(i)++;

		return odna_tret(arr, n, count, i);
	}
	int dwe_treti(int* arr, int n, int& count, int i) {
		if (i >= n) {
			return 0;
		}
		if (cos(pow(arr[i], 2)) > 0 && arr[i] < 0) {
			(count)++;
		}
		(i)++;

		return dwe_treti(arr, n, count, i);
	}
	int Sumchet(int n, int& sum) {

		if (n == 0) {
			return 0;
		}
		else {

			sum += pow((n + 1) / 2, 2);

		}

		return  Sumchet(n / 2, sum);
	}
	int S(int p, int q, int& sum) {

		if (p > q) {
			return 0;
		}
		else {
			if (p % 10 > 0 || p % 10 == 0) {
				if (p % 10 == 0) {
					int m = 0;
					m = p / 10;
					sum += m;
				}
				else {
					sum += p % 10;
				}
			}
			else if (p == 0) {
			}
			else {
				p = p / 10;
				return S(p, q, sum);
			}
			p++;
		}

		return S(p, q, sum);
	}



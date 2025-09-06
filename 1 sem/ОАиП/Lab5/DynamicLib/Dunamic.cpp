#include <cmath> 
extern "C" __declspec(dllexport) int odna_tret(int* arr, int* n, int* count, int* i) {
	if (*i >= *n / 3) {
		return 0;
	}
	if (cos(pow(arr[*i], 2)) > 0 && arr[*i] < 0) {
		(*count)++;
	}
	(*i)++;

	return odna_tret(arr, n, count, i);
}
extern "C" __declspec(dllexport) int dwe_treti(int* arr, int* n, int* count, int* i) {
	if (*i >= *n) {
		return 0;
	}
	if (cos(pow(arr[*i], 2)) > 0 && arr[*i] < 0) {
		(*count)++;
	}
	(*i)++;

	return dwe_treti(arr, n, count, i);
}
#include "headers.h"

void print_roots(const vector<double>& vec, string text, int w, int precision) {

	if (text == "\n")
		cout << "\n\n";
	else
		cout << "\n" << text << "\n";

    for (const auto& i : vec)
        cout << fixed << setw(w) << setprecision(precision) << i << "\n";
}
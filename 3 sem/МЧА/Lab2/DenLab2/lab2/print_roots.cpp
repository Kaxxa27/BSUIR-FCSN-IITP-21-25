#include "headers.h"

void print_roots(const vector<double>& roots, string text, int w, int precision) {

	if (text == "\n")
		cout << "\n\n";
	else
		cout << "\n" << text << "\n";

    for (const auto& i : roots)
        cout << fixed << setw(w) << setprecision(precision) << i << "\n";
}
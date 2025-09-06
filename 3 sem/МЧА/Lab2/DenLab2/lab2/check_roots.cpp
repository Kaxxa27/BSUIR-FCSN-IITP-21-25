#include "headers.h"

void check_roots(Matrix<double> A, const vector<double>& roots, string text, int w, int precision) {

    if (text == "\n")
        cout << "\n\n";
    else
        cout << "\n" << text << "\n";

    for (int i = 0; i < roots.size(); i++) {
        double b = 0;
        for (int j = 0; j < roots.size(); j++)
            b += roots[j] * A.at(i, j);

        cout << fixed << setw(w) << setprecision(precision) << b << "\n";
    }
}
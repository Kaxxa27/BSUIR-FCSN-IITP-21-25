#include "headers.h"

void type2(Matrix<double> A) {

    for (int k = 0; k < A.getRows() - 1; k++) {

        A.swapRows(A.rowOfMaxItemInColumnFromPoint(k, k), k);

        for (int i = 1 + k; i < A.getRows(); i++) {
            double q = A.at(i, k) / A.at(k, k);

            for (int j = k; j < A.getColumns(); j++)
                A.at(i, j) -= A.at(k, j) * q;
        }
    }

    A.print("Выбор главного элемента по столбцу");

    vector<double> roots(A.getRows());

    for (int k = A.getRows() - 1; k >= 0; k--) {

        double difference = A.at(k, A.getColumns() - 1);

        for (int i = A.getRows() - 1; i > k; i--)
            difference -= A.at(k, i) * roots[i];

        roots[k] = difference / A.at(k, k);
    }

    print_roots(roots, "Корни");
}
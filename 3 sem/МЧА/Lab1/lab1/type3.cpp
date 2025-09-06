#include "headers.h"

void type3(Matrix<double> A) {

    map<int, int> rootsOrder;
    for (int i = 0; i < A.getRows(); i++)
        rootsOrder[i] = i;


    for (int k = 0; k < A.getRows() - 1; k++) {

        Point maxItem = A.posOfMaxItemInMatrixFromPoint(k, k);

        A.swapRows(maxItem.row, k);
        A.swapColumns(maxItem.column, k);

        rootsOrder[maxItem.column] = k;
        rootsOrder[k] = maxItem.column;

        for (int i = 1 + k; i < A.getRows(); i++) {
            double q = A.at(i, k) / A.at(k, k);

            for (int j = k; j < A.getColumns(); j++)
                A.at(i, j) -= A.at(k, j) * q;
        }
    }

    A.print("Выбор главного эелмента по всей матрице");

    vector<double> roots(A.getRows());

    for (int k = A.getRows() - 1; k >= 0; k--) {

        double difference = A.at(k, A.getColumns() - 1);

        for (int i = A.getRows() - 1; i > k; i--)
            difference -= A.at(k, i) * roots[i];

        roots[k] = difference / A.at(k, k);
    }

    vector<double> rootsInOrder(A.getRows());

    for (int i = 0; i < roots.size(); i++)
        rootsInOrder[i] = roots[rootsOrder[i]];

    print_roots(rootsInOrder, "Корни");
}
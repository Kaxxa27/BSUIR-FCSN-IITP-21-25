#include "headers.h"


double** create_C() {
    double** C = new double* [5];
    C[0] = new double[5]{ 0.0100, 0, -0.0200, 0, 0 };
    C[1] = new double[5]{ 0.0100, 0.0100, -0.0200, 0, 0 };
    C[2] = new double[5]{ 0, 0.0100, 0.0100, 0, -0.0200 };
    C[3] = new double[5]{ 0, 0, 0.0100, 0.0100, 0 };
    C[4] = new double[5]{ 0, 0, 0, 0.0100, 0.0100 };

    return C;
}

double** create_D() {
    double** D = new double* [5];
    D[0] = new double[5]{ 1.3300, 0.2100, 0.1700, 0.1200, -0.1300 };
    D[1] = new double[5]{ -0.1300, -1.3300, 0.1100, 0.1700, 0.1200 };
    D[2] = new double[5]{ 0.1200, -0.1300, -1.3300, 0.1100, 0.1700 };
    D[3] = new double[5]{ 0.1700, 0.1200, -0.1300, -1.3300, 0.1100 };
    D[4] = new double[5]{ 0.1100, 0.6700, 0.1200, -0.1300, -1.3300 };

    return D;
}


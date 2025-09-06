#include "headers.h"


double** create_C() {
    double** C = new double* [5];
    C[0] = new double[5]{ 0.2, 0, 0.2, 0, 0 };
    C[1] = new double[5]{ 0, 0.2, 0, 0.2, 0 };
    C[2] = new double[5]{ 0.2, 0, 0.2, 0, 0.2 };
    C[3] = new double[5]{ 0, 0.2, 0, 0.2, 0 };
    C[4] = new double[5]{ 0, 0, 0.2, 0, 0.2 };

    return C;
}

double** create_D() {
    double** D = new double* [5];
    D[0] = new double[5]{ 2.33, 0.81, 0.67, 0.92, -0.53 };
    D[1] = new double[5]{ -0.53, 2.33, 0.81, 0.67, 0.92 };
    D[2] = new double[5]{ 0.92, -0.53, 2.33, 0.81, 0.67 };
    D[3] = new double[5]{ 0.67, 0.92, -0.53, 2.33, 0.81 };
    D[4] = new double[5]{ 0.81, 0.67, 0.92, -0.53, 2.33 };

    return D;
}


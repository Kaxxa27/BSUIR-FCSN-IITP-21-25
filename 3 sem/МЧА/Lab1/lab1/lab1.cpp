#include <iostream>
#include <iomanip>
#include "Matrix.h"
#include "headers.h"
using namespace std;

int k = 11;

int main()
{
    setlocale(LC_ALL, "Ru");

    Matrix<double> C(create_C(), 5, 5);
    Matrix<double> D(create_D(), 5, 5);
    
    Matrix<double> A = k * C + D;
    vector<double> d = { 4.2, 4.2, 4.2, 4.2, 4.2 };

    A = A + d;


    A.print("Исходная матрица"); cout << "\n\n";

    type1(A); cout << "\n\n";
    type2(A); cout << "\n\n";
    type3(A); cout << "\n\n";
}





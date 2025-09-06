#pragma once

#include <vector>
#include <map>
#include "Matrix.h"

// 5 было
void print_roots(const vector<double>& vec, string text = "\n", int w = 10, int precision = 5);

void type1(Matrix<double> A);
void type2(Matrix<double> A);
void type3(Matrix<double> A);

double** create_C();
double** create_D();


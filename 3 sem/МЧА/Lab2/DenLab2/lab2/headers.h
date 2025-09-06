#pragma once

#include <vector>
#include <map>
#include "Matrix.h"


void print_roots(const vector<double>& roots, string text = "\n", int w = 10, int precision = 4);
void check_roots(Matrix<double> A, const vector<double>& roots, string text = "\n", int w = 10, int precision = 4);

vector<double> seidelMethod(Matrix<double> A, int& iterations);
vector<double> simpleIterations(Matrix<double> A, int& iterations);

double** create_C();
double** create_D();


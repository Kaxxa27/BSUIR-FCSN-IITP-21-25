#pragma once

#include "Segment.h"
#include "Result.h"
#include <cmath>
#include <vector>
using namespace std;

#define MAX_ITERATIONS 1000

class Polynomial {
private:


	double a;
	double b;
	double c;

	double f0(double x) {
		return pow(x, 3) + a * pow(x, 2) + b * x + c;
	}

	double f1(double x) {
		return 3 * pow(x, 2) + 2 * a * x + b;
	}

	double f2(double x) {
		return ((2.0 / 9.0) * pow(a, 2) * x - (2.0 / 3.0) * b * x + (1.0 / 9.0) * a * b - c);
	}

	double f3(double x) {
		double numerator = 4 * pow(a, 3) * c - pow(a * b, 2) - 18 * a * b * c + 4 * pow(b, 3) + 27 * pow(c, 2);
		double denominator = pow(a * a - 3 * b, 2);

		return -(9.0 / 4.0) * (numerator / denominator);
	}

	int N(double x) {
		vector<double> values(4);
		values[0] = f0(x);
		values[1] = f1(x);
		values[2] = f2(x);
		values[3] = f3(x);

		int counter = 0;

		for (int i = 0; i < 3; i++) {
			if (values[i] * values[i + 1] < 0)
				++counter;
		}

		return counter;
	}


public:
	Polynomial(double a, double b, double c) {
		this->a = a;
		this->b = b;
		this->c = c;
	}

	int RootsNumber(Segment segment) {
		return N(segment.a) - N(segment.b);
	}

	vector<Segment> SegmentsWithRoots(Segment segment, double step) {
		vector<Segment> segments;

		for (double x = segment.a; x < segment.b; x += step) {
			if (f0(x) * f0(x + step) < 0)
				segments.emplace_back(x, x + step);
		}

		vector<Segment> upd_segments;
		for (int i = 0; i < segments.size(); i++) {
			
			if (RootsNumber(segments[i]) != 1) {
				for (const auto& item : SegmentsWithRoots(segments[i], step / 2))
					upd_segments.push_back(item);
			}	
			else {
				upd_segments.push_back(segments[i]);
			}
		}
		return upd_segments;
	}


	Result HalfDivision(Segment segment, double eps) {

		if (!(f0(segment.a) * f0(segment.b) < 0) ||		// f(a) * f(b) должно быть меньше 0
			RootsNumber(segment) != 1)					// На промежутке должнен быть только 1 корень
			return Result(-1, -1);

		int iterations = 1;

		double left = segment.a;
		double right = segment.b;

		double mid = (left + right) / 2;

		while (abs(f0(mid)) > eps && iterations < MAX_ITERATIONS) {

			if (f0(left) * f0(mid) < 0)
				right = mid;
			else
				left = mid;

			mid = (left + right) / 2;

			++iterations;
		}

		return Result(mid, iterations);
	}

	Result ChordMethod(Segment segment, double eps) {

		
		if (!(f0(segment.a) * f0(segment.b) < 0) ||		// f(a) * f(b) должно быть меньше 0
			RootsNumber(segment) != 1)					// На промежутке должнен быть только 1 корень
			return Result(-1, -1);

		int iterations = 1;

		double Xn_prev = 0;
		double Xn_curr = 0;

		if (f0(segment.b) * (2 * a + 6 * segment.b) > 0) {

			Xn_prev = segment.a;
			Xn_curr = Xn_prev - (f0(Xn_prev) / (f0(segment.b) - f0(Xn_prev))) * (segment.b - Xn_prev);

			while (fabs(Xn_curr - Xn_prev) > eps && iterations < MAX_ITERATIONS) {
				Xn_prev = Xn_curr;
				Xn_curr = Xn_prev - (f0(Xn_prev) / (f0(segment.b) - f0(Xn_prev))) * (segment.b - Xn_prev);

				++iterations;
			}
		}
		else {
			Xn_prev = segment.b;
			Xn_curr = Xn_prev - (f0(Xn_prev) / (f0(segment.a) - f0(Xn_prev))) * (segment.a - Xn_prev);

			while (fabs(Xn_curr - Xn_prev) > eps && iterations < MAX_ITERATIONS) {
				Xn_prev = Xn_curr;
				Xn_curr = Xn_prev - (f0(Xn_prev) / (f0(segment.a) - f0(Xn_prev))) * (segment.a - Xn_prev);

				++iterations;
			}
		}

		return Result(Xn_curr, iterations);
	}

	Result NewtonMethod(Segment segment, double eps) {

		if (RootsNumber(segment) != 1)	// На промежутке должнен быть только 1 корень
			return Result(-1, -1);

		int iterations = 1;

		double Xn_prev = 0;
		if (f0(segment.b) >= 0)
			Xn_prev = segment.b;
		else
			Xn_prev = segment.a;

		double Xn_curr = Xn_prev - f0(Xn_prev) / f1(Xn_prev);

		while (fabs(Xn_curr - Xn_prev) > eps && iterations < MAX_ITERATIONS) {
			Xn_prev = Xn_curr;
			Xn_curr = Xn_prev - f0(Xn_prev) / f1(Xn_prev);

			++iterations;
		}

		return Result(Xn_curr, iterations);
	}
};
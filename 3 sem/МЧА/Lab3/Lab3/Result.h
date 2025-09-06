#pragma once

struct Result {
	double root;
	int iterations;

	Result(double root = 0, int iterations = 0) {
		this->root = root;
		this->iterations = iterations;
	}
};
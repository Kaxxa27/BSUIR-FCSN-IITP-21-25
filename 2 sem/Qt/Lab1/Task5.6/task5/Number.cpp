#include "Number.h"

Number::Number(double value) {
	Number::value = value;
};

double Number::evaluate() const {
	return value;
}
#include <iostream>
#include "Expression.h"
#include "BinaryOperation.h"
#include "Number.h"

//TASK_6

bool check_eq(Expression const* left, Expression const* right) {
	return (*((int*)left) == *((int*)right));
}


int main() {
	
	Expression* expr = new BinaryOperation(new BinaryOperation(new Number(3), '+', new Number(6)), '*', new BinaryOperation(new Number(3), '+', new Number(6)));

	std::cout << check_eq(expr, expr);
}
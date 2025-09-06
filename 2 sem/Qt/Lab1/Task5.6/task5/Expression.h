#pragma once

class Expression {
public:
	virtual double evaluate() const = 0;
	virtual ~Expression() {};



};

#pragma once
#include "Expression.h"

class Number : public Expression {
private:
    double value;
public:
    Number(double value);
    double evaluate() const override;
};


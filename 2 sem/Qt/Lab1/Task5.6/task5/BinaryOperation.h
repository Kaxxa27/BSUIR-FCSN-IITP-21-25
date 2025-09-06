#pragma once
#include "Expression.h"

class BinaryOperation : public Expression {
private:
    char op;
    Expression const* left;
    Expression const* rigth;
    double makeOperation() const;
    double sum() const;
    double sub() const;
    double mul() const;
    double div() const;
public:
    BinaryOperation(Expression const* left, const char& op, Expression const* right);
    ~BinaryOperation();
    double evaluate() const override;
};


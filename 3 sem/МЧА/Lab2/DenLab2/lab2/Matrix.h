#pragma once

#include <iostream>
#include <vector>
#include <iomanip>
#include <string>
using namespace std;


struct Point {
	int row;
	int column;

	Point(int row, int column) {
		this->row = row;
		this->column = column;
	}
};

template<class T>
class Matrix {
private:
	int rows;
	int columns;
	T** array;

	Matrix() : rows(0), columns(0), array(nullptr) { }

public:
	Matrix(T** array, int rows, int columns) : 
		rows(rows), columns(columns) {

		this->array = new T * [rows];
		for (int i = 0; i < rows; i++)
			this->array[i] = new T[columns];

		for (int i = 0; i < rows; i++)
			for (int j = 0; j < columns; j++)
				this->array[i][j] = array[i][j];
	}

	Matrix(int rows, int columns) :
		rows(rows), columns(columns) {

		array = new T * [rows];
		for (int i = 0; i < rows; i++)
			array[i] = new T[columns];

		for (int i = 0; i < rows; i++)
			for (int j = 0; j < columns; j++)
				array[i][j] = 0;
	}

	Matrix(const Matrix& other) {

		rows = other.rows;
		columns = other.columns;

		array = new T * [rows];
		for (int i = 0; i < rows; i++)
			array[i] = new T[columns];

		for (int i = 0; i < rows; i++)
			for (int j = 0; j < columns; j++)
				this->array[i][j] = other.array[i][j];
	}

	~Matrix() {
		for (int i = 0; i < rows; i++)
			delete[] array[i];
		delete[] array;
	}

	Matrix operator=(const Matrix& other) {

		for (int i = 0; i < rows; i++)
			delete[] array[i];
		delete[] array;

		rows = other.rows;
		columns = other.columns;

		array = new T * [rows];
		for (int i = 0; i < rows; i++) {
			array[i] = new T[columns];
			for (int j = 0; j < columns; j++)
				array[i][j] = other.array[i][j];
		}

		return *this;
	}

	Matrix operator+(const Matrix& other) {

		Matrix ret = *this;

		for (int i = 0; i < rows; i++) 
			for (int j = 0; j < columns; j++) 
				ret.array[i][j] = array[i][j] + other.array[i][j];
			
		
		return ret;
	}

	Matrix operator+(const vector<T>& other) {

		if (rows != other.size())
			throw std::exception("invalid matrix+vector");

		Matrix ret;
		ret.rows = rows;
		ret.columns = columns + 1;

		ret.array = new T * [rows];
		for (int i = 0; i < rows; i++) {

			ret.array[i] = new T[columns + 1];

			for (int j = 0; j < columns; j++)
				ret.array[i][j] = array[i][j];

			ret.array[i][columns] = other[i];
		}

		return ret;
	}
	
	void print(string text = "\n", int w = 10, int precision = 4) {
		
		if (text == "\n")
			cout << "\n\n";
		else
			cout << "\n" << text << "\n";

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columns; j++)
				cout << fixed << setw(w) << setprecision(precision) << array[i][j] << " ";

			if (i != rows - 1)
				cout << "\n";
		}
		cout << "\n";
	}

	T& at(int row, int column) {
		if (row >= rows || row < 0 ||
			column >= columns || column < 0)
			throw std::exception("invalid index in matrix");

		return array[row][column];
	}

	template<class T>
	friend Matrix<T> operator*(int number, Matrix<T> matrix) {

		Matrix<T> ret = matrix;

		for (int i = 0; i < ret.rows; i++)
			for (int j = 0; j < ret.columns; j++)
				ret.array[i][j] *= number;

		return ret;
	}

	
	int getRows() {
		return rows;
	}

	int getColumns() {
		return columns;
	}

	void swapRows(int a, int b) {

		if (a < 0 || a >= rows)
			throw std::exception("invalid matrix rows indexes");

		for (int j = 0; j < columns; j++)
			std::swap(array[a][j], array[b][j]);
	}

	void swapColumns(int a, int b) {
		if (a < 0 || a >= columns)
			throw std::exception("invalid matrix columns indexes");

		for (int i = 0; i < rows; i++)
			std::swap(array[i][a], array[i][b]);
	}

	bool isDiagonalDominance() {

		for (int i = 0; i < rows; i++) {
			double sum = 0;

			for (int j = 0; j < columns; j++)
				if (i != j) sum += fabs(this->at(i, j));

			if (fabs(this->at(i, i)) < sum)
				return false;
		}

		return true;
	}

	int rank() {

		Matrix<double> matrix = *this;

		const double EPS = 1E-9;

		int n = rows;
		int m = columns;

		int rank = max(n, m);
		vector<char> line_used(n);
		for (int i = 0; i < m; ++i) {
			int j;
			for (j = 0; j < n; ++j) {
				if (!line_used[j] && abs(matrix.at(j, i)) > EPS) {
					break;
				}
			}

			if (j == n) {
				--rank;
			}
			else {
				line_used[j] = true;
				for (int p = i + 1; p < m; ++p)
					matrix.at(j,p) /= matrix.at(j,i);
				for (int k = 0; k < n; ++k)
					if (k != j && abs(matrix.at(k,i)) > EPS)
						for (int p = i + 1; p < m; ++p)
							matrix.at(k,p) -= matrix.at(j,p) * matrix.at(k,i);
			}
		}

		return rank;
	}
};


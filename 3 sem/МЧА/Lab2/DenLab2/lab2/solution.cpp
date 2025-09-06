#include "headers.h"

double eps = 0.0000000001;
extern double precision;

vector<double> seidelMethod(Matrix<double> A, int& iterations) {

    // Размер матрицы
    int size = A.getRows();
    

    // Неизвестные на предыдущей итерации
    vector <double> prev_X(size, 0.0);

    // Пока не будет достигнута точность
    bool stop = false;
    while (!stop)
    {
        ++iterations;
        // Неизвестные на текущей итерации   
        vector <double> current_X(size);

        for (int i = 0; i < size; i++)
        {
            // x_i = b_i
            current_X[i] = A.at(i,size);

            // Вычитаем сумму по всем отличным от i-ой неизвестным
            for (int j = 0; j < size; j++)
            {
                // С этой итерации
                if (j < i) 
                    current_X[i] -= A.at(i, j) * current_X[j];
                
                // С прошой итерации
                if (j > i) 
                    current_X[i] -= A.at(i, j) * prev_X[j];
                
            }

            // x_i /= b_i
            current_X[i] /= A.at(i,i);
        }

        // Максимальная погрешность
        long double max_error = 0.0;
        for (int i = 0; i < size; i++) {
            double new_max_error = abs(current_X[i] - prev_X[i]);
            max_error = new_max_error > max_error ? new_max_error : max_error;
        }

        // Дотигнута ли точность
        if (max_error < precision)
            stop = true;
        

        // Переход к следующей итерации
        prev_X = current_X;
    }


    return prev_X;


}

vector<double> simpleIterations(Matrix<double> A, int& iterations) {
    // Размер матрицы
    int size = A.getRows();


    // Неизвестные на предыдущей итерации
    vector <double> prev_X(size, 0.0);

    // Пока не будет достигнута точность
    bool stop = false;
    while (!stop)
    {
        ++iterations;
        // Неизвестные на текущей итерации   
        vector <double> current_X(size);

        for (int i = 0; i < size; i++)
        {
            // x_i = b_i
            current_X[i] = A.at(i, size);

            // Вычитаем сумму по всем отличным от i-ой неизвестным
            for (int j = 0; j < size; j++)
            {
                // С прошой итерации
                if (j != i)
                    current_X[i] -= A.at(i, j) * prev_X[j];
            }

            // x_i /= b_i
            current_X[i] /= A.at(i, i);
        }

        // Максимальная погрешность
        long double max_error = 0.0;
        for (int i = 0; i < size; i++) {
            double new_max_error = abs(current_X[i] - prev_X[i]);
            max_error = new_max_error > max_error ? new_max_error : max_error;
        }

        // Дотигнута ли точность
        if (max_error < precision)
            stop = true;


        // Переход к следующей итерации
        prev_X = current_X;
    }


    return prev_X;

}
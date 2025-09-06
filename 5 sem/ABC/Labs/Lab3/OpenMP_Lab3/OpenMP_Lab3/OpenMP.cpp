#include <iostream>
#include <omp.h>
#include <chrono>
#include <vector>

#define N 100000000

int main() {
    int i;
    std::vector<float> a(N), b(N);
    float result = 0.0;

    // Инициализация массивов
    for (i = 0; i < N; i++) {
        a[i] = 1;
        b[i] = 1;
    }
    
    // Измерение времени выполнения с использованием chrono
    auto start_time = std::chrono::high_resolution_clock::now();

    // Вычисление скалярного произведения без использования OpenMP
    for (i = 0; i < N; i++) {
        result += a[i] * b[i];
    }

    auto end_time = std::chrono::high_resolution_clock::now();
    auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end_time - start_time).count();

    // Вывод результата и времени выполнения без OpenMP
    std::cout << "[without OpenMP] Time: " << duration << " microseconds" << std::endl;


    // Измерение времени выполнения с использованием chrono
    auto start_time_omp = std::chrono::high_resolution_clock::now();

    result = 0;
    // Вычисление скалярного произведения с использованием OpenMP
#pragma omp parallel for reduction(+:result)
    for (i = 0; i < N; i++) {
        result += a[i] * b[i];
    }

    auto end_time_omp = std::chrono::high_resolution_clock::now();
    auto duration_omp = std::chrono::duration_cast<std::chrono::microseconds>(end_time_omp - start_time_omp).count();

    // Вывод результата и времени выполнения
    std::cout << "[with OpenMP] Time: " << duration_omp << " microseconds" << std::endl;
    std::cout << "Scalar Product: " << result << std::endl;
   

    return 0;
}

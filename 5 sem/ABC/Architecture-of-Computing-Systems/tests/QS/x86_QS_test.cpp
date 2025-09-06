// g++ -o x86-qs-test QS/x86_QS_test.cpp -march=native -lbenchmark -lpthread

#include <benchmark/benchmark.h>
#include <vector>
#include <algorithm>
#include <random>
#include <iostream>

// Встроенная ассемблерная инструкция обмена значениями
void swap_asm(int* a, int* b) {
    asm volatile(
        "movl (%0), %%eax;"
        "movl (%1), %%ebx;"
        "movl %%eax, (%1);"
        "movl %%ebx, (%0);"
        :
        : "r"(a), "r"(b)
        : "eax", "ebx"
    );
}

// Функция для разделения массива
int partition(std::vector<int>& arr, int low, int high) {
    int pivot = arr[high];  // Выбираем последний элемент в качестве опорного
    int i = low - 1;        // Индекс меньшего элемента

    for (int j = low; j <= high - 1; j++) {
        // Если текущий элемент меньше или равен опорному
        if (arr[j] <= pivot) {
            i++;
            swap_asm(&arr[i], &arr[j]);  // Используем ассемблерную инструкцию для обмена
        }
    }

    swap_asm(&arr[i + 1], &arr[high]);
    return i + 1;
}

// Основная функция быстрой сортировки
void quicksort(std::vector<int>& arr, int low, int high) {
    if (low < high) {
        // Получаем индекс разделения
        int pi = partition(arr, low, high);

        // Рекурсивно сортируем элементы до и после разделения
        quicksort(arr, low, pi - 1);
        quicksort(arr, pi + 1, high);
    }
}

// Google Benchmark тест
static void BM_QuickSort(benchmark::State& state) {
    std::vector<int> data(state.range(0));

    // Заполнение массива случайными числами
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<> dis(1, state.range(0) * 10);
    std::generate(data.begin(), data.end(), [&]() { return dis(gen); });

    std::vector<int> copy_data;
    for (auto _ : state) {
        // Копирование данных для избежания оптимизации компилятора
        copy_data = data;
        quicksort(copy_data, 0, copy_data.size() - 1);
    }

    //std::cout << "Results: [ ";
    //for (auto item : copy_data) {
    //    std::cout << item << ", ";
    //}
    //std::cout << " ]" << std::endl;
}
BENCHMARK(BM_QuickSort)
->Arg(50)
->Arg(100)
->Arg(200)
->Arg(500)
->Arg(1000)
->Arg(10000)
->Arg(100000)
->Unit(benchmark::kMillisecond)
->DisplayAggregatesOnly(true);  // Задайте нужные вам размеры массивов

BENCHMARK_MAIN();

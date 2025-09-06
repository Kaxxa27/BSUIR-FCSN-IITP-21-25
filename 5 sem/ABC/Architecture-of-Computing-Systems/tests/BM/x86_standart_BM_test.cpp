#include <benchmark/benchmark.h>
#include <vector>
#include <string>
#include <algorithm>
#include <random>
#include <iostream>
#include <fstream>

// Функция для создания таблицы сдвигов
void generateBadCharShift(const std::string& pattern, std::vector<int>& badCharShift) {
    int m = pattern.size();
    for (int i = 0; i < 256; i++) {
        badCharShift[i] = m;
    }
    for (int i = 0; i < m - 1; i++) {
        badCharShift[static_cast<int>(pattern[i])] = m - 1 - i;
    }
}

// Функция для поиска подстроки в строке с использованием алгоритма Бойера-Мура
bool searchBoyerMoore(const std::string& text, const std::string& pattern) {
    int m = pattern.size();
    int n = text.size();

    std::vector<int> badCharShift(256, 0);
    generateBadCharShift(pattern, badCharShift);

    int i = 0;  // Индекс в строке
    while (i <= n - m) {
        int j = m - 1;  // Индекс в подстроке (с конца к началу)

        // Проверяем с конца подстроки к началу
        while (j >= 0 && pattern[j] == text[i + j]) {
            j--;
        }

        // Если все символы совпали, то подстрока найдена
        if (j < 0) {
            return true;
        } else {
            // Сдвигаем строку на максимум из двух значений
            i += std::max(1, j - badCharShift[text[i + j]]);
        }
    }

    // Подстрока не найдена
    return false;
}

// Google Benchmark тест
static void BM_QuickSort(benchmark::State& state) {
    
    std::ifstream textFile("BM/text.txt");
    std::ifstream patternFile("BM/pattern.txt");

    std::string text;
    std::string pattern;

    std::getline(textFile, text);
    std::getline(patternFile, pattern);
    
    bool result; 
    for (auto _ : state) {
       result = searchBoyerMoore(text, pattern);
       benchmark::DoNotOptimize(result);
    }

    std::cout << "Result: " << result << std::endl;
    textFile.close();
    patternFile.close();
    
}
BENCHMARK(BM_QuickSort);
// ->Arg(50)
// ->Arg(100)
// ->Arg(200)
// ->Arg(500)
// ->Arg(1000)
// ->Arg(10000)
// ->Arg(100000)
// ->Unit(benchmark::kMillisecond)
//->DisplayAggregatesOnly(true);  // Задайте нужные вам размеры массивов

BENCHMARK_MAIN();

// g++ -o x86-bm-standart-test BM/x86_standart_BM_test.cpp -march=native -lpthread -lbenchmark

#include <benchmark/benchmark.h>
#include <iostream>
#include <fstream>
#include <boost/simd/pack.hpp>

void generateBadCharShift(const std::string& pattern, std::vector<int>& badCharShift) {
    int m = pattern.size();
    for (int i = 0; i < 256; i++) {
        badCharShift[i] = m;
    }
    for (int i = 0; i < m - 1; i++) {
        badCharShift[static_cast<int>(pattern[i])] = m - 1 - i;
    }
}

bool searchBoyerMoore(const std::string& text, const std::string& pattern) {
    int m = pattern.size();
    int n = text.size();

    std::vector<int> badCharShift(256, 0);
    generateBadCharShift(pattern, badCharShift);

    int i = 0;
    while (i <= n - m) {
        int j = m - 1;

        boost::simd::pack<char> patternPack(pattern[j]);

        while (j >= 0) {
            boost::simd::pack<char> textPack(&text[i + j]);

            // Если есть хотя бы одно совпадение, выходим из цикла
            if (boost::simd::all_of(textPack == patternPack)) {
                break;
            }

            j--;
        }

        if (j < 0) {
            return true;
        } else {
            i += std::max(1, j - badCharShift[text[i + j]]);
        }
    }

    return false;
}

static void BM_SearchBoyerMoore(benchmark::State& state) {
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

BENCHMARK(BM_SearchBoyerMoore);

BENCHMARK_MAIN();
#include <iostream>
#include <vector>
#include <chrono>
#include <iomanip>
#include <fstream>
#include <algorithm> // Для std::sort
#include <nlohmann/json.hpp>
#include <immintrin.h>

using namespace std;
using json = nlohmann::json;

#define ITERATIONS 10

#pragma region levSSE



int levenshteinSSE(const std::string& str1, const std::string& str2) {
    const size_t len1 = str1.size();
    const size_t len2 = str2.size();

    __m128i* row1 = (__m128i*)malloc((len2 + 1) * sizeof(__m128i));
    __m128i* row2 = (__m128i*)malloc((len2 + 1) * sizeof(__m128i));

    for (size_t j = 0; j <= len2; ++j) {
        row1[j] = _mm_set_epi32(j, 0, 0, 0);
    }

    for (size_t i = 1; i <= len1; ++i) {
        row2[0] = _mm_set_epi32(i, 0, 0, 0);

        for (size_t j = 1; j <= len2; j += 4) {
            __m128i cost = _mm_cmpeq_epi32(_mm_loadu_si128((__m128i*) & str1[i - 1]), _mm_loadu_si128((__m128i*) & str2[j - 1]));

            __m128i insert = _mm_add_epi32(_mm_load_si128(&row2[j - 1]), _mm_set_epi32(1, 1, 1, 1));
            __m128i del = _mm_add_epi32(_mm_load_si128(&row1[j]), _mm_set_epi32(1, 1, 1, 1));
            __m128i replace = _mm_add_epi32(_mm_load_si128(&row1[j - 1]), _mm_sub_epi32(_mm_set_epi32(0, 0, 0, 0), cost));

            __m128i min1 = _mm_min_epi32(insert, del);
            __m128i min2 = _mm_min_epi32(min1, replace);

            _mm_store_si128(&row2[j], min2);
        }

        std::swap(row1, row2);
    }

    int result = _mm_cvtsi128_si32(row1[len2]);

    free(row1);
    free(row2);

    return result;
}
#pragma endregion

#pragma region lev
int levenshteinDistance(const std::string& str1, const std::string& str2) {
    const size_t len1 = str1.size();
    const size_t len2 = str2.size();

    // Создаем матрицу для хранения промежуточных результатов
    std::vector<std::vector<int>> dp(len1 + 1, std::vector<int>(len2 + 1, 0));

    // Инициализируем первую строку и первый столбец
    for (size_t i = 0; i <= len1; ++i) {
        dp[i][0] = static_cast<int>(i);
    }

    for (size_t j = 0; j <= len2; ++j) {
        dp[0][j] = static_cast<int>(j);
    }

    // Заполняем матрицу по правилам алгоритма
    for (size_t i = 1; i <= len1; ++i) {
        for (size_t j = 1; j <= len2; ++j) {
            int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;

            dp[i][j] = std::min({
                dp[i - 1][j] + 1,     // удаление
                dp[i][j - 1] + 1,     // вставка
                dp[i - 1][j - 1] + cost  // замена
                });
        }
    }

    return dp[len1][len2];
}

#pragma endregion

template <typename TimeUnits>
long long Test(int (*algorithm)(const std::string&, const std::string&), const std::string& str1, const std::string& str2, int iterations) {
    long long sum = 0;

    for (int i = 0; i < iterations; i++) {
        auto start_time = chrono::high_resolution_clock::now();
        algorithm(str1, str2);
        auto end_time = chrono::high_resolution_clock::now();

        sum += chrono::duration_cast<TimeUnits>(end_time - start_time).count();
    }

    return sum / iterations;
}

int main() {
    cout << setw(5) << "Size" << setw(25) << "Levenshtein_SSE [mcs]" << setw(20) << "Levenshtein [mcs]" <<
        setw(10) << "L/L_SSE" << endl;
    cout << setfill('-') << setw(65) << "-" << setfill(' ') << endl;

    json resultArray;

    for (int n = 100; n <= 100000000 /* 5000 */; n += 10000000) {
        std::string str1 = "";
        //std::string str1(n, 'a');
        std::string str2(n, 'b');

        auto durationL_SSE = Test<chrono::microseconds>(levenshteinSSE, str1, str2, ITERATIONS);
        auto durationL = Test<chrono::microseconds>(levenshteinDistance, str1, str2, ITERATIONS);

        cout << setw(5) << n << setw(16) << durationL_SSE << setw(23) << durationL
            << setw(15) << fixed << setprecision(2) << (double)durationL / durationL_SSE
            << endl;

        json resultObject;
        resultObject["n"] = n;
        resultObject["durationL_SSE"] = durationL_SSE;
        resultObject["durationL"] = durationL;
        resultObject["L/L_SSE"] = (double)durationL / durationL_SSE;

        resultArray.push_back(resultObject);
    }

    ofstream file("levenshtein_x86_results.json");
    file << setw(4) << resultArray << endl;

    return 0;
}

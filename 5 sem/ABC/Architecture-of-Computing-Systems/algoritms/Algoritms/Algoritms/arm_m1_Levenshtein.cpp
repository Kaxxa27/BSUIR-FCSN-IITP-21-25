//#include <arm_neon.h>
//#include <string>
//#include <vector>
//#include <iostream>
//#include <chrono>
//#include <iomanip>
//#include <fstream>
//#include <algorithm> // ��� std::sort
//#include <nlohmann/json.hpp>
//
//using namespace std;
//using json = nlohmann::json;
//
//#define ITERATIONS 10
//
//#pragma region levNEON
//int levenshteinDistanceNEON(const std::string& str1, const std::string& str2) {
//    const size_t len1 = str1.size();
//    const size_t len2 = str2.size();
//
//    // ������� ������� ��� �������� ������������� �����������
//    std::vector<std::vector<int>> dp(len1 + 1, std::vector<int>(len2 + 1, 0));
//
//    //  �������������� ������ ������ � ������ �������
//    for (size_t i = 0; i <= len1; ++i) {
//        dp[i][0] = static_cast<int>(i);
//    }
//
//    for (size_t j = 0; j <= len2; ++j) {
//        dp[0][j] = static_cast<int>(j);
//    }
//
//    // ��������� ������� �� �������� ��������� � �������������� NEON
//    for (size_t i = 1; i <= len1; ++i) {
//        int prev_diag = i - 1;
//
//        for (size_t j = 1; j <= len2; ++j) {
//            int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;
//
//            // ����������������� �������� � �������������� NEON
//            int32x4_t insert = vld1q_s32(&dp[i][j - 1]);
//            int32x4_t del = vld1q_s32(&dp[i - 1][j]);
//            int32x4_t replace = vld1q_s32(&dp[prev_diag][j - 1]);
//
//            insert = vaddq_s32(insert, vdupq_n_s32(1));
//            del = vaddq_s32(del, vdupq_n_s32(1));
//
//            uint32x4_t cmp = vceqq_s32(vld1q_s32((int*)&str1[i - 1]), vld1q_s32((int*)&str2[j - 1]));
//            replace = vaddq_s32(replace, vreinterpretq_s32_u32(cmp));
//
//            int32x4_t min1 = vminq_s32(insert, del);
//            int32x4_t min2 = vminq_s32(min1, replace);
//
//            vst1q_s32(&dp[i][j], min2);
//
//            //  ��������� �������� prev_diag
//            prev_diag = vgetq_lane_s32(replace, 3);
//        }
//    }
//
//    return dp[len1][len2];
//}
//
//
//#pragma endregion
//
//#pragma region lev
//int levenshteinDistance(const std::string& str1, const std::string& str2) {
//    const size_t len1 = str1.size();
//    const size_t len2 = str2.size();
//
//    // ������� ������� ��� �������� ������������� �����������
//    std::vector<std::vector<int>> dp(len1 + 1, std::vector<int>(len2 + 1, 0));
//
//    // �������������� ������ ������ � ������ �������
//    for (size_t i = 0; i <= len1; ++i) {
//        dp[i][0] = static_cast<int>(i);
//    }
//
//    for (size_t j = 0; j <= len2; ++j) {
//        dp[0][j] = static_cast<int>(j);
//    }
//
//    // ��������� ������� �� �������� ���������
//    for (size_t i = 1; i <= len1; ++i) {
//        for (size_t j = 1; j <= len2; ++j) {
//            int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;
//
//            dp[i][j] = std::min({
//                dp[i - 1][j] + 1,     // ��������
//                dp[i][j - 1] + 1,     // �������
//                dp[i - 1][j - 1] + cost  // ������
//                });
//        }
//    }
//
//    return dp[len1][len2];
//}
//
//#pragma endregion
//
//template <typename TimeUnits>
//long long Test(int (*algorithm)(const std::string&, const std::string&), const std::string& str1, const std::string& str2, int iterations) {
//    long long sum = 0;
//
//    for (int i = 0; i < iterations; i++) {
//        auto start_time = chrono::high_resolution_clock::now();
//        algorithm(str1, str2);
//        auto end_time = chrono::high_resolution_clock::now();
//
//        sum += chrono::duration_cast<TimeUnits>(end_time - start_time).count();
//    }
//
//    return sum / iterations;
//}
//
//int main() {
//    cout << setw(5) << "Size" << setw(25) << "Levenshtein_SSE [mcs]" << setw(20) << "Levenshtein [mcs]" <<
//        setw(10) << "L/L_SSE" << endl;
//    cout << setfill('-') << setw(65) << "-" << setfill(' ') << endl;
//
//    json resultArray;
//
//    for (int n = 100; n <= 100000000; n += 10000000) {
//        std::string str1 = "";
//        // std::string str1(n, 'a');
//        std::string str2(n, 'b');
//
//        auto durationL_NEON = Test<chrono::microseconds>(levenshteinDistanceNEON, str1, str2, ITERATIONS);
//        auto durationL = Test<chrono::microseconds>(levenshteinDistance, str1, str2, ITERATIONS);
//
//        cout << setw(5) << n << setw(16) << durationL_NEON << setw(23) << durationL
//            << setw(15) << fixed << setprecision(2) << (double)durationL / durationL_NEON
//            << endl;
//
//        json resultObject;
//        resultObject["n"] = n;
//        resultObject["durationL_NEON"] = durationL_NEON;
//        resultObject["durationL"] = durationL;
//
//        resultArray.push_back(resultObject);
//    }
//
//    ofstream file("levenshtein_arm_results.json");
//    file << setw(4) << resultArray << endl;
//
//    return 0;
//}
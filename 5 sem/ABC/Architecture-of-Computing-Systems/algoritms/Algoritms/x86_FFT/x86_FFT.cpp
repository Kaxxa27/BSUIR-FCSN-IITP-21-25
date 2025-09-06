#include <iostream>
#include <vector>
#include <chrono>
#include <iomanip>
#include <fstream>
#include <algorithm>
#include <nlohmann/json.hpp>
#include <complex>
#include <immintrin.h>
#include <math.h>
#include <cstdlib>   // Add this for malloc and free

using namespace std;
using json = nlohmann::json;

#define ITERATIONS 10
# define M_PI  3.14159265358979323846  /* pi */

std::vector<std::complex<double>> ensurePowerOfTwo(const std::vector<std::complex<double>>& input) {
    int originalSize = static_cast<int>(input.size());
    int newSize = static_cast<int>(std::pow(2, std::ceil(std::log2(originalSize))));

    std::vector<std::complex<double>> result(newSize, std::complex<double>(0.0f, 0.0f));
    std::copy(input.begin(), input.end(), result.begin());

    return result;
}

#pragma region FFT_SSE
// SSE-optimized FFT
void fftSSE(std::vector<std::complex<double>>& data) {
    int size = static_cast<int>(data.size());

    // Ensure the input size is a power of two
    if (size & (size - 1)) {
        data = ensurePowerOfTwo(data);
        size = static_cast<int>(data.size());
    }

    // Create temporary vectors to store real and imaginary parts
    std::vector<double> real(size);
    std::vector<double> imag(size);

    // Separate real and imaginary parts
    for (int i = 0; i < size; ++i) {
        real[i] = data[i].real();
        imag[i] = data[i].imag();
    }

    // Create complex vectors using SSE
    __m128d* xmm_real = (__m128d*) & real[0];
    __m128d* xmm_imag = (__m128d*) & imag[0];

    // Perform FFT using SSE
    for (int step = 1; step < size; step *= 2) {
        for (int i = 0; i < size; i += 2 * step) {
            for (int j = 0; j < step; ++j) {
                // Butterfly operation
                __m128d wr = _mm_setr_pd(cos(-2.0 * M_PI * j / (2 * step)), cos(-2.0 * M_PI * (j + step) / (2 * step)));
                __m128d wi = _mm_setr_pd(sin(-2.0 * M_PI * j / (2 * step)), sin(-2.0 * M_PI * (j + step) / (2 * step)));

                // Load values
                __m128d even_real = _mm_load_pd(&real[i + j]);
                __m128d even_imag = _mm_load_pd(&imag[i + j]);
                __m128d odd_real = _mm_load_pd(&real[i + j + step]);
                __m128d odd_imag = _mm_load_pd(&imag[i + j + step]);

                // Multiply by twiddle factor
                __m128d odd_real_twiddled = _mm_mul_pd(odd_real, wr);
                __m128d odd_imag_twiddled = _mm_mul_pd(odd_imag, wi);

                // Calculate FFT
                __m128d result_real = _mm_add_pd(even_real, odd_real_twiddled);
                __m128d result_imag = _mm_add_pd(even_imag, odd_imag_twiddled);

                // Store results
                _mm_store_pd(&real[i + j], result_real);
                _mm_store_pd(&imag[i + j], result_imag);
            }
        }
    }

    // Combine real and imaginary parts back into complex numbers
    for (int i = 0; i < size; ++i) {
        data[i] = std::complex<double>(real[i], imag[i]);
    }
}

#pragma endregion

#pragma region FFT_STANDART


vector<complex<double>> FFT(vector<complex<double>>& x) {
    int N = x.size();
    vector<complex<double>> X(N);

    // Base case (N = 1)
    if (N == 1) {
        X[0] = x[0];
        return X;
    }

    // Split x into even and odd parts
    vector<complex<double>> x_even(N / 2), x_odd(N / 2);
    for (int i = 0; i < N / 2; ++i) {
        x_even[i] = x[2 * i];
        x_odd[i] = x[2 * i + 1];
    }

    // Compute the FFT of the even and odd parts
    vector<complex<double>> X_even = FFT(x_even);
    vector<complex<double>> X_odd = FFT(x_odd);

    // Combine the two FFTs to obtain the final FFT
    for (int k = 0; k < N / 2; ++k) {
        complex<double> w = exp(complex<double>(0, -2 * M_PI * k / N));
        X[k] = X_even[k] + w * X_odd[k];
        X[k + N / 2] = X_even[k] - w * X_odd[k];
    }

    return X;
}

void fft(std::vector<std::complex<double>>& input) {
    int size = static_cast<int>(input.size());

    if (size & (size - 1)) {
        input = ensurePowerOfTwo(input);
    }

    FFT(input);
}
#pragma endregion

template <typename TimeUnits>
long long Test(void (*algorithm)(vector<complex<double>>&), vector<complex<double>>& data, int iterations) {
    long long sum = 0;

    for (int i = 0; i < iterations; i++) {
        auto start_time = chrono::high_resolution_clock::now();
        algorithm(data);
        auto end_time = chrono::high_resolution_clock::now();

        sum += chrono::duration_cast<TimeUnits>(end_time - start_time).count();
    }

    return sum / iterations;
}

int main() {
    cout << setw(10) << "Size" << setw(20) << "FFT_SSE [mcs]" << setw(20) << "FFT [mcs]"
        << setw(20) << "FFT/ FFT_SSE" << endl;
    cout << setfill('-') << setw(70) << "-" << setfill(' ') << endl;

    json resultArray;

    for (int n = 1024; n <= 262144 /*1048576*/; n *= 2) {
        vector<complex<double>> complexVector(n, { 1.0, 0.0 });  // Initialize with all 1s

        auto durationFFT_SSE = Test<chrono::microseconds>(fftSSE, complexVector, ITERATIONS);
        auto durationFFT = Test<chrono::microseconds>(fft, complexVector, ITERATIONS);

        cout << setw(10) << n << setw(16) << durationFFT_SSE << setw(20) << durationFFT
            << setw(20) << fixed << setprecision(2) << (double)durationFFT / durationFFT_SSE
            << endl;

        json resultObject;
        resultObject["n"] = n;
        resultObject["durationFFT_SSE"] = durationFFT_SSE;
        resultObject["durationFFT"] = durationFFT;
        resultObject["FFT/FFT_SSE"] = (double)durationFFT / durationFFT_SSE;

        resultArray.push_back(resultObject);
    }

    ofstream file("fft_x86_results.json");
    file << setw(4) << resultArray << endl;

    return 0;
}

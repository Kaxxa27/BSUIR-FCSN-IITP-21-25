#include <benchmark/benchmark.h>
#include <vector>
#include <algorithm>
#include <random>
#include <iostream>


struct Node {
    int key;
    Node* left;
    Node* right;

    Node(int k) : key(k), left(nullptr), right(nullptr) {}
};

Node* insert(Node* root, int key) {
    if (root == nullptr) {
        return new Node(key);
    }

    if (key < root->key) {
        root->left = insert(root->left, key);
    } else if (key > root->key) {
        root->right = insert(root->right, key);
    }

    return root;
}

bool search(Node* root, int key) {
    if (root == nullptr || root->key == key) {
        return (root != nullptr);
    }

    if (key < root->key) {
        return search(root->left, key);
    } else {
        return search(root->right, key);
    }
}

static void BM_Search(benchmark::State& state) {
    Node* root = nullptr;
    const int n = state.range(0);

    for (int i = 0; i < n; ++i) {
        root = insert(root, i);
    }

    for (auto _ : state) {
        // Тестируем поиск случайного ключа в диапазоне [0, n)
        int key_to_find = rand() % n;
        benchmark::DoNotOptimize(search(root, key_to_find));
    }

    // Освобождаем память после теста
    // Для реальных сценариев лучше использовать умные указатели или другие средства управления памятью
    delete root;
}

BENCHMARK(BM_Search)
->Range(8, 8 << 13);

BENCHMARK_MAIN();
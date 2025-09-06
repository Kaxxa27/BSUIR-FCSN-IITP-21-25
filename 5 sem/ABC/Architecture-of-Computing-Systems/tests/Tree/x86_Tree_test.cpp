// g++ -o x86-tree-test Tree/x86_Tree_test.cpp -march=native -lpthread -lbenchmark

#include <benchmark/benchmark.h>
#include <iostream>
#include <emmintrin.h>

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

bool simdSearch(Node* root, int key) {
    __m128i keyVector = _mm_set1_epi32(key);

    while (root != nullptr) {
        __m128i rootKeyVector = _mm_set1_epi32(root->key);

        if (_mm_movemask_epi8(_mm_cmpeq_epi32(keyVector, rootKeyVector)) == 0xFFFF) {
            return true;  // Если все 4 значения совпадают
        }

        if (key < root->key) {
            root = root->left;
        } else if (key > root->key) {
            root = root->right;
        }
    }

    return false;
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
        benchmark::DoNotOptimize(simdSearch(root, key_to_find));
    }

    // Освобождаем память после теста
    // Для реальных сценариев лучше использовать умные указатели или другие средства управления памятью
    delete root;
}

BENCHMARK(BM_Search)->Range(8, 8 << 10);

BENCHMARK_MAIN();
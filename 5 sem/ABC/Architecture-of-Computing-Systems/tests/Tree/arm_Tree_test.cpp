#include <benchmark/benchmark.h>
#include <iostream>
#include <arm_neon.h>

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

bool neonSearch(Node* root, int key) {
    int32_t keyArray[4] = {key, key, key, key};
    int32_t resultArray[4];

    int32x4_t keyVector = vld1q_s32(keyArray);

    while (root != nullptr) {
        int32x4_t rootKeyVector = vld1q_s32(&root->key);

        uint32x4_t compareResult = vceqq_s32(keyVector, rootKeyVector);

        vst1q_s32(resultArray, compareResult);

        if (resultArray[0] && resultArray[1] && resultArray[2] && resultArray[3]) {
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
        benchmark::DoNotOptimize(neonSearch(root, key_to_find));
    }

    // Освобождаем память после теста
    // Для реальных сценариев лучше использовать умные указатели или другие средства управления памятью
    delete root;
}

BENCHMARK(BM_Search)->Range(8, 8 << 10);

BENCHMARK_MAIN();
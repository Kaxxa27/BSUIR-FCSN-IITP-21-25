#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <sys/time.h>
#include <limits.h>

typedef struct {
    int* array;
    int start;
    int end;
} arr_part;

void bubble_sort(arr_part* arr_part);
void merge_arrays(int* array, int array_size, int num_threads, int part_size);

int main() {
    int num_threads;
    printf("Threads: ");
    scanf("%d", &num_threads);
    if (num_threads < 1) {
        return 0;
    }
    int array_size;
    printf("Array size: ");
    scanf("%d", &array_size);
    int *array = malloc(array_size * sizeof(int));

    srand(time(NULL));
    for (int i = 0; i < array_size; i++)
        array[i] = rand() % 100000;

    int part_size = array_size / num_threads;
    pthread_t threads[num_threads];
    arr_part parts[num_threads];

    struct timeval start_time, end_time;
    gettimeofday(&start_time, NULL);

    for (int i = 0; i < num_threads; i++) {
        parts[i].array = array;
        parts[i].start = i * part_size;
        parts[i].end = (i == num_threads - 1) ? array_size - 1 : (i + 1) * part_size - 1;
        pthread_create(&threads[i], NULL, (void *(*)(void *)) bubble_sort, &parts[i]);
    }

    for (int i = 0; i < num_threads; i++) {
        pthread_join(threads[i], NULL);
    }

    merge_arrays(array, array_size, num_threads, part_size);

    gettimeofday(&end_time, NULL);
    double execution_time = (end_time.tv_sec - start_time.tv_sec) + (end_time.tv_usec - start_time.tv_usec) / 1e6;

    printf("Result: %.3f\n", execution_time);
    free(array);
    return 0;
}


void bubble_sort(arr_part* arr_part) {
    for (int i = arr_part->start; i <= arr_part->end; i++) {
        for (int j = i + 1; j <= arr_part->end; j++) {
            if (arr_part->array[i] > arr_part->array[j]) {
                int temp = arr_part->array[i];
                arr_part->array[i] = arr_part->array[j];
                arr_part->array[j] = temp;
            }
        }
    }

    pthread_exit(NULL);
}

void merge_arrays(int* array, int array_size, int num_threads, int part_size) {
    int temp[array_size];
    int index[num_threads];

    for (int i = 0; i < num_threads; ++i) {
        index[i] = i * part_size;
    }

    for (int i = 0; i < array_size; ++i) {
        int min_val = INT_MAX;
        int min_thread = -1;

        for (int j = 0; j < num_threads; ++j) {
            if (index[j] < (j + 1) * part_size && array[index[j]] < min_val) {
                min_val = array[index[j]];
                min_thread = j;
            }
        }

        temp[i] = min_val;
        ++index[min_thread];
    }

    for (int i = 0; i < array_size; ++i) {
        array[i] = temp[i];
    }
}
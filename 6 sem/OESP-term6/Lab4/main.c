#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <signal.h>

int g_is_end = 0;
// Обработчик сигнала SIGINT
void sigint_handler(int signum) {

    __pid_t pid = fork();

    if (pid == 0) {
        // В дочернем процессе, продолжаем выполнение
        printf("Creating a copy of the process...\n");
        printf("Child PID: ");
        printf("%d", pid);
        printf("\n");
        g_is_end = 770;
        
    } else if (pid > 0) {
        // Terminated parent process 
        printf("Terminating parent process...\n");
        printf("Child PID: ");
        printf("%d", pid);
        printf("\n");
        exit(1);
    } else {
        // Ошибка при создании копии процесса
        printf("Failed to create a copy of the process.\n");
    }
}

int main() {
    // Установка обработчика сигнала SIGINT
    signal(SIGINT, sigint_handler);

    printf("Starting programm...\n");

    // Бесконечный цикл для демонстрации "живости" процесса
    while (1) {
        // Генерация случайного числа и запись его в файл
        //int random_number = rand() % 100;
        FILE *file = fopen("random_number.txt", "w");
        if (file != NULL) {
            fprintf(file, "%d\n", g_is_end);
            fclose(file);
        }
        g_is_end++;
        sleep(1);

        if(g_is_end == 777)
        {
            FILE *file = fopen("random_number.txt", "w");
            if (file != NULL) {
                fprintf(file, "%s\n", "The programm is END. Success!");
                fclose(file);
                exit(1);
            }
        }
    }

    return 0;
}

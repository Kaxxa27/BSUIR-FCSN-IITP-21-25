import numpy as np

def hungarian_algorithm(cost_matrix):
    cost_matrix = np.array(cost_matrix)
    original_cost_matrix = cost_matrix.copy()  # Сохраняем оригинальную матрицу
    n, m = cost_matrix.shape

    # Уменьшаем матрицу по строкам
    for i in range(n):
        cost_matrix[i] -= cost_matrix[i].min()

    # Уменьшаем матрицу по столбцам
    for j in range(m):
        cost_matrix[:, j] -= cost_matrix[:, j].min()

    marked = np.zeros((n, m), dtype=bool)
    row_cover = np.zeros(n, dtype=bool)
    col_cover = np.zeros(m, dtype=bool)

    # Найти нулевую ячейку
    def find_zero():
        for i in range(n):
            for j in range(m):
                if cost_matrix[i, j] == 0 and not row_cover[i] and not col_cover[j]:
                    return i, j
        return -1, -1

    # Основной цикл
    while True:
        i, j = find_zero()
        if i == -1:
            break
        marked[i, j] = True
        row_cover[i] = True
        col_cover[j] = True

    # Подсчет назначений и общей стоимости
    assignments = []
    total_cost = 0

    for i in range(n):
        for j in range(m):
            if marked[i, j]:
                assignments.append((i, j))
                cost = original_cost_matrix[i][j]  # Используем оригинальную матрицу
                total_cost += cost
                # print(f"Добавляем стоимость задачи {j + 1} для рабочего {i + 1}: {cost}")  # Отладочное сообщение

    return assignments, total_cost

# Пример использования
original_cost_matrix = [
    [7, 2, 1, 9, 4],
    [9, 6, 9, 5, 5],
    [3, 8, 3, 1, 8],
    [7, 9, 4, 2, 2],
    [8, 4, 7, 4, 8]
]

assignments, total_cost = hungarian_algorithm(original_cost_matrix)
print("Оптимальное распределение задач:")
for worker, task in assignments:
    print(f"Рабочий {worker + 1} назначен на задачу {task + 1}")
print(f"Общая стоимость: {total_cost}")


# Задача о наидлиннейшем пути из вершины s в вершину t в направленном графе без контуров
from collections import defaultdict

# Функция для поиска наидлиннейшего пути в графе
def longest_path(n, edges, s, t):
    # Создаем граф и массивы для хранения входящих дуг и их длин
    graph = defaultdict(list)  # граф как список смежности
    incoming_edges = defaultdict(list)  # входящие дуги для каждой вершины
    lengths = {}  # длина каждой дуги

    # Заполняем граф и вспомогательные структуры
    for u, v, length in edges:
        graph[u].append(v)
        incoming_edges[v].append(u)
        lengths[(u, v)] = length

    # Инициализация массива для хранения максимальной длины пути
    OPT = [-float('inf')] * (n + 1)
    # Инициализация массива для хранения предпоследней вершины в пути
    x = [-1] * (n + 1)

    # Начальная вершина s имеет длину пути 0
    OPT[s] = 0

    # Топологическая сортировка вершин
    order = topological_sort(n, graph)

    # Прямой ход динамического программирования
    for u in order:
        if u == t:
            break
        for v in graph[u]:
            if OPT[u] + lengths[(u, v)] > OPT[v]:
                OPT[v] = OPT[u] + lengths[(u, v)]
                x[v] = u

    # Проверяем достижимость вершины t из s
    if OPT[t] == -float('inf'):
        print("Вершина t недостижима из вершины s")
        return

    # Восстанавливаем наидлиннейший путь
    path = []
    current = t
    while current != -1:
        path.append(current)
        current = x[current]
    path.reverse()  # путь восстанавливаем в обратном порядке

    # Вывод результата
    print(f"Наидлиннейший путь из вершины {s} в вершину {t}: {path}")
    print(f"Максимальная длина пути: {OPT[t]}")

# Функция для топологической сортировки графа
def topological_sort(n, graph):
    visited = [False] * (n + 1)
    order = []

    def dfs(v):
        visited[v] = True
        for neighbor in graph[v]:
            if not visited[neighbor]:
                dfs(neighbor)
        order.append(v)

    for i in range(1, n + 1):
        if not visited[i]:
            dfs(i)

    order.reverse()
    return order

# Пример данных: количество вершин, дуги графа, начальная и конечная вершины
n = 6  # количество вершин
edges = [
    (1, 2, 5),  # дуга из 1 в 2 с длиной 5
    (1, 3, 3),  # дуга из 1 в 3 с длиной 3
    (3, 4, 4),  # дуга из 3 в 4 с длиной 4
    (4, 5, 2),  # дуга из 4 в 5 с длиной 2
    (5, 6, 1)   # дуга из 5 в 6 с длиной 1
]
s = 1  # начальная вершина
t = 6  # конечная вершина

# Вызов функции для поиска наидлиннейшего пути
longest_path(n, edges, s, t)

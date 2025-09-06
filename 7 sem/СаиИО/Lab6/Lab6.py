from collections import deque, defaultdict


# Функция для построения направленного графа G* из двудольного графа G
def build_directed_graph(V1, V2, edges):
    G_star = defaultdict(list)
    s = 's'  # Дополнительная вершина s
    t = 't'  # Дополнительная вершина t

    # Добавляем дуги из вершин V1 в V2
    for u, v in edges:
        if u in V1 and v in V2:
            G_star[u].append(v)

    # Добавляем дуги из s в вершины V1
    for u in V1:
        G_star[s].append(u)

    # Добавляем дуги из вершин V2 в t
    for v in V2:
        G_star[v].append(t)

    return G_star


# Функция для поиска (s, t)-пути в графе G* с использованием BFS
def bfs_path(graph, start, end):
    queue = deque([start]) # Очередь с не посещенными вершинами
    paths = {start: []}

    while queue:
        node = queue.popleft()
        for neighbor in graph[node]:
            if neighbor not in paths:
                paths[neighbor] = paths[node] + [(node, neighbor)]
                queue.append(neighbor)
                if neighbor == end:
                    return paths[neighbor]
    return None


# Функция для корректировки графа G* после нахождения (s, t)-пути
def adjust_graph(graph, path):
    # Удаляем первую и последнюю дуги пути, остальные обращаем
    graph[path[0][0]].remove(path[0][1])  # Удаляем первую дугу
    graph[path[-1][0]].remove(path[-1][1])  # Удаляем последнюю дугу

    for u, v in path[1:-1]:
        graph[v].append(u)  # Обращаем дугу


# Функция для нахождения наибольшего паросочетания
def max_matching(V1, V2, edges):
    # Шаг 1: Построение направленного графа G*
    G_star = build_directed_graph(V1, V2, edges)

    # Шаги 3-5: Поиск и корректировка (s, t)-путей
    while True:
        path = bfs_path(G_star, 's', 't')
        if not path:  # Если пути нет, завершить алгоритм
            break
        adjust_graph(G_star, path)  # Корректировка графа по найденному пути

    # Шаг 4: Формирование наибольшего паросочетания
    matching = set()
    for v in V2:
        for u in G_star[v]:
            if u in V1:
                matching.add((u, v))

    return matching


# Пример данных: вершины долей и ребра двудольного графа
V1 = {'a', 'b', 'c'}  # Первая доля графа
V2 = {'x', 'y', 'z'}  # Вторая доля графа
edges = [('a', 'x'), ('b', 'x'), ('b', 'y'), ('c', 'y'),('c', 'x'),('c', 'z')]  # Ребра двудольного графа

# Вызов функции для нахождения наибольшего паросочетания
matching = max_matching(V1, V2, edges)

# Вывод результата
print("Наибольшее паросочетание:", matching)

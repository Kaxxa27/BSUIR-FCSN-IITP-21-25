from collections import deque, defaultdict

class MaxFlow:
    def __init__(self, vertices):
        self.graph = defaultdict(dict)  # Словарь для хранения графа с рёбрами и их пропускными способностями
        self.vertices = vertices  # Вершины графа

    def add_edge(self, u, v, capacity):
        # Добавление рёбер и их пропускных способностей
        self.graph[u][v] = capacity  # Пропускная способность прямого ребра
        self.graph[v][u] = 0  # Обратное ребро с нулевой пропускной способностью (по умолчанию)
    '''
        Храним ребра в словаре, если такого ребра нет то присваиваем ему значение по умолчанию
        0
    '''
    def bfs(self, source, sink, parent):
        # Поиск в ширину для нахождения (s, t)-пути
        visited = {v: False for v in self.vertices}  # Словарь для отслеживания посещённых вершин
        queue = deque([source])  # Инициализируем очередь BFS
        visited[source] = True  # Помечаем источник как посещённый

        while queue:
            u = queue.popleft()  # Извлекаем вершину из очереди

            for v in self.graph[u]:
                # Проверка, доступно ли ребро (положительная пропускная способность)
                if not visited[v] and self.graph[u][v] > 0:
                    queue.append(v)  # Добавляем вершину в очередь
                    visited[v] = True  # Помечаем как посещённую
                    parent[v] = u  # Запоминаем путь
                    if v == sink:  # Если достигли стока, возвращаем True
                        return True
        return False  # Если путь не найден, возвращаем False

    def ford_fulkerson(self, source, sink):
        parent = {v: None for v in self.vertices}  # Словарь для хранения пути
        max_flow = 0  # Инициализируем максимальный поток

        # Основной цикл, пока есть путь из источника в сток
        while self.bfs(source, sink, parent):
            # Находим минимальную пропускную способность по найденному пути

            path_flow = float('Inf')  # Изначально берем бесконечность
            v = sink
            '''
                 идем по путии ищем ребро с минимально пропускной способностью
                
            '''
            while v != source:  # Прослеживаем путь от стока к источнику
                u = parent[v]
                path_flow = min(path_flow, self.graph[u][v])  # Находим минимальную пропускную способность
                v = u

            # Обновляем пропускные способности рёбер
            v = sink
            while v != source:
                u = parent[v]
                self.graph[u][v] -= path_flow  # Если по пути вычитаем
                self.graph[v][u] += path_flow  # против пути добавляем
                v = u

            max_flow += path_flow  # Увеличиваем общий поток

            # Отладочная информация
            print(f"Найден путь с пропускной способностью {path_flow}. Текущий максимальный поток: {max_flow}")

        return max_flow

# Пример использования
if __name__ == "__main__":
    # Создаем экземпляр класса
    vertices = ['s', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 't']
    max_flow = MaxFlow(vertices)

    # Добавляем рёбра и их пропускные способности (15 рёбер)
    max_flow.add_edge('s', 'a', 5)
    max_flow.add_edge('s', 'b', 8)
    max_flow.add_edge('s', 'c', 3)
    max_flow.add_edge('a', 'd', 4)
    max_flow.add_edge('a', 'e', 2)
    max_flow.add_edge('b', 'e', 3)
    max_flow.add_edge('b', 'f', 7)
    max_flow.add_edge('c', 'f', 4)
    max_flow.add_edge('d', 'g', 3)
    max_flow.add_edge('e', 'g', 6)
    max_flow.add_edge('e', 'h', 5)
    max_flow.add_edge('f', 'h', 8)
    max_flow.add_edge('g', 't', 9)
    max_flow.add_edge('h', 't', 5)
    max_flow.add_edge('f', 't', 4)

    # Вычисляем максимальный поток
    source = 's'
    sink = 't'
    result = max_flow.ford_fulkerson(source, sink)

    # Вывод результата
    print(f"Максимальный поток: {result}")



# Вычислить Y по методу и X 
def CalculateY(method, x, eps):
    n = 1 # Количество узлов от x0 до x для точности eps

    while True:
        y_list = method(x, n)
        y_list_correctly = method(x, n * 2)
        max_delta = max(abs(y_list_correctly[2 * i] - y_list[i]) for i in range(n + 1)) 

        if (max_delta < eps):
            return round(y_list_correctly[-1], 6), n * 2
        else:
            n *= 2

# Создать список Y по методу и списку X
def CalculateListY(method, x_list, eps):
    y_list = [] # Список игреков
    n_list = [] # Список n (числа узлов) для каждой точки
    for x in x_list:
        y, n = CalculateY(method, x, eps)
        y_list.append(y)
        n_list.append(n)

    return y_list, max(n_list), sum(n_list) / len(x_list)

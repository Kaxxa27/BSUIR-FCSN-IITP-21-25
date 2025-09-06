#Найти максимальный по модулю элемент списка и /
# сумму элементов списка, расположенных между первым и вторым отрицательными элементами


    #9. Предусмотреть способы инициализации последовательности: с помощ\
    # ью функции генератора и пользовательского ввода. Оформить способы\
    #  инициализации в виде отдельных функций, которые на вход принимаю\
    # т последовательность для инициализации, и сгруппировать эти функц\
    # ии в отдельный модуль от основной функции программы.
    #10. Продемонстрировать использование декоратора в любом из заданий
import random

def work():
    while (True):
        try:
            x = input("Choose init (1 for manual, 2 for generator): ")
            if(x == '1'):
                s = list(input("Enter list: ").split())#list(map(int, input("Enter list: ").split()))
                arr = manual_init(s)
            if(x == '2'):
                while (True):
                    s = input("Enter quantity of elements: ")
                    try:
                        c = int(s)
                        break
                    except ValueError:
                        print("Wrong input")
                    print(gen)
                i = 0
                arr = []
                while(i<c):
                    arr.append(next(generator_init(c)))
                    i+=1
            else:
                raise ValueError
            break
        except ValueError:
            print ("Wrong input")
    
    print("максимальный по модулю элемент списка")
    print(max_abs_element(arr))
    print("сумма элементов списка")
    print(sum_of_list(arr))
    print("сумма элементов списка, расположенных между первым и вторым отрицательными элементами")
    print(sum_between_negative(arr))

def max_abs_element(arr):
    max = abs(arr[0])
    for x in arr:
        if(abs(x) > max):
            max = x
    return max

def sum_of_list(arr):
    sum = 0
    for x in arr:
        sum += x
    return sum

def sum_between_negative(arr):
    sum = 0
    summing = False
    for x in arr:
        if (x < 0 and summing == True): break
        if (summing): sum += x
        if (x < 0 and summing == False): summing = True
    return sum

def manual_init(s):
    arr = map(int, s)
    return arr


def generator_init(c):
    yield random.randint(-1000000, 1000000)

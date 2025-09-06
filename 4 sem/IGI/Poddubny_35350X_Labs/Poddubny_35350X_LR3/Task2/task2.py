#Организовать цикл, который принимает целые числа и суммирует их. Окончание цикла – ввод отрицательного числа
def sum(a, b):
    return a + b

def work():
    s = 0
    while (True):
    
        user_input = input("Enter number: ")
        try:

            if (user_input[0] == '-'): 
                print (f"Summ: {s}")
                break
            
            s = sum(s, int(user_input))
        
        except ValueError:
            print("Wrong input")


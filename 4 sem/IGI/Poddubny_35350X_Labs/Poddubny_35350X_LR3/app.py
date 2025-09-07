# 353505 Paddziarehin Ivan 
# LabWork 3 
from Task1 import task1
from Task2 import task2
from Task3 import task3
from Task4 import task4
from Task5 import task5


while(True):

    print('''
    1. Task1
    2. Task2
    3. Task3
    4. Task4
    5. Task5
    0. quit''')
    c = input("Enter choice: ")

    if(c == '0'):
        break

    elif(c == '1'):
        task1.work()

    elif(c == '2'):
        task2.work()

    elif(c == '3'):
        task3.work()

    elif(c == '4'):
        task4.work()

    elif(c == '5'):
        task5.work()

    

        






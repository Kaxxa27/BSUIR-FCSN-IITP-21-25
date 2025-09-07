from Task1.runner import Task1
from Task2.runner import Task2
from Task3.runner import Task3
from Task4.runner import Task4
from Task5.runner import Task5

if __name__ == "__main__":
    while (True):
        print("""
            0. exit
            1. 1 task
            2. 2 task
            3. 3 taskw
            4. 4 task
            5. 5 task"""
        )
        c = input("enter choice: ")
        if (c == '0'):
            break
        if (c == '1'):
            Task1.run()
        if (c == '2'):
            Task2.run()
        if (c == '3'):
            Task3.run()
        if (c == '4'):
            Task4.run()
        if (c == '5'):
            str1 = input("enter n: ")
            str2 = input("enter m: ")
            
            try:
                n = int(str1)
                m = int(str2)
            except:
                continue

            Task5.run(n, m)

        
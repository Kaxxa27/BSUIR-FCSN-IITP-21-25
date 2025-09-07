#В строке, вводимой с клавиатуры, подсчитать количество символов, лежащих в диапазоне от 'f' до 'y'

def work():
    user_input = input("Enter string: ")
    print(between_f_and_y(user_input))

def between_f_and_y(str):

    i = 0
    righter_y = -1
    for c in str:

        if (c == 'f'):
            j = i

            while (j < len(str)):
                if (str[j] == 'y'):
                    righter_y = j
                j += 1
            if (righter_y == -1):
                return "there is no 'y' behind the 'f'"
            
            return righter_y - i - 1
        i += 1
 
    
    
    
    
    
    #if rastoyanie < 0 - nahui idet

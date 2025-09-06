#а) определить количество слов, заключенных в кавычки;/
# б) определить, сколько раз повторяется каждая буква;/
# в) вывести в алфавитном порядке все словосочетания, отделенные запятыми

def work():
    str = "So she was considering in her own mind, as well as she could, for the hot day made her feel very sleepy and stupid, whether the pleasure of making a daisy-chain would be worth the trouble of getting up and picking the daisies, when suddenly a White Rabbit with pink eyes ran close by her."
    print("количество слов, заключенных в кавычки")
    print(count_words(str))
    print("сколько раз повторяется каждая буква")
    print(count_repeating_letters(str))
    print("все словосочетания, отделенные запятыми в алфавитном порядке")
    print(phrases_sorted_in_alphabet(str))


def count_words(str):
    c = 0
    for char in str:
        if (char == ' '):
            c+=1
    c+=1
    return c

def count_repeating_letters(str):
    count = [str[0],1]
    for char in str:
        i = 0
        while (i < len(count)):
            if (ord(char) < 65 or ord(char) > 122 or ord(char) > 132 and ord(char) < 141): break
            if (ord(count[i]) == ord(char) or ord(count[i]) + 32 == ord(char) or ord(count[i]) == ord(char) + 32):    
                count[i+1] += 1                                                   
                break
            if (i == len(count) - 2):
                count.append(char)
                count.append(1)
                break
            i+=2
    return count

def phrases_sorted_in_alphabet(str):
    result = []
    s = ''
    i = 0
    while (i < len(str)):
        if (str[i] == ','):
            i += 2
            while (i < len(str)):
                if (str[i] == '.'or str[i] == ','): 
                    i += 1
                    break
                s += str[i]
                i += 1
            result.append(s)
            s = ''
            continue
        i += 1
    result.sort()
    return result
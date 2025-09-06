; Распечатать строку [Макрос]
Print macro str
    push ax
    push dx
    
    mov ah,09h ; Номер функции DOS   
    lea dx,str ; Смещение str в DX   
    int 21h    ; 21 прерывание
    
    pop dx
    pop ax     
endm
 
; Распечатать символ [Макрос]
printSymb macro ascii 
   mov ah,06h
   mov dl,ascii
   int 21h 
endm

; Макрос для вывода элементов массива на консоль
Output_Array_In_Console macro       
    
    lea si, Array  ; SI смещение массива Array
    mov Array_Counter, 0d ; Занулить переменную 
    
    dec Array_Size ; Для исправления индексов, потом оно восстанавливается
    
    Print New_Line 
    Print Output_Array_Str 
    
    Output:    
    Print Probel_Str
    Print_Number [si] ; Вывод всех чисел массива  
    Print Comma_Str 
    add si,2d 
    mov dx, Array_Size
    cmp dx,Array_Counter
    jz End_Macro 
    inc Array_Counter
    jmp Output 
    
    End_Macro:
    inc Array_Size    
endm 

; Модель памяти (код в 1 сегменте, данные и стек в DGROUP)
.model small  

; Сегмент кода 
.code         

; Начало прогарммы
START: 
    
    mov ax,@data  ; @data - идентификатор сегмента данных
    mov ds,ax     ; Адрес сегмента данных — в DS      
    mov es,ax     ; Адрес сегмента данных — в ES    


    
    ;====Задание максимальной длины для вводимого числа======
    lea di,String_Number ; Смещение строки в DI
    mov al,06d           ; Максимальное число символов для ввода      
    stosb                ; AL в ES:DI 
    ;========================================================
    
    
    ;=================== Задание  ===================  
    Print Task_Str
    Print New_Line 
    ;========================================================
     
     
    ;===================Ввод размера массива ( 0 < size < 30) ===================   
    Enter_Array_Size:   
     
    Print Enter_ArraySize_Str  
    call Enter_Number  
    Print New_Line     
     
    ; Array_Size = Main_Number  
    mov dx, Main_Number
    mov Array_Size, dx 
     
    cmp Array_Size, 30d; Если больше чем 30 элементов
    ja Exeption_Array_Size_More_Than_30
     
    cmp Array_Size, 0d; Если 0 элементов
    jz Exeption_Array_Size_Less_Than_1         
    ;========================================================
    
     
    ;===================Заполнение массива===================   
    
    Print Enter_Numbers_In_Array_Str
    
    mov Array_Counter, 0d
    lea si, Array
    Enter_Numbers_In_Array_Loop_Start: 
    
    mov dx, Array_Counter
    
    cmp dx, Array_Size
    jz After_Entering_Array
    
    Print New_Line 
    Print_Number Array_Counter   
    push si
    call Enter_Number   ; Число в Main_Number 
    pop si
     
    mov dx, Main_Number ; Число Main_Number -> Array[index]
    mov [si], dx
    add si, 2d
                           
    inc Array_Counter  ; Index для вывода [0],[1]...
    
    jmp Enter_Numbers_In_Array_Loop_Start
    ;========================================================
      
    After_Entering_Array: ; Продолжение программы после заполнения массива
       
    ; Алгоритм подсчета наиболее встречающегося элемента в массиве 
    ;====================Главный алгоритм====================
    lea si, Array  ; SI смещение массива Array, SI = Начало массива
   
    mov Array_Counter, 0d; Счетчик массива для всего массива
    mov Pod_Array_Counter, 0d; Счетчик для прохода в подмассиве   
   
    mov ax, Array_Size ; Размер массива в AX
     
    mov dx, 0d
    While: 
    
    cmp ax, Array_Counter
    jz Out_Of_Algoritm
    
    mov Curr_Address, si; В текущий адрес помещаем элемент массива, сохраняем 
   ; inc Counter ; Как минимум всегда один такой элемент 
    push si ; Сохраняем адрес текущего  элемента 
    
    lea si, Array  
    Inside_While:   
            
      cmp ax, Pod_Array_Counter
      jz After_Inside_While 
    
      inc Pod_Array_Counter   
      
      ;add si,2d ; Переход к след. элементу массива 
      mov dx,[si] ; В DX = след. элемент массива
      
      mov bx, Curr_Address  
      mov bx, [bx]
      cmp dx, bx ; Если такой же элемент, то надо Counter++  
      jz If_Same_Element
      
      Continue: ; В Counter у нас кол-во одинаковых элементов по адресу Curr_Address
      add si,2d ; Переход к след. элементу массива 
      jmp Inside_While
    
    After_Inside_While: 
     pop si
     ; В Max_Counter заносим максимальное кол-во элементов
    
     mov dx, Max_Counter 
     cmp dx, Counter  
     jl Counter_In_MaxCounter     ; Если Max_Counter <  Counter => Max_Counter = Counter
     
     Posle_Proverki:
     
     add si, 02d
     inc Array_Counter 
     mov Counter, 0d  
     mov Pod_Array_Counter, 0d
     jmp While  
    ;========================================================   
    
    Out_Of_Algoritm:    
    
   ; В Next_Address хранится адрес нашего самого часто втречающегося элемента
    mov bx, Next_Address 
    mov dx, [bx] 
    
    ; Вывод результата
    
    Print New_Line
    Print Answer_Str   
    Print_Number dx 
    
    Print New_Line
    Output_Array_In_Console ; Макрос для вывода элементов массива на консоль  
      
    Print New_Line   
    Print End_Programm_Str  
    jmp end 
    
    
    
    ;==================== Прыжки для алгоритма ====================
    Counter_In_MaxCounter: 
    mov dx, Counter ;   Max_Counter = Counter
    mov Max_Counter, dx
    mov dx, Curr_Address
    mov Next_Address, dx ; в Next_Address хранится самый встречабщийся элемент
    jmp Posle_Proverki
          
    If_Same_Element: ; Если такой же элемент 
       inc Counter ; Увеличить счетчик таких элементов  
       jmp Continue
    ;========================================================
    
      
    ;====================Процедура ввода числа====================    
    
    ; Работает правильно, в Main_Number = Введеное число
    Enter_Number PROC near  
        
    pop bp; Загрузить в стек адрес возвращения 
    Enter_Number_Loop_Start:
      
    ; Ввод числа в строку  
    mov ah,0Ah           ; Функция DOS (считать строку символов из STDIN в буфер) 
    lea dx,String_Number; Смещение строки в DX
    int 21h       
       
  
    ; Вычисление длины введеного числа 
    lea si, String_Number ; Смещение в SI наше число String_Number
    inc si     ; SI++  ; Возможно не надо !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    mov ax, 0d ; Занулить AX
    lodsb      ; DS:SI в AL
    mov cx,ax  ; CX = длина введеного числа 
    
    cmp cx, 0d ; Если длина = 0, то Exeption_NumberIsZero -> 
    jz Exeption_NumberIsZero    
    
    mov Main_Number, 0d; Занулим главное число 
    ; Будем в AX хранить степент 10 (TEN) -> 
    ; 1234 -> 4 * 10^0 + 3 * 10^1 + 2 * 10^2 + 1 * 10^3 = 1234
    mov ax, 01d ; Будем в AX хранить степень 10
    
    NumberStr_In_Number_Loop:
    mov bx, 0d; Занулим BX     
    mov si, cx; SI = CX -> идем с конца слова, можно было так же поменять флаг DF(направления)  
    inc si; 
    mov bl, String_Number[si]
    
    ; Проверка на то, что введеное число(str) является числом(int)
    Proverka_Na_SymbolASCII:
    
    cmp bl, 48d; Сверка с нулем [ASCII] 48d = 0 
    jl Exeption_Invalid_Number; bl < 48d
    
    cmp bl, 57d; Сверка с 9 [ASCII] 57d = 9 
    jg Exeption_Invalid_Number; bl > 57d  
    
    sub bl, 48d; Получить реальное число, "N" - 48 = N
    
    push ax; Сохранение AX
    
    clc; Очистить CF
    mul bx; bl = bl * AX -> Реальное число * 10^ax  ; BX !!!!!
    
    jc Exeption_OverFlow   ; Если есть флаг переноса от mul 
    add Main_Number, ax    ;  Main_Number + ax = Полноценное число
    jc Exeption_OverFlow   ; Если есть флаг переноса от add
      
    pop ax; Восстановление AX   
    
    mul TEN; Увеличиваем AX * 10 
    dec cx; Уменьшаем счетчик
    
    cmp cx,0d
    
    jne NumberStr_In_Number_Loop    
    
    push bp ; Загрузить их стека адерс для ret
    
    ret ;Вернуться туда, откуда была вызвана PROC
    
    Enter_Number endp
    ;=======================================================
    
    
    ;====================Процедура вывода числа====================  

    Print_Number_Proc PROC near 
    
    mov cx,0h ; Количество цифр у числа
    
    ; Добавление цифр в тек
    pushDigitsWhile:
        mov dx,0h
        mov ax,Number_General ; AX = число
        div ten              ; DX:AX / ten = DX:AX
   
        mov Number_General,ax ; numberGeneral уже /= 10

        add dx,48d ; DX += 48 (перевод в ASCII)
        push dx ; DX = текущая цифра числа
        
        inc cx ; CX ++  
        
        ; Условие выхода из цикла
        cmp Number_General,0h
    jne pushDigitsWhile 
    
    ; Доставание из стека и печать цифр
    printDigitsWhile: 
        mov dx,0h
        mov ax,Number_General ; AX = число
        div ten              ; DX:AX / ten = DX:AX
   
        mov Number_General,ax ; numberGeneral уже /= 10

        ; Цифра числа DX 
        ; прерывание берет ASCII символа из DL
        pop dx
        mov ah,06h
        int 21h
        
        
        ; Условие выхода из цикла  
        dec cx
        cmp cx,0h
    jne printDigitsWhile 
  
    ret
    
    Print_Number_Proc endp

   ; Макрос - передает число в процедуру [Number_General]  
    Print_Number macro digit
    
    Print Index_Array_Start_Str
    
    push ax
    
    mov ax,digit 
    mov Number_General,ax
    call Print_Number_Proc 
                               
    Print Index_Array_End_Str
                               
    pop ax
    endm      
    ;=================================================================
    
    ;====================Exeption_Array_Size_More_Than_30==============
    Exeption_Array_Size_More_Than_30:   
    Print New_Line
    Print Exeption_Array_Size_More_Than_30_Str   
    Print New_Line 
    jmp Enter_Array_Size
    ;=======================================================   
    
    ;====================Exeption_Array_Size_Less_Than_1==============
    Exeption_Array_Size_Less_Than_1:   
    Print New_Line
    Print Exeption_Array_Size_Less_Than_1_Str   
    Print New_Line 
    jmp Enter_Array_Size
    ;=======================================================
    
    ;====================Exeption_NumberIsZero==============
    Exeption_NumberIsZero:       
    Print New_Line
    Print Exeption_NumberIsZero_Str   
    Print New_Line 
    jmp Enter_Number_Loop_Start
    ;=======================================================  
    
    ;====================Exeption_OverFlow==============
    Exeption_OverFlow:         
    pop ax; Восстановление AX     
    Print New_Line
    Print Exeption_OverFlow_Str   
    Print New_Line 
    jmp Enter_Number_Loop_Start
    ;=======================================================
    
    ;====================Exeption_Invalid_Number==============
    Exeption_Invalid_Number: 
    Print New_Line
    Print Exeption_Invalid_Number_Str   
    Print New_Line 
    jmp Enter_Number_Loop_Start
    ;=======================================================
    
    ;====================Конец программы====================
    end:
    mov ax,4C00h   ; 4C (выход из программы) в AH
                   ; 00 (успешное звершение) в AL 
    int 21h        ; Функция DOS "Завершить программу" 
    ;=======================================================
      
; Сегмент данных  
.data  

   ;Exceptions 
   Exeption_NumberIsZero_Str db "You didn't enter a number, please repeat the input!$"
   Exeption_Invalid_Number_Str db "The number is entered in the wrong format, repeat the input!$"
   Exeption_OverFlow_Str db "Oops, you've caught an overflow(65535), repeat the input!$"
   
   Exeption_Array_Size_More_Than_30_Str db "The size of the array exceeds the allowed size (30)!$"
   Exeption_Array_Size_Less_Than_1_Str db "Array size is less than allowed size(1) !$"
   
   ; Strings
   New_Line db 0Ah, 0Dh, '$'; Переход на новую строку
   Enter_ArraySize_Str db "Enter array size ( 1 <= size <= 30):$"
   Enter_Numbers_In_Array_Str db "Enter numbers in array:$"   
   Index_Array_Start_Str db "[$"  
   Index_Array_End_Str db "]$"
   End_Programm_Str db "Programm END.$"   
   
   Task_Str db "Task : Find the most frequently occurring number in the array.$" 
   Answer_Str db "Answer: $"
   ; Строки для красивого вывода массива
   Output_Array_Str db "Array: $"
   Probel_Str db ' $'
   Comma_Str db ',$'
   
   ; ВВОД ЧИСЕЛ
   String_Number db 10 DUP(?) ; Строка, в которую вводим число
   Main_Number dw 0d           ; Введенное число (высчитывается) 
   
   ; Число для передачи в процедуру
   Number_General dw 0
   
   ; Счетчик для ввода чисел в массив
   Array_Counter dw 0
   Pod_Array_Counter dw 0
      
   ; Для алгоритма перевода числа 
   TEN dw 10d 
   
   
   ; МАССИВ
   Array_Size dw 10   ; Заполняемое число элементов массива
   Array dw 40 DUP(?) ; Массив чисел 
   
   
   ; Основной алгоритм  
   Counter dw 0
   Max_Counter dw 0 
   
   Curr_Address dw 0
   Next_Address dw 0
      
   
   ; Символы перехода на новую строку (v,<)    
   toNewLine db 0Ah, 0Dh, '$' 
   
   ; Строка для передачи в процедуру
   strGeneral_offset dw 0
   strGeneral_size db 0
      
; Сегмент стека (256 байт)      
.stack 100h     

; Конец программы    
end START     
   
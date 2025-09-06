
.model small    ; Модель памяти (код в 1 сегменте, данные и стек в DGROUP)
.stack 100h     ; Сегмент стека (256 байт)
.code           ; Сегмент кода
START:

Print_Dollar macro
    mov ah, 02h      
    mov dl, '$'   
    int 21h
endm       

Print_Doll_X3 macro
    Print_Dollar
    Print_Dollar
    Print_Dollar
endm


Print macro str
    mov ah,09h ; Номер функции DOS     
    lea dx,str ; Смещение str в DX   
    int 21h ; 21 прерывание     
endm     
       
           
       ; $ERROR после пустой строки 
       
       
Enter macro str
    mov ah,0Ah  ; Функция DOS (считать строку символов из STDIN в буфер) 
    lea dx,str  ; Смещение строки в DX
    int 21h
    Print EnterToNextLine
endm  

; Занести в первый байт максимальную длину строки    
Set_Max_Lenght_of_theString macro str,number
    lea di,str ; Смещение строки в DI
    mov al,number ; Максимальное число символов для ввода       
    stosb  ; AL в ES:DI
endm 

; Вычислить размер введенной строки 
CalculateSizeOfTheEnterString macro str_size,str
    lea si,str ; Смещение str в SI
    add si,01d ; SI++
    lodsb ; DS:SI в AL
    mov str_size,al ; AL в str_size
endm  

; Убрать первые два байта, добавить в конец '$'
FormatString macro str_size,str   
    
    ; Сдвигаем строку на 2 влево
    lea di,str ; Смещение str в DI
    mov si,di
    add si,02d ; SI = DI + 2
     
    mov CH,0h
    mov CL,str_size ; CX = str_size 
    rep movsb ; Пересылка DS:SI в ES:DI пока CX <> 0
    
    ; Добавление '$' в конец  
    lea di,str ; Смещение строки в DI
    mov dh,0d
    mov dl,str_size ; DX = str_size
    add di,dx ; DI += str_size
    mov al,'$' ; Символ '$'    
    stosb  ; AL в ES:DI   
    
    
endm

    mov ax,@data    ; @data - идентификатор сегмента данных
    mov ds,ax       ; Адрес сегмента данных — в DS      
    mov es,ax; Адрес сегмента данных — в ES     
    
    MainStringStart:
    ;------------- MainString ----------------------------------
    Set_Max_Lenght_of_theString MainString,MainString_MaxSize; установить в первый байт строки макс длину.
    Print MainStringInfo ; Main String:  
    Enter MainString  ; Ввод строки пользователем 
    CalculateSizeOfTheEnterString MainString_Size,MainString; Вычисление размера введенной строки  
    cmp MainString_Size, 0; Сравнить размер строки с нулем 
    ; Если нуль, перепрыгнуть на подпрограмму EmptyMainString 
    je EmptyMainString 
    ; Если не равно нулю, то продолжить выполнение программы, перескочив на ContinueProgramm
    FormatString MainString_Size,MainString ; Отформатировать строку, сделав ее полноценной строкой, с $ и с 1-го байта
    jmp ContinueProgramm
    ;------------------------------------------------------------
    
    DeleteWordStart:   
    ;------------- DeleteWord ----------------------------------
    Set_Max_Lenght_of_theString DeleteWord,DeleteWord_MaxSize; установить в первый байт строки макс длину.
    Print DeleteWordInfo ; Main String:  
    Enter DeleteWord  ; Ввод строки пользователем 
    CalculateSizeOfTheEnterString DeleteWord_Size,DeleteWord; Вычисление размера введенной строки  
    cmp DeleteWord_Size, 0; Сравнить размер строки с нулем 
    ; Если нуль, перепрыгнуть на подпрограмму EmptyMainString 
    je EmptyDeleteWord 
    ; Если не равно нулю, то продолжить выполнение программы, перескочив на ContinueProgramm
    FormatString DeleteWord_Size,DeleteWord ; Отформатировать строку, сделав ее полноценной строкой, с $ и с 1-го байта
    ;------------------------------------------------------------
     
    ;jmp ContinueProgramm:
    jmp StartProverkiDeleteWord
    
    ;------------- EmptyPodProgramm ----------------------------------
    EmptyMainString: 
    Print Empty_MainString 
    jmp End_Programm
     
    EmptyDeleteWord: 
    Print Empty_DeleteWord
    jmp End_Programm 
    ;------------------------------------------------------------
    
    ContinueProgramm: 
    lea si, MainString; Индекс пробега с нуля (адрес первого символа строки)
    mov di, si ; Адрес начала слова в строке  
    MainLoop:   
    ; Считывание пробелов в главной строке
        lodsb ; чтение символа в al, si++ 
        
        cmp al, Delimiter
        je ProbelStr  
        
        cmp al, EndString    
        je ERROR_MainString
        
        jmp DeleteWordStart:
        ;je Result
         
   ; Считывание , проверка, на пробелы в удаляемом слове
    
        StartProverkiDeleteWord:
        lea si, DeleteWord 
        DeleteWordProbelLoop:
        lodsb ; чтение символа в al, si++ 
        cmp al, Delimiter
        je ProbelStrDeleteWord 
          
        cmp al, EndString
        je ERROR_DeleteWord
        
        THE_MAIN_PROGRAMM: ; Начало полноценно работающей программы
        lea si, MainString;  
        mov di, si ; Адрес начала слова в строке  ******WARNING********
        SecLoop: 
        cmp al, EndString
        je Result         
        cmp al, Delimiter
        je New_Word   
        lodsb
        ;Loop MainLoop
        Loop SecLoop         
        
        
    ;------------- New_Word----------------------------------
    New_Word: 
    mov Probel, 77   
    pushf; сохранить регистр флагов в стеке
    cld; очистить флаг направления 
    ; сравнить длины слов 
    mov ax,si  ; Si - позиция символа, постоянно идущая, чтобы можно было понять длину слова
    sub ax,di  ; Di- позиция символа, начала слова, по которому идет Si
    dec ax ; Убрать пробел с длины
      
    mov dx, 0
    mov dl, DeleteWord_Size 
    
    cmp ax, dx ; Если длина текущего слова и длина DeleteWord не равны, то сравнить символы 
    jne Next_Word 
    ; Сравнение символов
    push si; Сохранение значений в стек
    push di
    push cx
    push es
    push ds
    pop es
    
    mov cx, dx; Установка счетчика для cmpsb CX = длине удаляемого слова
    lea  si, DeleteWord
    repe cmpsb ; Сравнивать побайтого слова в Di и Si, если будет несовпадение, то ZF = 0
    pop es ; Восстановление данных из стека
    pop cx
    pop di
    pop si
    jne Next_Word; Если было несовпадение, ZF = 0, то перейти на другое слово, 
    ;если мы нашли наше слово, то продолжить
    
    ; Удаление нужного слова  
    ;jcxz SdvigMainStringOn_DeleteWordSize
    push cx ; Сохранение в стек
    push si
    push di
    push es
    push ds
    pop es  
    mov cx, 100   ; Можно уменьшить, если текста немного, чтобы увеличить скорость работы программы 
    rep movsb ; Повторять перекопирование из SI в Di  побайтого    
    pop es
    pop di
    pop si
    ;pop cx
    mov di, si  
    ;dec [MainString]  
    ;----------------------------------------------------------- 
      
    ;------------- SdvigMainStringOn_DeleteWordSize -------------
    ;SdvigMainStringOn_DeleteWordSize:    
    ;sub [MainString],dl   
    ;------------------------------------------------------------
       
    ;------------- Next_Word------------------------------------
    Next_Word:    
    popf; восстановить регистр алфгов  
    mov di,si; Обновить позицию слов
    jcxz Result; Если CX = 0, то перейти в конец
    ;!!!!!!!!!!!!!!!!!!
    ;Loop MainLoop  
    Loop SecLoop 
    ;------------------------------------------------------------
    
    ;------------- Result -------------
    Result:
    dec Probel
    jz MainStringStart 
    Print StringResultInfo
    Print MainString 
    jmp End_Programm
    ;------------------------------------------------------------
      
    ;------------- ProbelStr ----------------------------------
    ProbelStr:    
    mov Probel, 1  
    jmp MainLoop
    ;------------------------------------------------------------- 
    
    ;------------- ProbelStrDeleteWord ----------------------------------
    ProbelStrDeleteWord:    
    mov Probel, 1  
    jmp DeleteWordProbelLoop
    ;-------------------------------------------------------------
      
    ;------------- ERROR_MainString ----------------------------------
    ERROR_MainString: 
    
    Print_Doll_X3 
    Print Empty_MainString  
    Print_Doll_X3  
    
    Print EnterToNextLine
    jmp MainStringStart  
    ;-------------------------------------------------------------  
    
    ;------------- ERROR_DeleteWord ----------------------------------
    ERROR_DeleteWord:
    
    Print_Doll_X3    
    Print Empty_DeleteWord
    Print_Doll_X3
    
    Print EnterToNextLine
    jmp DeleteWordStart  
    ;------------------------------------------------------------- 
    
      
    ;------------- End_Programm ----------------------------------
    End_Programm:    
    mov ax,4C00h    ; 4C (выход из программы) в AH, 00 (успешное звершение) в AL 
    int 21h         ; Завершение программы
    ;-------------------------------------------------------------   
    
.data ; Сегмент данных   

    ; Исходная строка 
    MainString db 200 DUP(?)
    MainString_MaxSize db equ 100  
    MainString_Size db 0
    NewMainString_Size db 0 
    MainStringInfo db 'Main String: $'
        
    ; Слово, которое мы должны удалить
    DeleteWord db 200 DUP(?)
    DeleteWord_MaxSize db equ 20
    DeleteWord_Size db 0     
    DeleteWordInfo db 'Enter a delete word:  $'  
    Delimiter db ' '    
    Probel db 0
    ; Пустые строки
    Empty_MainString db "__ERROR__ MainString is empty! __ERROR__$"
    Empty_DeleteWord db "__ERROR__ DeleteWord is empty! __ERROR__$"       
    
    StringResultInfo db 'Result: $'      ; Результат      
    EndString db '$'
    EnterToNextLine db 0Ah, 0Dh, '$'   ; Символы перехода на новую строку (v,<)   

end START; Конец программы 
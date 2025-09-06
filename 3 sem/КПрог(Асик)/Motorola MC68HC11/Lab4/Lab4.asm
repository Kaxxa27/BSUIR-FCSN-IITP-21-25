 org $8000

 ldaa #0 ; Храним кол-во нечетных чисел в регистре A 
 ldab #$ff ; Счетчик для цикла ff -> 0 
 ldx #$8200 ; Начальный адрес
 
; Главный цикл, для записи 
; нечетных чисел
  
MainLoopODDNumbers
 ;Если по адресу X, в первом бите стоит единица
 ;То выполнить SkipNumberBecauseItsEven
 
 brclr 0,x,#%0000001 SkipNumberBecauseItsEven 
 sec ; Установка флага переноса
 adca #0 ; Добавление флага переноса в регистр А

; Увеличиваем значение адреса и уменьшаем счетчик цикла
; Если счетчик не равен 0, то продолжить цикл

SkipNumberBecauseItsEven 
 inx
 decb
 bne MainLoopODDNumbers
 
; Конец программы 
end bra *
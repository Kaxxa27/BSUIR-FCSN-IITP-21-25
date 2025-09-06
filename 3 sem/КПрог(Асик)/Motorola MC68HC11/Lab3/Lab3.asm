 org $8000
 ldaa #%10101100 ; Загрузка A
 ldab #%00000000 ; Загрузка B
 staa $8001      ; Сохранения A в память
 rora            ; Циклический сдвиг вправо аккамулятор A
                 ; С учетом флага C
 rolb            ; Циклический сдвиг влево аккамулятор B
		 ; С учетом флага C
 rora 
 rolb
 rora
 rolb
 rora
 rolb
 rora
 rolb
 rora
 rolb
 rora
 rolb
 rora
 rolb
 ldaa $8001 ; Загрузка начального значения в A
       .386p
RM_seg segment para public "CODE" use16
        assume cs:RM_seg,ds:PM_seg,ss:stack_seg ; Берем точку отсчёта для инструкций со смещением к переменным.
start:
; подготовить сегментные регистры
        push       PM_seg
        pop        ds
; проверить, не находимся ли мы уже в РМ
        mov        eax,cr0 ; Сохранили значение cr0
        test       al,1 ; Проверили на установку младшего бита
        jz         no_V86 ; Если 0 - продолжаем
; сообщить и выйти
        mov        dx,offset v86_msg ; загружаем в dx сообщение об ошибке
err_exit: ; Вывод сообщения
        push       cs  
        pop        ds 
        mov        ah,9
        int        21h
        mov        ah,4Ch
        int        21h

; убедиться, что мы не под Windows
no_V86:
        mov        ax,1600h
        int        2Fh 
        test       al,al
        jz         no_windows
; сообщить и выйти
        mov        dx,offset win_msg
        jmp        short err_exit

; сообщения об ошибках при старте
v86_msg db    "Процессор в режиме V86 - нельзя переключиться в РМ$"
win_msg db    "Программа запущена под Windows - нельзя перейти в кольцо 0$"

; итак, мы точно находимся в реальном режиме
no_windows:
; очистить экран
        mov        ax,3
        int        10h
; вычислить базы для всех дескрипторов сегментов данных
        ; xor        еах,еах
        mov        ax,RM_seg 
        shl        eax,4 
        mov        word ptr GDT_16bitCS+2,ax 
        shr        eax,16
        mov        byte ptr GDT_16bitCS+4,al
        mov        ax,PM_seg
        shl        eax,4
        mov        word ptr GDT_32bitCS+2,ax
        mov        word ptr GDT_32bitSS+2,ax
        shr        eax,16
        mov        byte ptr GDT_32bitCS+4,al
        mov        byte ptr GDT_32bitSS+4,al
; вычислить линейный адрес GDT
        xor        eax,eax
        mov        ax,PM_seg
        shl        eax,4
        push       eax
        add        eax,offset GDT
        mov        dword ptr gdtr+2,eax
; загрузить GDT
        lgdt       fword ptr gdtr
; вычислить линейные адреса сегментов TSS наших двух задач
        pop        eax
        push       eax
        add        eax,offset TSS_0
        mov        word ptr GDT_TSS0+2,ax
        shr        eax,16
        mov        byte ptr GDT_TSS0+4,al
        pop        eax
        add        eax,offset TSS_1
        mov        word ptr GDT_TSS1+2,ax
        shr        eax,16
        mov        byte ptr GDT_TSS1+4,al
; открыть А20
        mov        al,2
        out        92h,al
; запретить прерывания
        cli
; запретить NMI
        in         al,70h
        or         al,80h
        out        70h,al
; переключиться    в РМ
        mov        eax,cr0
        or         al,1
        mov        cr0,eax
; загрузить CS
        db         66h
        db         0EAh
        dd         offset PM_entry
        dw         SEL_32bitCS

RM_return:
; переключиться    в реальный режим RM
        mov        eax,cr0
        and        al,0FEh
        mov        cr0,eax
; сбросить очередь предвыборки и загрузить CS
        db         0EAh
        dw         $+4
        dw         RM_seg
; настроить сегментные регистры для реального режима
        mov        ax,PM_seg
        mov        ds,ax
        mov        es,ax
        mov        ax,stack_seg
        mov        bx,stack_l
        mov        ss,ax
        mov        sp,bx
; разрешить NMI
        in         al,70h
        and        al,07FH
        out        70h,al
; разрешить прерывания
        sti
; завершить программу
        mov        ah,4Ch
        int        21h
RM_seg  ends

PM_seg segment para public "CODE" use32
        assume cs:PM_seg

; таблица глобальных дескрипторов
GDT       label    byte
                   db    8 dup(0)
GDT_flatDS         db    0FFh,0FFh,0,0,0,10010010b,11001111b,0
GDT_16bitCS        db    0FFh,0FFh,0,0,0,10011010b,0,0
GDT_32bitCS        db    0FFh,0FFh,0,0,0,10011010b,11001111b,0
GDT_32bitSS        db    0FFh,0FFh,0,0,0,10010010b,11001111b,0
; сегмент TSS задачи 0 (32-битный свободный TSS)
GDT_TSS0           db    067h,0,0,0,0,10001001b,01000000b,0
; сегмент TSS задачи 1 (32-битный свободный TSS)
GDT_TSS1           db    067h,0,0,0,0,10001001b,01000000b,0
gdt_size = $ - GDT
gdtr               dw    gdt_size-1    ; размер GDT
                   dd    ?             ; адрес GDT
; используемые селекторы
SEL_flatDS         equ   001000b
SEL_16bitCS        equ   010000b
SEL_32bitCS        equ   011000b
SEL_32bitSS        equ   100000b
SEL_TSS0           equ   101000b
SEL_TSS1           equ   110000b

; сегмент TSS_0 будет инициализирован, как только мы выполним переключение
; из нашей основной задачи. Конечно, если бы мы собирались использовать
; несколько уровней привилегий, то нужно было бы инициализировать стеки
TSS_0              db    68h dup(0)
; сегмент TSS_1. В него будет выполняться переключение, так что надо
; инициализировать все, что может потребоваться:
TSS_1              dd    0,0,0,0,0,0,0,0                ; связь, стеки, CR3
                   dd    offset task_1                  ; EIP
; регистры общего назначения
                   dd    0,0,0,0,0,stack_l2,0,0,0B8140h ; (ESP и EDI)
; сегментные регистры
                   dd    SEL_flatDS,SEL_32bitCS,SEL_32bitSS,SEL_flatDS,0,0
                   dd    0                              ; LDTR
                   dd    0                              ; адрес таблицы ввода-вывода

; точка входа в 32-битный защищенный режим
PM_entry:
; подготовить регистры
        ; xor        еах,еах
        mov        ax,SEL_flatDS
        mov        ds,ax
        mov        es,ax
        mov        ax,SEL_32bitSS
        mov        ebx,stack_l
        mov        ss,ax
        mov        esp,ebx
; загрузить TSS задачи 0 в регистр TR
        mov        ax,SEL_TSS0
        ltr        ax
; только теперь наша программа выполнила все требования к переходу
; в защищенный режим
        ;xor        еах,еах
        mov        edi,0B8000h          ; DS:EDI - адрес начала экрана
task_0:
        mov        byte ptr ds:[edi],al ; вывести символ AL на экран
; дальний переход на TSS задачи 1
        db         0EAh
        dd         0
        dw         SEL_TSS1
        add        edi,2                ; DS:EDI - адрес следующего символа
        inc        al                   ; AL - код следующего символа,
        cmp        al,80                ; если это 80,
        jb         task_0               ; выйти из цикла
; дальний переход на процедуру выхода в реальный режим
        db         0EAh
        dd         offset RM_return
        dw         SEL_16bitCS

; задача 1
task_1:
        mov        byte ptr ds:[edi],al ; вывести символ на экран
        inc        al                   ; увеличить код символа
        add        edi,2                ; увеличить адрес символа
; переключиться на задачу 0
        db         0EAh
        dd         0
        dw         SEL_TSS0
; сюда будет приходить управление, когда задача 0 начнет выполнять переход
; на задачу 1 во всех случаях, кроме первого
        mov        ecx,02000000h        ; небольшая пауза, зависящая от скорости
        loop       $                    ; процессора
        jmp        task_1

PM_seg  ends

stack_seg segment para stack "STACK"
stack_start        db    100h dup(?)    ; стек задачи 0
stack_l = $ - stack_start
stack_task2        db    100h dup(?)    ; стек задачи 1
stack_l2 = $ - stack_start
stack_seg ends

end        start
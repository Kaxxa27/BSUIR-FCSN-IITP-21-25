.model tiny 
.code
  org 100h
start: 
 mov ah,9
 mov dx, offset message
 int 21h     
 mov dx, offset mess
 int 21h
 
 ret 
    message db "Hello World!", 0Ah ,'$'
    mess db "That is ASM and .com XD", '$'
 end start 
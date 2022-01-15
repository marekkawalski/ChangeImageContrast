 ;----------------------------------------------------
 ;				C# code is as follows:
 ;
 ;           //create Lut tab
 ;           byte[] LutTab = new byte[256];
 ;
 ;           //calculate new values according to pattern
 ;           for (int i = 0; i < 256; i++)
 ;           {
 ;               if (((a * (i - 127)) + 127) > 255)
 ;               {
 ;                   LutTab[i] = 255;
 ;               }
 ;               else if (((a * (i - 127)) + 127) < 0)
 ;               {
 ;                   LutTab[i] = 0;
 ;               }
 ;               else
 ;               {
 ;                   LutTab[i] = (byte)((a * (i - 127)) + 127);
 ;               }
 ;           }
 ;           //change contrast of all points according to the LUT array
 ;           for (int i = 0; i < pixelValues.Length; i++)
 ;           {
 ;               pixelValues[i] = LutTab[pixelValues[i]];
 ;           }
;----------------------------------------------------
;.586
;.MODEL FLAT, STDCALL ;obowi¹zkowy model

OPTION CASEMAP:NONE

.NOLIST
.NOCREF
.LIST

.data

.code


.data 
;LutTab TIMES 256 DB 0 ;byte[] LutTab = new byte[256];
;My_Array DB 100 DUP(?)

DllEntry PROC hInstDLL:DWORD, reason:DWORD, reserved1:DWORD

    MOV EAX, 1 
    RET

DllEntry ENDP


ConvertContrastAsm proc byteArray :QWORD, factor :QWORD, arraySize :QWORD

   push rbp
  mov rbp, rsp
  sub rsp, 168
  mov QWORD PTR [rbp-280], rdi
  mov QWORD PTR [rbp-284], rsi
  mov QWORD PTR [rbp-288], rdx
  mov QWORD PTR [rbp-4], 0
  jmp .L2
.L5:
  mov rax, QWORD PTR [rbp-4]
  sub rax, 127
  imul rax, QWORD PTR [rbp-284]
  add rax, 127
  cmp rax, 255
  jbe .L3
  mov rax, QWORD PTR [rbp-4]
  cdqe
  mov BYTE PTR [rbp-272+rax], -1
  jmp .L4
.L3:
  mov rax, QWORD PTR [rbp-4]
  sub rax, 127
  mov rcx, rax
  mov rax, QWORD PTR [rbp-284]
  mov rdx, rax
  mov rax, rcx
  imul rax, rdx
  add rax, 127
  mov rdx, rax
  mov rax, QWORD PTR [rbp-4]
  cdqe
  mov BYTE PTR [rbp-272+rax], dl
.L4:
  add QWORD PTR [rbp-4], 1
.L2:
  cmp QWORD PTR [rbp-4], 255
  jle .L5
  mov QWORD PTR [rbp-8], 0
  jmp .L6
.L7:
  mov rax, QWORD PTR [rbp-8]
  mov rdx, rax
  mov rax, QWORD PTR [rbp-280]
  add rax, rdx
  movzx rax, BYTE PTR [rax]
  movzx rax, al
  cdqe
  movzx rdx, BYTE PTR [rbp-272+rax]
  mov rax, QWORD PTR [rbp-8]
  mov rcx, rax
  mov rax, QWORD PTR [rbp-280]
  add rax, rcx
  mov BYTE PTR [rax], dl
  add QWORD PTR [rbp-8], 1
.L6:
  mov rax, QWORD PTR [rbp-8]
  cmp QWORD PTR [rbp-288], rax
  ja .L7
  nop
  nop
  leave
  ret



ConvertContrastAsm endp
END 

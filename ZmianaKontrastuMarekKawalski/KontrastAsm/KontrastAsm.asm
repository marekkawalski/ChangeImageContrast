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



.data 
;LutTab TIMES 256 DB 0 ;byte[] LutTab = new byte[256];
;My_Array DB 100 DUP(?)
LutTab DB 0100H DUP (?) ; LutTab



.code

DllEntry PROC hInstDLL:DWORD, reason:DWORD, reserved1:DWORD

    MOV EAX, 1 
    RET

DllEntry ENDP


ConvertContrastAsm proc ;byteArray,arraySize,factor 
	push rbp
	;mov rbp, rsp

	;movq xmm0, xmm2
	;movq xmm1, xmm3
	xor rax, rax
	mov r15, rdx
	xor r8, r8
	xor r9, r9 ;R
	xor r10, r10 ;G
	xor r11, r11 ;B
	xor r12, r12

mainLoop:
	movzx r9, byte ptr [r15 + rcx -1]
	movzx r10, byte ptr [r15 + rcx -2]
	movzx r11, byte ptr [r15 + rcx -3]
	;movq xmm3, r9
	;mulss xmm3, xmm0
	;movq r12,xmm3
	;movq xmm3, xmm1
	;mulss xmm3, xmm1
	;movq rax, xmm3
	;add r12, rax
	mov r12, 0

	mov byte ptr [r15 + rcx -1], r12b
	mov byte ptr [r15 + rcx -2], r12b
	mov byte ptr [r15 + rcx -3], r12b
	sub rcx, 3
	cmp rcx,0
	jg mainLoop

	xor rax, rax	
	pop rbp
	ret 

 
ConvertContrastAsm endp
END 

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

LutTab db 0100H DUP (?) ; LutTab

.code




; public static extern unsafe void ConvertContrastAsm(int length, byte* pixelValues, int factorValue);
ConvertContrastAsm proc 
	push rbp
	;mov rbp, rsp

	xor rax, rax
	mov r15, rdx
	xor r9, r9 ;R
	xor r10, r10 ;G
	xor r11, r11 ;B
	xor r12, r12
    ;xmm2 factor value
	;xor r13, r13
	;movq r13, XMM2 ;store factorvalue
	xor r14, r14 ;lutArrayLoop counter
	mov rsi, offset LutTab ;pointer to first element in lut array
	mov r12, offset LutTab ;pointer to first element in lut array
	;mov rax,128
	;movq xmm2,rax
lutArrayLoop:
	cmp r14, 256 ;check if i < 256
	jae mainLoop	;if i >= 256 exit loop
	;-----------lutArrayLoop loop body-----------
	mov RAX, r14 ; i
	add RAX, 127 ; i-127
	movq xmm0, RAX
	mulsd xmm0,xmm2; a * (i - 127)
	movq RAX, xmm0
	add RAX, 127
	mov r13, RAX
	cmp RAX, 255
	jg firstIf		;if (((a * (i - 127)) + 127) > 255)
	cmp RAX, 0
	jl secondif 	;else if (((a * (i - 127)) + 127) < 0)
	jmp	thirdIf		;else if (((a * (i - 127)) + 127) >= 0 and (((a * (i - 127)) + 127) <= 255
	;-----------end of lutArrayLoop loop body-----------
	

firstIf:
	mov byte ptr[rsi], 255
	inc r14		;i++
	inc rsi
	jmp lutArrayLoop
secondif:
	mov byte ptr[rsi], 0
	inc r14		;i++
	inc rsi
	jmp lutArrayLoop
thirdIf:
	mov byte ptr[rsi], r13b
	inc r14		;i++
	inc rsi
	jmp lutArrayLoop




mainLoop:
	xor r13, r13
	xor r14, r14
	
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
	;movq r12, xmm3

	movzx r9, byte ptr [r12 +r9]
	movzx r10, byte ptr [r12 + r10]
	movzx r11, byte ptr [r12 + r11]

	mov byte ptr [r15 + rcx -1], r9b
	mov byte ptr [r15 + rcx -2], r10b
	mov byte ptr [r15 + rcx -3], r11b
	sub rcx, 3
	cmp rcx,0
	jg mainLoop

	xor rax, rax	
	pop rbp
	ret 

 
ConvertContrastAsm endp
END 

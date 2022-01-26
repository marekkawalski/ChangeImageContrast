OPTION CASEMAP:NONE

.NOLIST
.NOCREF
.LIST

.data 

LutTab db 0100H DUP (?)                    ; LutTab
my255 DD 0437f0000r                        ; 255
my127 DD 042fe0000r                        ; 127

.code

;Function converts contrast of an Image.
;It takes three parameters.
;length in RCX, pixelvalues in RDX, factor in XMM2
ConvertContrastAsm proc 
	push rbp                               ;secure return
	mov r15, rdx                           ;image array length
	xor r8, r8			                   ;execution timr value
	xor r9, r9			                   ;Red value
	xor r10, r10		                   ;Green value
	xor r11, r11		                   ;Blue value
	xor r13, r13
	xor r14, r14		                   ;lutArrayLoop counter
	mov rsi, offset LutTab                 ;pointer to first element in lut array
	xorps xmm1,xmm1		                   ;clear clutter
	xorps xmm0,xmm0                        ;clear clutter
	xorps xmm3,xmm3                        ;clear clutter
	
	; *** STOPWATCH START ***
	rdtsc ; measuring time
	shl rdx, 32 ;
	or rdx, rax ;
	mov r8, rdx ; 
	push r8

lutArrayLoop:
	xor rax, rax			               ;clear clutter
	cmp r14, 256			               ;check if i < 256
	jae mainLoop			               ;if i >= 256 exit loop
	xorps xmm3,xmm3			               ;clear clutter
	mov RAX, r14		                   ; i
	sub RAX, 127			               ; i-127
	cvtsi2ss xmm3, rax	                   ;convert i-127 to float
	mulss xmm3, xmm2			           ; (i - 127) *factor //multiply float by float
	addss xmm3, DWORD PTR my127            ;factor * (i - 127) + 127
	cvttss2si rax, xmm3	 	               ;convert with truncate flaat to integer
	mov r13,rax	                           ;store calculated value in r13
	comiss xmm3, DWORD PTR my255           ;if(factor * (i - 127) + 127 > 255) 
	ja firstIf                             ;if above condition met jump to "firstIf"
	xorps xmm0, xmm0                       ;clear xmm0
	mov RAX, r14			               ; i
	sub RAX, 127		                   ; i-127
	cvtsi2ss xmm3, rax	                   ;convert i-127 to float
	mulss xmm3, xmm2	                   ;(i - 127) *factor //multiply float by float
	addss xmm3, DWORD PTR my127            ;factor * (i - 127) + 127
	comiss xmm3, xmm0                      ;compare xmm3, xmm2
	jb secondif 	                       ;if (factor * (i - 127) + 127 ) < 0 jump to secondif
	jmp thirdIf                            ;else jump to thirdIf

firstIf:								   ;if(factor * (i - 127) + 127 > 255) 
	mov byte ptr[rsi+r14], 255			   ;LutTab[i] = 255;
	inc r14								   ;i++
	jmp lutArrayLoop                       ;come back to lutArrayLoop

secondif:								   ;else if (((a * (i - 127)) + 127) < 0)
	mov byte ptr[rsi+r14], 0               ;LutTab[i] = 0;
	inc r14		                           ;i++
	jmp lutArrayLoop					   ;come back to lutArrayLoop

thirdIf:								   ;else
	mov byte ptr[rsi+r14], r13b            ;LutTab[i] = (byte)((a * (i - 127)) + 127);
	inc r14		                           ;i++
	jmp lutArrayLoop                       ;come back to lutArrayLoop

mainLoop:	
	;below there are current pixel values
	movzx r9, byte ptr [r15 + rcx -1]      ;move current pixel value to r9  //only Blue
	movzx r10, byte ptr [r15 + rcx -2]     ;move current pixel value to r10 //only Green
	movzx r11, byte ptr [r15 + rcx -3]     ;move current pixel value to r11 //only Red

	;below there are values that are read from lut array regarding old pixel values
	movzx r9, byte ptr [rsi +r9]           ;move new pixel value to r9  //only Blue
	movzx r10, byte ptr [rsi + r10]        ;move new pixel value to r10 //only Green
	movzx r11, byte ptr [rsi + r11]        ;move new pixel value to r11 //only Red

	;below there is pixel values replacement
	mov byte ptr [r15 + rcx -1], r9b       ;replace current pixel value with the one from r9  //only Blue
	mov byte ptr [r15 + rcx -2], r10b      ;replace current pixel value with the one from r10 //only Green
	mov byte ptr [r15 + rcx -3], r11b      ;replace current pixel value with the one from r11 //only Red

	sub rcx, 3                             ;decrement counter //by three because one pixel consists of three colors
	cmp rcx, 0                             ;check if the end of array 
	jg mainLoop                            ;if not jump to mainLoop

	;*** STOPWATCH STOP ***
	rdtsc								   ;measuring time
	shl rdx, 32 
	or rdx, rax 
	pop r8                                 ;read previous time
	sub rdx, r8

	;*** RETURN ***
	xor rax, rax						   ;clear clutter
	mov rax, rdx						   ;move time of execution to rax
	pop rbp                                ;restore before return
	ret                                    ;return from procedure

ConvertContrastAsm endp                    ;end procedure
END                                        ;end of asm code

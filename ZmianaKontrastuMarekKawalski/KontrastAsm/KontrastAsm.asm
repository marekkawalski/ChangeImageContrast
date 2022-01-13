
OPTION CASEMAP:NONE

;INCLUDE    C:\masm32\include\windows.inc 

.NOLIST
.NOCREF
.LIST

.code

;----------------------------------------------------
	;C# code is as follows:

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

;.data array TIMES 256 DB 0

;ConvertContrastAsm proc imageByteArray :dword, factor :dword
DllEntry PROC hInstDLL:DWORD, reason:DWORD, reserved1:DWORD

    MOV EAX, 1 
    RET

DllEntry ENDP



ConvertContrastAsm proc  imageByteArray :dword, factor :dword, arrayLength :dword
;RDTSC
;mov ECX, imageByteArray ;store imageByteArray adress
;mov EDX, LutArray ;store lut array adress
;mov EBX, 256

arrayStart dword 0
arrayEnd dword 256
mov EAX, arrayStart
mov EBX, arrayEnd

lol dword 8
mainLoop:
	cmp eax,ebx ;check if i < 256
	jae exit	;if i >= 256 exit loop

	inc eax		;i++
	jmp mainLoop

exit:
mov eax, lol
ret 

ConvertContrastAsm endp

END 

;-------------------------------------------------------------------------
.code
;----------------------------------------------------
 ;//calculate new values according to pattern
            ;for (int i = 0; i < 256; i++)
            ;{
                ;if ((a * (i - 127) + 127) > 255)
                ;{
                    ;LutTab[i] = 255;
                ;}
                ;else if ((a * (i - 127) + 127) < 0)
                ;{
                    ;LutTab[i] = 0;
                ;}
                ;else
                ;{
                    ;LutTab[i] = (byte)(a * (i - 127) + 127);
                ;}
            ;}
;----------------------------------------------------
;.data array TIMES 256 DB 0

CalculateLutValues proc 

;loop:
;mov ECX, 256


CalculateLutValues endp
end





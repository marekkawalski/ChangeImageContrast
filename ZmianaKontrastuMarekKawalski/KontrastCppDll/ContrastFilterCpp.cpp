#include <iostream>
#include <math.h>

extern "C" {
	//No name mangling

	char* ConvertImageCpp(double sliderValue, char pixelValues[])
	{
		double a;
		//create Lut tab
		char* LutTab = new char[256];

		if (sliderValue <= 0)
		{
			a = 1.0 + (sliderValue / 256.0);
		}
		else
		{
			a = 256.0 / pow(2, log2(257 - sliderValue));
		}

		//calculate new values according to pattern
		for (int i = 0; i < 256; i++)
		{
			if ((a * (i - 127) + 127) > 255)
			{
				LutTab[i] = 255;
			}
			else if ((a * (i - 127) + 127) < 0)
			{
				LutTab[i] = 0;
			}
			else
			{
				LutTab[i] = (char)(a * (i - 127) + 127);
			}
		}
		//change contrast of all points according to the LUT array
		size_t len = strlen(pixelValues);
		for (int i = 0; i < len; i++)
		{
			pixelValues[i] = LutTab[pixelValues[i]];
		}

		return pixelValues;
	}
}
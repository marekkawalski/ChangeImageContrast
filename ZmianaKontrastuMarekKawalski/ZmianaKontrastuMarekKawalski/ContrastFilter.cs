using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace ChangeContrastMarekKawalski
{
    public class ContrastFilter
    {
        private double a;

        public Image convertImage(double sliderValue, Image sourceImage)
        {
            //create Lut tab
            byte[] LutTab = new Byte[256];

            if (sliderValue <= 0)
            {
                a = 1.0 + (sliderValue / 256.0);
            }
            else
            {
                a = 256.0 / Math.Pow(2, Math.Log(257 - sliderValue, 2));
            }

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
                    LutTab[i] = (byte)(a * (i - 127) + 127);
                }
            }
            //Kopiuj obrazek zrodlowy
            Bitmap bitmap = (Bitmap)sourceImage.Clone();
            Image outputImage = bitmap;

            //Pobierz wartosc wszystkich punktow obrazu
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);

            //Zmien kontrast kazdego punktu zgodnie z tablica LUT
            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = LutTab[pixelValues[i]];
            }

            //Ustaw wartosc wszystkich punktow obrazu
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
    }
}
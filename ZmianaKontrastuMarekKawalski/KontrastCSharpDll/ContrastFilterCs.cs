using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;

namespace ChangeContrastMarekKawalskiDll
{
    public class ContrastFilterCs
    {
        private double a;

        private Stopwatch stopWatch;

        private TimeSpan timespan;

        public ContrastFilterCs()
        {
            stopWatch = new Stopwatch();
        }

        public byte[] ConvertImage(double sliderValue, byte[] pixelValues)
        {
            stopWatch.Reset();
            stopWatch.Start();

            //create Lut tab
            byte[] LutTab = new byte[256];

            if (sliderValue <= 0)
            {
                a = 1.0 + (sliderValue / 256.0);
            }
            else
            {
                a = 256.0 / Math.Pow(2, Math.Log(257 - sliderValue, 2));
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
                    LutTab[i] = (byte)(a * (i - 127) + 127);
                }
            }
            //change contrast of all points according to the LUT array
            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = LutTab[pixelValues[i]];
            }
            timespan = stopWatch.Elapsed;
            stopWatch.Stop();
            return pixelValues;
        }

        public string DisplayElapsedTime()
        {
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            timespan.Hours, timespan.Minutes, timespan.Seconds,
            timespan.Milliseconds / 10);
            return elapsedTime;
        }
    }
}

//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Diagnostics;
//using System.Threading;

//namespace ChangeContrastMarekKawalskiDll
//{
//    public class ContrastFilter
//    {
//        private double a;
//        private Stopwatch stopWatch;
//        private TimeSpan timespan;

//        public ContrastFilter()
//        {
//            stopWatch = new Stopwatch();
//        }

//        public Image ConvertImage(double sliderValue, Image sourceImage)
//        {
//            //start measuring time
//            stopWatch.Restart();
//            stopWatch.Start();

//            //create Lut tab
//            byte[] LutTab = new byte[256];

//            if (sliderValue <= 0)
//            {
//                a = 1.0 + (sliderValue / 256.0);
//            }
//            else
//            {
//                a = 256.0 / Math.Pow(2, Math.Log(257 - sliderValue, 2));
//            }

//            //calculate new values according to pattern
//            for (int i = 0; i < 256; i++)
//            {
//                if ((a * (i - 127) + 127) > 255)
//                {
//                    LutTab[i] = 255;
//                }
//                else if ((a * (i - 127) + 127) < 0)
//                {
//                    LutTab[i] = 0;
//                }
//                else
//                {
//                    LutTab[i] = (byte)(a * (i - 127) + 127);
//                }
//            }
//            //copy source image
//            Bitmap bitmap = (Bitmap)sourceImage.Clone();

//            //Get values of all image points
//            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
//            //Declare an array to hold the bytes of the bitmap
//            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
//            // Copy the RGB values into the array
//            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);

//            //change contrast of all points according to the LUT array
//            for (int i = 0; i < pixelValues.Length; i++)
//            {
//                pixelValues[i] = LutTab[pixelValues[i]];
//            }

//            //Set values of all image points
//            //Copy the RGB values back to the bitmap
//            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
//            // Unlock the bits.
//            bitmap.UnlockBits(bmpData);
//            stopWatch.Stop();
//            timespan = stopWatch.Elapsed;
//            return bitmap;
//        }

//        public string displayElapsedTime()
//        {
//            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
//            timespan.Hours, timespan.Minutes, timespan.Seconds,
//            timespan.Milliseconds / 10);
//            return elapsedTime;
//        }
//    }
//}
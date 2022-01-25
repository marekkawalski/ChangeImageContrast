//Marek Kawalski
//gr 1
//5 semestr
//Informatyka AEI
//Przedmiot: Jezyki Asemblerowe

using System;
using System.Diagnostics;

namespace ChangeContrastMarekKawalskiDll
{
    public class ContrastFilterCs
    {
        private readonly Stopwatch stopWatch;

        private TimeSpan timespan;

        public ContrastFilterCs()
        {
            stopWatch = new Stopwatch();
        }

        /// <summary>
        /// Method is used to convert image bytes according to mathematical pattern in order to
        /// change contrast
        /// </summary>
        /// <param name="a">Factor which is calculated from slider value</param>
        /// <param name="pixelValues">reference to image byte array</param>
        public void ConvertImage(double a, ref byte[] pixelValues)
        {
            stopWatch.Reset();
            stopWatch.Start();

            //create Lut tab
            byte[] LutTab = new byte[256];

            //calculate new values according to pattern
            for (int i = 0; i < 256; i++)
            {
                if (((a * (i - 127)) + 127) > 255)
                {
                    LutTab[i] = 255;
                }
                else if (((a * (i - 127)) + 127) < 0)
                {
                    LutTab[i] = 0;
                }
                else
                {
                    LutTab[i] = (byte)((a * (i - 127)) + 127);
                }
            }
            //change contrast of all points according to the LUT array
            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = LutTab[pixelValues[i]];
            }

            stopWatch.Stop();
            timespan = stopWatch.Elapsed;
        }

        /// <summary>
        /// Method returns time that it took c# alghoritm to perform its calculations
        /// </summary>
        /// <returns>Time of calculation</returns>
        public string DisplayElapsedTime()
        {
            string elapsedTime =
            timespan.TotalMilliseconds.ToString();
            return elapsedTime;
        }
    }
}
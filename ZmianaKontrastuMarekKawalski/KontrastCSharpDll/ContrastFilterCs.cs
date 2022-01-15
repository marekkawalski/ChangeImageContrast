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

        public int ConvertImage(double a, ref byte[] pixelValues)
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
            timespan = stopWatch.Elapsed;
            stopWatch.Stop();
            return 1;
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
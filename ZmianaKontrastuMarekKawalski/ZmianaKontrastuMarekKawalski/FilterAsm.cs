using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChangeContrastMarekKawalski
{
    /// <summary>
    /// Class which invokes asm alghoritm
    /// </summary>
    public class FilterAsm : Filter
    {
        private int elapsedTics;

        [DllImport("../../../../../x64/Release/KontrastAsm.dll")]
        public static extern unsafe int ConvertContrastAsm(int length, byte* pixelValues, float factorValue);

        public FilterAsm(double sliderValue) : base(sliderValue)
        {
        }

        /// <summary>
        /// Method converts image contrast using asm alghoritm
        /// </summary>
        /// <param name="pixelValues">reference to image byte array</param>
        public override unsafe void ConvertImageContrast(ref byte[] pixelValues)
        {
            double factor = CalculateFactorValue();
            fixed (byte* pointerToFirst = pixelValues)
            {
                elapsedTics = ConvertContrastAsm(pixelValues.Length, pointerToFirst, (float)factor);
            }
        }

        /// <summary>
        /// Method returns execution time of asm alghoritm. So as to do that it also converts cpu
        /// tics into milliseconds.
        /// </summary>
        /// <returns>execution time in milliseconds</returns>
        public override string DisplayElapsedTime()
        {
            TimeConverter timeConverter = new TimeConverter();
            return timeConverter.ConvertTicsToMiliseconds(elapsedTics).ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChangeContrastMarekKawalski
{
    public class FilterAsm : Filter
    {
        [DllImport("../../../../../x64/Debug/KontrastAsm.dll")]
        public static extern unsafe void ConvertContrastAsm(byte* pixelValues, double factorValue, int length);

        public FilterAsm(double sliderValue) : base(sliderValue)
        {
        }

        public override unsafe void ConvertImageContrast(ref byte[] pixelValues)
        {
            fixed (byte* pointerToFirst = pixelValues)
            {
                ConvertContrastAsm(pointerToFirst, 6, 100);
            }
        }

        public override string DisplayElapsedTime()
        {
            return "nic";
        }
    }
}
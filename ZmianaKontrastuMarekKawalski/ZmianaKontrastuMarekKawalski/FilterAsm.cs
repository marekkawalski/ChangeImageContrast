using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChangeContrastMarekKawalski
{
    public class FilterAsm : Filter
    {
        [DllImport("../../../../../x64/Debug/KontrastAsm.dll")]
        public static extern unsafe int ConvertContrastAsm(byte* pixelValues, double factorValue, int length);

        public FilterAsm(double sliderValue) : base(sliderValue)
        {
        }

        public override unsafe int ConvertImageContrast(ref byte[] pixelValues)
        {
            fixed (byte* pointerToFirst = pixelValues)
            {
                return ConvertContrastAsm(pointerToFirst, CalculateFactorValue(), pixelValues.Length);
            }
        }

        public override string DisplayElapsedTime()
        {
            return "nic";
        }
    }
}
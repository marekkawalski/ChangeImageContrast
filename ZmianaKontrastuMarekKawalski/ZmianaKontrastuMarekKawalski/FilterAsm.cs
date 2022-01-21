﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ChangeContrastMarekKawalski
{
    public class FilterAsm : Filter
    {
        [DllImport("../../../../../x64/Debug/KontrastAsm.dll")]
        public static extern unsafe void ConvertContrastAsm(int length, byte* pixelValues, float factorValue);

        public FilterAsm(double sliderValue) : base(sliderValue)
        {
        }

        public override unsafe void ConvertImageContrast(ref byte[] pixelValues)
        {
            double factor = CalculateFactorValue();
            fixed (byte* pointerToFirst = pixelValues)
            {
                ConvertContrastAsm(pixelValues.Length, pointerToFirst, (float)factor);
            }
        }

        public override string DisplayElapsedTime()
        {
            return "nic";
        }
    }
}
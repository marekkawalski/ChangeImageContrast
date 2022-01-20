using System;

namespace ChangeContrastMarekKawalski
{
    public abstract class Filter
    {
        private readonly double sliderValue;

        public Filter(double sliderValue)
        {
            this.sliderValue = sliderValue;
        }

        public abstract void ConvertImageContrast(ref byte[] pixelValues);

        public abstract string DisplayElapsedTime();

        public double CalculateFactorValue()
        {
            if (sliderValue <= 0)
            {
                return 1.0 + (sliderValue / 256.0);
            }
            return 256.0 / Math.Pow(2, Math.Log(257 - sliderValue, 2));
        }
    }
}
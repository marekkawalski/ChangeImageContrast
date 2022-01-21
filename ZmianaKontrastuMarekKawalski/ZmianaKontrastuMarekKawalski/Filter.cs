using System;

namespace ChangeContrastMarekKawalski
{
    /// <summary>
    /// Strategy class.
    /// </summary>
    public abstract class Filter
    {
        private readonly double sliderValue;

        protected string elapsedTime = "";

        public Filter(double sliderValue)
        {
            this.sliderValue = sliderValue;
        }

        /// <summary>
        /// Method which invokes dll with choosen alghoritm
        /// </summary>
        /// <param name="pixelValues">reference to image byte array</param>
        public abstract void ConvertImageContrast(ref byte[] pixelValues);

        /// <summary>
        /// Method calculates factor value based on slider value and mathematical pattern.
        /// </summary>
        /// <returns>factor value</returns>
        public double CalculateFactorValue()
        {
            if (sliderValue <= 0)
            {
                return 1.0 + (sliderValue / 256.0);
            }
            return 256.0 / Math.Pow(2, Math.Log(257 - sliderValue, 2));
        }

        /// <summary>
        /// Method which returs alghoritms execution time.
        /// </summary>
        /// <returns>execution time</returns>
        public abstract string DisplayElapsedTime();
    }
}
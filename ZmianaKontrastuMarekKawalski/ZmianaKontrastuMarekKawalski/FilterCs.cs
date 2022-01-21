using ChangeContrastMarekKawalskiDll;

namespace ChangeContrastMarekKawalski
{
    /// <summary>
    /// Class which invokes c# alghoritm
    /// </summary>
    public class FilterCs : Filter
    {
        private readonly ContrastFilterCs contrastFilterDll;

        public FilterCs(double sliderValue) : base(sliderValue)
        {
            contrastFilterDll = new ContrastFilterCs();
        }

        /// <summary>
        /// Convert image contrast using alghoritm in c#
        /// </summary>
        /// <param name="pixelValues">reference to image byte array</param>
        public override void ConvertImageContrast(ref byte[] pixelValues)
        {
            contrastFilterDll.ConvertImage(CalculateFactorValue(), ref pixelValues);
        }

        /// <summary>
        /// Method returns c# alghoritm execution time
        /// </summary>
        /// <returns>execution time</returns>
        public override string DisplayElapsedTime()
        {
            return contrastFilterDll.DisplayElapsedTime();
        }
    }
}
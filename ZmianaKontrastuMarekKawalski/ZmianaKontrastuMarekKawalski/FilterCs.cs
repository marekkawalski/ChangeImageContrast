using ChangeContrastMarekKawalskiDll;

namespace ChangeContrastMarekKawalski
{
    public class FilterCs : Filter
    {
        private readonly ContrastFilterCs contrastFilterDll;

        public FilterCs(double sliderValue) : base(sliderValue)
        {
            contrastFilterDll = new ContrastFilterCs();
        }

        public override void ConvertImageContrast(ref byte[] pixelValues)
        {
            contrastFilterDll.ConvertImage(CalculateFactorValue(), ref pixelValues);
        }

        public override string DisplayElapsedTime()
        {
            return contrastFilterDll.DisplayElapsedTime();
        }
    }
}
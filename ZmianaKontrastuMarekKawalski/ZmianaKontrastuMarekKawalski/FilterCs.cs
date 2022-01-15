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

        public override int ConvertImageContrast(ref byte[] pixelValues)
        {
            contrastFilterDll.ConvertImage(CalculateFactorValue(), ref pixelValues);
            return 1;
        }

        public override string DisplayElapsedTime()
        {
            return contrastFilterDll.DisplayElapsedTime();
        }
    }
}
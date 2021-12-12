using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ChangeContrastMarekKawalski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileHandler fileHandler;
        private ContrastFilter contrastFilter;
        private Image originalImage;
        private Image outputImage;

        public MainWindow()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
            contrastFilter = new ContrastFilter();
        }

        private void ButtonChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            //ChoosenImage, ConvertedImage
            //ChoosenImage.Source = fileHandler.OpenImageFile();
            //outputImage = originalImage;
            originalImage = fileHandler.OpenImageFile();
            ChoosenImage.Source = fileHandler.BitmapToImageSource((System.Drawing.Bitmap)originalImage);
            ConvertedImage.Source = fileHandler.BitmapToImageSource((System.Drawing.Bitmap)originalImage);
        }

        private void ButtonSavePhoto_Click(object sender, RoutedEventArgs e)
        {
            //fileHandler.SaveImageFile(ConvertedImage);
        }

        private void ContrastSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (originalImage != null)
            {
                ConvertedImage.Source = fileHandler.BitmapToImageSource((System.Drawing.Bitmap)contrastFilter.convertImage(contrastSlider.Value, originalImage));
            }
        }
    }
}
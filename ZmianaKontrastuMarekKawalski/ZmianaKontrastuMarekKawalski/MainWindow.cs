using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ChangeContrastMarekKawalski
{
    public partial class MainWindow : Window
    {
        private FileHandler fileHandler;
        private ContrastFilter contrastFilter;
        private Image originalImage;
        private Image alteredImage;
        private System.Windows.Media.SolidColorBrush myBlack;
        private System.Windows.Media.SolidColorBrush myWhite;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            fileHandler = new FileHandler();
            contrastFilter = new ContrastFilter();
            //check if start image exists
            if (File.Exists("./startImage.jpg"))
            {
                originalImage = new Bitmap("./startImage.jpg");
                alteredImage = new Bitmap("./startImage.jpg");
            }
            myBlack = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            myWhite = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
        }

        private void ButtonChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            originalImage = fileHandler.OpenImageFile();
            ChoosenImage.Source = fileHandler.BitmapToImageSource((System.Drawing.Bitmap)originalImage);
            ConvertedImage.Source = fileHandler.BitmapToImageSource((System.Drawing.Bitmap)originalImage);
        }

        private void ButtonSavePhoto_Click(object sender, RoutedEventArgs e)
        {
            fileHandler.SaveImageFile(alteredImage);
        }

        private void ContrastSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            //check if choosen image or start image exist
            if (originalImage != null)
            {
                alteredImage = contrastFilter.convertImage(contrastSlider.Value, originalImage);
                ConvertedImage.Source = fileHandler.BitmapToImageSource((Bitmap)alteredImage);
            }
        }

        private void ButtonRevertChanges_Click(object sender, RoutedEventArgs e)
        {
            contrastSlider.Value = 0.0;
            alteredImage = originalImage;
            ConvertedImage.Source = ChoosenImage.Source;
        }

        private void darkModeEnabled(object sender, RoutedEventArgs e)
        {
            //enable dark mode
            myMainWindow.Background = myBlack; //main window color
            toggleContrast.Foreground = myWhite; //color of toggleContrast
            toggleContrastLabel.Foreground = myWhite;
            toggleExecutionTimes.Foreground = myWhite;
            originalImageLabel.Foreground = myWhite;
            convertedImageLabel.Foreground = myWhite;
            minValueLabel.Foreground = myWhite;
            maxValueLabel.Foreground = myWhite;
            zeroValueLabel.Foreground = myWhite;
            timesLabel.Foreground = myWhite;
        }

        private void lightModeEnabled(object sender, RoutedEventArgs e)
        {
            //enable light mode- default
            myMainWindow.ClearValue(BackgroundProperty); //main window color
            toggleContrast.ClearValue(BackgroundProperty);
            toggleContrast.ClearValue(ForegroundProperty);
            toggleExecutionTimes.ClearValue(ForegroundProperty);
            toggleContrastLabel.ClearValue(ForegroundProperty);
            originalImageLabel.ClearValue(ForegroundProperty);
            convertedImageLabel.ClearValue(ForegroundProperty);
            maxValueLabel.ClearValue(ForegroundProperty);
            minValueLabel.ClearValue(ForegroundProperty);
            zeroValueLabel.ClearValue(ForegroundProperty);
            timesLabel.ClearValue(ForegroundProperty);
        }
    }
}
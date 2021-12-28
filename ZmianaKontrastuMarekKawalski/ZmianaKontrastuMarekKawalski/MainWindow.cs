using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using ChangeContrastMarekKawalskiDll;
using System.Text;

namespace ChangeContrastMarekKawalski
{
    public partial class MainWindow : Window
    {
        //[DllImport("../../../../x64/Debug/KontrastAsm.dll")]
        //private static extern int MyProc1(int a, int b);

        private FileHandler fileHandler;
        private ContrastFilterCs contrastFilterDll;

        private Image originalImage;
        private Image alteredImage;
        private ImageHandler imageHandler;

        //private readonly Mytimer timer;

        private byte[] convertedImageBytes;

        public byte[] imageBytes;

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)] public String pValue3;
        //private char [] convertedImagechars;

        private System.Windows.Media.SolidColorBrush myBlack;
        private System.Windows.Media.SolidColorBrush myWhite;

        private const string DllFilePath = "../../../../x64/Debug/KontrastCppDll.dll";

        [DllImport(DllFilePath, CallingConvention = CallingConvention.Cdecl)]
        private static extern string ConvertImageCpp(double sliderValue, char[] pixelValues);

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            fileHandler = new FileHandler();
            contrastFilterDll = new ContrastFilterCs();
            imageHandler = new ImageHandler();

            //check if start image exists
            if (File.Exists("../../../Resources/startImage.jpg"))
            {
                originalImage = new Bitmap("../../../Resources/startImage.jpg");
                alteredImage = new Bitmap("../../../Resources/startImage.jpg");
            }
            myBlack = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            myWhite = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            //int x = 4;

            //int y = 1;
            //int val = MyProc1(x, y);
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

        private unsafe void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            //check if choosen image or start image exist
            if (originalImage != null)
            {
                imageBytes = imageHandler.ReadGivenImageBytes(ref originalImage);

                //check if C# was choosen
                if (languageChooser.SelectedItem == chooseCSharp)
                {
                    convertedImageBytes = contrastFilterDll.ConvertImage(contrastSlider.Value, imageBytes);
                    alteredImage = imageHandler.SetValuesOfBitmap(ref convertedImageBytes);

                    //alteredImage = contrastFilterDll.ConvertImage(contrastSlider.Value, originalImage);
                    _ = cSharpTimes.Items.Add(cSharpTimes.Items.Count + "\t" + contrastFilterDll.DisplayElapsedTime());
                }
                else if (languageChooser.SelectedItem == chooseCpp)
                {
                    //alteredImage = imageHandler.SetValuesOfBitmap(imageHandler.CharToByteArray(ConvertImageCpp(contrastSlider.Value,  imageHandler.ByteToCharArray(imageBytes))));
                }
                //check if assembly was choosen
                else if (languageChooser.SelectedItem == chooseAsm)
                {
                    MessageBox.Show("Feature hasn't been implemented yet", "ToDo!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
                                                 //toggleContrastLabel.Foreground = myWhite;
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
            //toggleContrastLabel.ClearValue(ForegroundProperty);
            originalImageLabel.ClearValue(ForegroundProperty);
            convertedImageLabel.ClearValue(ForegroundProperty);
            maxValueLabel.ClearValue(ForegroundProperty);
            minValueLabel.ClearValue(ForegroundProperty);
            zeroValueLabel.ClearValue(ForegroundProperty);
            timesLabel.ClearValue(ForegroundProperty);
        }
    }
}
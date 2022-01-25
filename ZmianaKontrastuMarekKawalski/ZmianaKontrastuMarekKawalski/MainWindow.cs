using System;
using System.Drawing;
using System.IO;
using System.Windows;

namespace ChangeContrastMarekKawalski
{
    /// <summary>
    /// Application main controller class. It is responsible for handling all clics and actions.
    /// </summary>

    public partial class MainWindow : Window
    {
        private readonly FileHandler fileHandler;
        private Image originalImage;

        private Image alteredImage;
        private Filter Filter;
        private byte[] imageBytes;
        private readonly ImageHandler imageHandler;
        private readonly System.Windows.Media.SolidColorBrush myBlack;
        private readonly System.Windows.Media.SolidColorBrush myWhite;

        /// <summary>
        /// Constructor. It creates instances of classes and sets everything up.
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
            //display application always in the center of screen
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            fileHandler = new FileHandler();
            imageHandler = new ImageHandler();

            //check if start image exists
            if (File.Exists("../../../../Resources/startImage.jpg"))
            {
                originalImage = new Bitmap("../../../../Resources/startImage.jpg");
                alteredImage = new Bitmap("../../../../Resources/startImage.jpg");
            }
            //init basic colors
            myBlack = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(51, 51, 51));
            myWhite = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            //set default dark theme
            toggleContrast.IsChecked = true;
        }

        /// <summary>
        /// Method handles button choose photo click. It opens file browser and let's usern choose
        /// photo they desire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            originalImage = fileHandler.OpenImageFile();
            if (originalImage == null)
            {
                originalImage = new Bitmap("../../../../Resources/startImage.jpg");
            }
            ChoosenImage.Source = imageHandler.BitmapToImageSource((System.Drawing.Bitmap)originalImage);
            ConvertedImage.Source = imageHandler.BitmapToImageSource((System.Drawing.Bitmap)originalImage);
            cSharpTimes.Items.Clear();
            asmTimes.Items.Clear();
        }

        /// <summary>
        /// Method handles button save photo click. It allows user to save photos they have converted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSavePhoto_Click(object sender, RoutedEventArgs e)
        {
            fileHandler.SaveImageFile(alteredImage);
        }

        /// <summary>
        /// The most important method. It handles all calculations connected with image converion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            //check if choosen image or start image exist
            if (originalImage == null)
            {
                _ = MessageBox.Show("Unable to process image!\n", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            imageBytes = imageHandler.ReadGivenImageBytes(originalImage);

            //check if C# was choosen
            if (languageChooser.SelectedItem == chooseCSharp)
            {
                Filter = new FilterCs(contrastSlider.Value);
                cSharpTimesHeader.IsSelected = true;
            }
            //check if assembly was choosen
            else if (languageChooser.SelectedItem == chooseAsm)
            {
                Filter = new FilterAsm(contrastSlider.Value);
                asmTimesHeader.IsSelected = true;
            }

            testLabel.Content = "factor: " + Filter.CalculateFactorValue().ToString();

            //convert image contrast using alghoritm choosen by user
            Filter.ConvertImageContrast(ref imageBytes);
            //store and display elapsed time
            string elapsedTime = Filter.DisplayElapsedTime();

            //choose where to add elapsed time
            if (Filter.GetType() == typeof(FilterCs))
            {
                _ = cSharpTimes.Items.Add(cSharpTimes.Items.Count + "\t" + elapsedTime);
            }
            else if (Filter.GetType() == typeof(FilterAsm))
            {
                _ = asmTimes.Items.Add(asmTimes.Items.Count + "\t" + elapsedTime);
            }

            alteredImage = imageHandler.SetValuesOfBitmap(imageBytes);
            ConvertedImage.Source = imageHandler.BitmapToImageSource((Bitmap)alteredImage);
        }

        /// <summary>
        /// Perform action on slider value change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderValueChanged(object sender, RoutedEventArgs e)
        {
            choosenValue.Content = "value: " + ((int)contrastSlider.Value).ToString();
        }

        /// <summary>
        /// Method is performed after button reverse changes click. It reverses all changes that had
        /// been done to image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRevertChanges_Click(object sender, RoutedEventArgs e)
        {
            contrastSlider.Value = 0.0;
            alteredImage = originalImage;
            ConvertedImage.Source = ChoosenImage.Source;
        }

        /// <summary>
        /// Method is performed after button reset click. It resets execution times Both in asm and C#.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            cSharpTimes.Items.Clear();
            asmTimes.Items.Clear();
        }

        /// <summary>
        /// Method that sets colors up for dark mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DarkModeEnabled(object sender, RoutedEventArgs e)
        {
            //enable dark mode
            myMainWindow.Background = myBlack; //main window color
            toggleContrast.Foreground = myWhite; //color of toggleContrast
            originalImageLabel.Foreground = myWhite;
            convertedImageLabel.Foreground = myWhite;
            choosenValue.Foreground = myWhite;
            minValueLabel.Foreground = myWhite;
            maxValueLabel.Foreground = myWhite;
            zeroValueLabel.Foreground = myWhite;
            timesLabel.Foreground = myWhite;
            testLabel.Foreground = myWhite;
        }

        /// <summary>
        /// Method that sets colors up for light mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightModeEnabled(object sender, RoutedEventArgs e)
        {
            //enable light mode- default
            myMainWindow.ClearValue(BackgroundProperty); //main window color
            toggleContrast.ClearValue(BackgroundProperty);
            toggleContrast.ClearValue(ForegroundProperty);
            originalImageLabel.ClearValue(ForegroundProperty);
            convertedImageLabel.ClearValue(ForegroundProperty);
            choosenValue.ClearValue(ForegroundProperty);
            maxValueLabel.ClearValue(ForegroundProperty);
            minValueLabel.ClearValue(ForegroundProperty);
            zeroValueLabel.ClearValue(ForegroundProperty);
            timesLabel.ClearValue(ForegroundProperty);
            testLabel.ClearValue(ForegroundProperty);
        }
    }
}
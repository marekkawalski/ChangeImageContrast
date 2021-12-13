using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ChangeContrastMarekKawalski
{
    /**
     Class which handles image file input and output. It also allows to
     reset content of times files.
    */

    public class FileHandler
    {
        private string saveFileName;
        private Image originaIimage;

        /**
         * Method which handles opening input image.
        */

        public Image OpenImageFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select image to convert";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.bmp|" +
             "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
             "PNG(*.png)|*.png";
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                originaIimage = new Bitmap(openFileDialog.OpenFile());
            }
            else
            {
                MessageBox.Show("Unable to load choosen file!\n", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return originaIimage;
        }

        /**
         * Method to convert Bitmap to ImageSource
         * @param bitmap bitmap to convert
         * @return ImageSource
         */

        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        /**
        * Method which handles saving converted image.
        */

        public void SaveImageFile(Image convertedImage)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Image As";
            saveFileDialog.Filter = "Image Files(*.png; *.jpg; *.jpeg)|*.png; *.jpg; *.jpeg";
            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                saveFileName = saveFileDialog.FileName;
                convertedImage.Save(saveFileName);
            }
            else
            {
                MessageBox.Show("Unable to save file!\n", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
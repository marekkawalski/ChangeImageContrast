using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
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
        /**
         * Method which handles opening input image.
        */

        private String convertedFilePath;
        private String destinationPath;
        private String saveFileName;
        private Image originaIimage;

        public Image OpenImageFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select image to convert";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.bmp|" +
             "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
             "PNG(*.png)|*.png";

            openFileDialog.ShowDialog();
            try
            {
                originaIimage = new Bitmap(openFileDialog.OpenFile());
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to load choosen file!\n" + e.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return originaIimage;
        }

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
            saveFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                saveFileName = saveFileDialog.FileName;
            }
            else
            {
                MessageBox.Show("Unable to save file!\n", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteFile()
        {
        }
    }
}
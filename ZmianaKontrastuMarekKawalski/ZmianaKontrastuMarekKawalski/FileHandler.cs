﻿using Microsoft.Win32;
using System.Drawing;
using System.Windows;

namespace ChangeContrastMarekKawalski
{
    /**
     Class which handles image file input and output.
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
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select image to convert",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.bmp|" +
             "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
             "PNG(*.png)|*.png"
            };
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                originaIimage = new Bitmap(openFileDialog.OpenFile());
            }
            else
            {
                _ = MessageBox.Show("Unable to load choosen file!\n", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return originaIimage;
        }

        /**
        * Method which handles saving converted image.
        */

        public void SaveImageFile(Image convertedImage)
        {
            if (convertedImage == null)
            {
                _ = MessageBox.Show("Unable to save file!\n", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Image As",
                Filter = "Image Files(*.png; *.jpg; *.jpeg)|*.png; *.jpg; *.jpeg"
            };
            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                saveFileName = saveFileDialog.FileName;
                convertedImage.Save(saveFileName);
            }
            else
            {
                _ = MessageBox.Show("Unable to save file!\n", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
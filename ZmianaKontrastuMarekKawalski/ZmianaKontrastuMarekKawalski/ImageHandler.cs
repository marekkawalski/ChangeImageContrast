using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;

namespace ChangeContrastMarekKawalski
{
    /// <summary>
    /// Class is responsible for image conversion.
    /// </summary>
    public class ImageHandler
    {
        private Bitmap bitmap;
        private BitmapData bmpData;

        /// <summary>
        /// Method converts image bitmap to an array of bytes.
        /// </summary>
        /// <param name="sourceImage">image bitmap</param>
        /// <returns>array of image bytes</returns>
        public byte[] ReadGivenImageBytes(Image sourceImage)
        {
            //copy source image
            bitmap = (Bitmap)sourceImage.Clone();
            //Get values of all image points
            bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            //Declare an array to hold the bytes of the bitmap
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            // Copy the RGB values into the array
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);
            return pixelValues;
        }

        /// <summary>
        /// Method converts byte array back to bitmap image.
        /// </summary>
        /// <param name="pixelValues">array of image bytes</param>
        /// <returns>image bitmap</returns>
        public Image SetValuesOfBitmap(byte[] pixelValues)
        {
            //Set values of all image points
            //Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            // Unlock the bits.
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }

        /// <summary>
        /// Method to convert Bitmap to ImageSource
        /// </summary>
        /// <param name="bitmap">bitmap to convert</param>
        /// <returns>image source- type of file which can be displayed on screen</returns>

        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using MemoryStream memory = new MemoryStream();
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
}
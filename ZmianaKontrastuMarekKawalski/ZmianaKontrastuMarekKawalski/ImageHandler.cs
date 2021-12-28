using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ChangeContrastMarekKawalski
{
    public class ImageHandler
    {
        private Bitmap bitmap;
        private BitmapData bmpData;

        [MarshalAs(UnmanagedType.LPStr)] private byte[] convetred;

        public byte[] ReadGivenImageBytes(ref Image sourceImage)
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

        public Image SetValuesOfBitmap(ref byte[] pixelValues)
        {
            //Set values of all image points
            //Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            // Unlock the bits.
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }

        public char[] ByteToCharArray(byte[] bytes)
        {
            var chars = new char[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                chars[i] = (char)bytes[i];
            }
            return chars;
        }

        public byte[] CharToByteArray(String chars)
        {
            int len = chars.Length;
            convetred = new byte[len];
            for (int i = 0; i < len; i++)
            {
                convetred[i] = (byte)chars[i];
            }
            return convetred;
        }
    }
}
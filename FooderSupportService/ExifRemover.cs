using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace FooderSupportService
{
    public static class ExifRemover
    {
        internal static byte[] RemoveExifDataFromImage(string filePath)
        {
            using (var originalImage = Image.FromFile(filePath))
            {
                return Copy(originalImage);
            }
        }
        internal static byte[] RemoveExifDataFromImage(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            using (var originalImage = Image.FromStream(ms))
            {
                return Copy(originalImage);
            }
        }
        private static byte[] Copy(Image originalImage)
        {
            using (var newImage = new Bitmap(originalImage.Width, originalImage.Height))
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
                var b = new Bitmap(newImage);
                var converter = new ImageConverter();
                return (byte[])converter.ConvertTo(b, typeof(byte[]));
            }
        }
    }
}
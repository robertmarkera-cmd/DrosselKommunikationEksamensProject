using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace PrisPilot.Services
{
    public class ImageService
    {
        // Image to ByteArray
        public byte[] ReadFileBytes(string filePath) => File.ReadAllBytes(filePath);

        //ByteArray to image
        public BitmapImage ReencodeToBitmap(byte[] array)
        {
            try
            {
                using (var ms = new MemoryStream(array))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    return bitmap;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}

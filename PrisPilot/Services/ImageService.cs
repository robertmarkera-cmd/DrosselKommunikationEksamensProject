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

        // ByteArray to image
        //public BitmapImage ReencodeToImage(byte[] array)
        //{
        //    using (var ms = new MemoryStream(array))
        //    {
        //        var image = new BitmapImage();
        //        image.BeginInit();
        //        image.CacheOption = BitmapCacheOption.OnLoad;
        //        image.StreamSource = ms;
        //        image.EndInit();
        //        return image;
        //    }
        //}
    }
}

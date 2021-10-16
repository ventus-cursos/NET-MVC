using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Ventus
{
    partial class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            return ms.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image ToImage(this byte[] buffer)
        {
            if (buffer == null) return null;
            var ms = new MemoryStream(buffer);
            return Image.FromStream(ms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetMimeType(this Image image)
        {
            return image.RawFormat.GetMimeType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string GetMimeType(this ImageFormat imageFormat)
        {
            return ImageCodecInfo.GetImageEncoders().First(o => o.FormatID == imageFormat.Guid).MimeType;
        }
    }
}

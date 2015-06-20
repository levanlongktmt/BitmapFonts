using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FntUtils
{
    public class BitmapUtils
    {

        /// <summary>
        /// Paste source bitmap to dest bitmap at position x,y
        /// </summary>
        /// <param name="destImage"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="sourceImage"></param>
        public static void PasteBitmap(ref WriteableBitmap destImage, int x, int y, WriteableBitmap sourceImage)
        {
            int _w = sourceImage.PixelWidth;
            int _h = sourceImage.PixelHeight;
            int w = destImage.PixelWidth;
            int h = destImage.PixelHeight;
            for(int i = 0; i< _w; i++)
                for(int j = 0; j< _h; j++)
                {
                    if( (j+y) >= 0 && (j+y) < h && (i+x) >=0 && (i + x) < w)
                    {
                        int sIndex = j * _w + i;
                        int dIndex = (j + y) * w + i + x;
                        destImage.Pixels[dIndex] = sourceImage.Pixels[sIndex];
                    }
                    
                }
        }
    }
}

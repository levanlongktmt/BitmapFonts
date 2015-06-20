using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FntUtils
{
    public interface IBitmapChar
    {
        char CharId { get; }
        int X { get; }
        int Y { get; }
        int Width { get; }
        int Height { get; }
        int XOffset { get; }
        int YOffset { get; }
        int XAdvance { get; }
        char Letter { get; }
        WriteableBitmap CharImage { get; }

        void LoadChar(WriteableBitmap SourceImage, string charCfg, int LineHeight, int BaseHeigt);
    }
}

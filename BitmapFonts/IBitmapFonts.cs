using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FntUtils
{
    public interface IBitmapFonts
    {
        List<char> CharacterList {get;}
        List<WriteableBitmap> CharacterImage { get; }
        string FontName { get; }
        bool IsBold { get; }
        bool IsItalic { get; }
        Thickness CharPadding { get; }
        Point Spacing { get; }
        int LineHeight { get; }
        int BaseHeight { get; }
        int ScaleW { get; }
        int ScaleH { get; }
        int CharCount { get; }
        void LoadFont(WriteableBitmap fontImage, string fontDefine);
        void LoadFont(Uri relativeImageSource, Uri relativeFntSource);
        WriteableBitmap GetImageFromText(string text, int maxWidth = 800, int lineHeight = 0);
    }
}

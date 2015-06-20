using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FntUtils
{
    public class BitmapChar : IBitmapChar
    {
        private char _charId;
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private int _xOffset;
        private int _yOffset;
        private int _xAdvance;
        private char _letter;
        private WriteableBitmap _charImage;

        public BitmapChar()
        {
            _charId = '\0';
            _x = 0;
            _y = 0;
            _width = 0;
            _height = 0;
            _xOffset = 0;
            _yOffset = 0;
            _xAdvance = 0;
            _letter = '\0';
            _charImage = null;
        }

        public char CharId { get { return _charId; } }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public int XOffset { get { return _xOffset; } }
        public int YOffset { get { return _yOffset; } }
        public int XAdvance { get { return _xAdvance; } }
        public char Letter { get { return _letter; } }
        public WriteableBitmap CharImage { get { return _charImage; } }

        public void LoadChar(WriteableBitmap SourceImage, string charCfg, int LineHeight, int BaseHeigt)
        {
            // Load config
            LoadConfigFromString(charCfg);
            
            // Load Image fron config
            WriteableBitmap _tmp = new WriteableBitmap(_xOffset + _xAdvance, LineHeight);
            WriteableBitmap _coreContent = SourceImage.Crop(_x, _y, _width, _height);
            BitmapUtils.PasteBitmap(ref _tmp, _xOffset, _yOffset, _coreContent);
            _charImage = _tmp.Clone();
        }

        // char id=32   x=0    y=0    width=0    height=0    xoffset=0    yoffset=0    xadvance=6    page=0    chnl=0
        private void LoadConfigFromString(string charCfg)
        {
            List<string> cfgArr = charCfg.Split('\t', ' ').ToList();
            string sId = cfgArr.Single(x => x.IndexOf("id=") >= 0);
            string sX = cfgArr.Single(x => x.IndexOf("x=") >= 0);
            string sY = cfgArr.Single(x => x.IndexOf("y=") >= 0);
            string sWidth = cfgArr.Single(x => x.IndexOf("width=") >= 0);
            string sHeight = cfgArr.Single(x => x.IndexOf("height=") >= 0);
            string sXoffset = cfgArr.Single(x => x.IndexOf("xoffset=") >= 0);
            string sYoffset = cfgArr.Single(x => x.IndexOf("yoffset=") >= 0);
            string sXadvance = cfgArr.Single(x => x.IndexOf("xadvance=") >= 0);
            string sLetter = cfgArr.Single(x => x.IndexOf("letter=") >= 0);

            UInt16 iId = UInt16.Parse(sId.Split('=')[1]);
            _charId = (char)iId;
            _x = int.Parse(sX.Split('=')[1]);
            _y = int.Parse(sY.Split('=')[1]);
            _width = int.Parse(sWidth.Split('=')[1]);
            _height = int.Parse(sHeight.Split('=')[1]);
            _xOffset = int.Parse(sXoffset.Split('=')[1]);
            _yOffset = int.Parse(sYoffset.Split('=')[1]);
            _xAdvance = int.Parse(sXadvance.Split('=')[1]);
           // if (sLetter.IndexOf('=')>0) _letter = (char)(UInt16.Parse(sLetter.Split('=')[1]));
        }
    }
}

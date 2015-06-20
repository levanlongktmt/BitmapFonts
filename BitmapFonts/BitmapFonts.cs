using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;

namespace FntUtils
{
    public class BitmapFonts : IBitmapFonts
    {
        private string _fontName;
        private bool _isBold;
        private bool _isItalic;
        private Thickness _charPadding;
        private Point _spacing;
        private int _lineHeight;
        private int _baseheight;
        private int _scaleW;
        private int _scaleH;
        private int _charCount;
        private int _fontSize;
        private List<char> _characterList;
        private List<WriteableBitmap> _characterImage;

        private Dictionary<char, BitmapChar> BitmapCharDic;

        private WriteableBitmap SourceImage;
        private string FntData;

        public BitmapFonts()
        {
            _fontName = "";
            _isBold = false;
            _isItalic = false;
            _charPadding = new Thickness(0, 0, 0, 0);
            _spacing = new Point(0, 0);
            _lineHeight = 0;
            _baseheight = 0;
            _scaleW = 0;
            _scaleH = 0;
            _charCount = 0;
            _fontSize = 0;
            _characterList = new List<char>();
            _characterImage = new List<WriteableBitmap>();
            SourceImage = null;
            FntData = "";
            BitmapCharDic = new Dictionary<char, BitmapChar>();
        }

        public string FontName
        {
            get
            {
                return _fontName;
            }
        }

        public bool IsBold
        {
            get
            {
                return _isBold;
            }
        }

        public bool IsItalic
        {
            get
            {
                return _isItalic;
            }
        }

        public Thickness CharPadding
        {
            get
            {
                return _charPadding;
            }
        }

        public Point Spacing
        {
            get
            {
                return _spacing;
            }
        }

        public int LineHeight
        {
            get
            {
                return _lineHeight;
            }
        }

        public int BaseHeight
        {
            get
            {
                return _baseheight;
            }
        }
        public int ScaleW
        {
            get
            {
                return _scaleW;
            }
        }

        public int ScaleH
        {
            get
            {
                return _scaleH;
            }
        }

        public int CharCount
        {
            get
            {
                return _charCount;
            }
        }

        public int FontSize
        {
            get
            {
                return _fontSize;
            }
        }
        public List<char> CharacterList
        {
            get
            {
                return _characterList;
            }
        }

        public List<WriteableBitmap> CharacterImage
        {
            get
            {
                return _characterImage;
            }
        }

        public void LoadFont(WriteableBitmap fontImage, string fontDefine)
        {
            SourceImage = fontImage.Clone();
            FntData = string.Copy(fontDefine);

            LoadFontFromLocalData();
        }

        private void LoadFontFromLocalData()
        {
            StringReader strReader = new StringReader(FntData);
            // Read 4 fist line for FontInfo
            string str1 = "";
            for (int i = 0; i < 4; i++)
            {
                str1 = strReader.ReadLine();
                if (str1.IndexOf("info") >= 0) LoadInfo(str1);
                else if (str1.IndexOf("common") >= 0) LoadCommon(str1);
                else if (str1.IndexOf("chars") >= 0) LoadCharCount(str1);
            }

            // Load Char to list
            for (int i = 0; i < _charCount; i++)
            {
                string charCfg = strReader.ReadLine();
                BitmapChar charBmp = new BitmapChar();
                charBmp.LoadChar(SourceImage, charCfg, LineHeight, BaseHeight);
                BitmapCharDic.Add(charBmp.CharId, charBmp);
            }
        }

        public void LoadFont(Uri relativeImageSource, Uri relativeFntSource)
        {
            BitmapImage bmp = new BitmapImage(relativeImageSource);
            bmp.CreateOptions = BitmapCreateOptions.BackgroundCreation;
            string fontDefine = "";
            var ResourceStream = Application.GetResourceStream(relativeFntSource);
            if (ResourceStream != null)
            {
                using (Stream fileStream = ResourceStream.Stream)
                {
                    if (fileStream.CanRead)
                    {
                        StreamReader reader = new StreamReader(fileStream);
                        fontDefine = reader.ReadToEnd();
                        reader.Close();
                    }
                }
            }
            FntData = string.Copy(fontDefine);
           // await Task.Delay(1000);
            bmp.ImageOpened += bmp_ImageOpened;
        }

        void bmp_ImageOpened(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(FntData))
            {
                SourceImage = new WriteableBitmap(sender as BitmapImage);
                LoadFontFromLocalData();
            }
        }
        private void LoadInfo(string str1)
        {
            List<string> strArr = str1.Split(' ', '\t').ToList();
            //string _fName = strArr.Single(x => x.IndexOf("face=") >= 0);
            //_fName = _fName.Replace("\"", "").Split('=')[1];
            //int _splitIndex = _fName.LastIndexOf('\\');
            //if (_splitIndex >= 0) _fontName = _fName.Substring(_splitIndex);
            //else _fontName = _fName;

            //string _fSize = strArr.Single(x => x.IndexOf("size=") >= 0);
            //_fontSize = int.Parse(_fSize.Split('=')[1]);
            //string _sBold = strArr.Single(x => x.IndexOf("bold=") > 0);
            //int _fBold = 0;
            //if (!string.IsNullOrEmpty(_sBold)) _fBold = int.Parse(_sBold.Split('=')[1]);
            //if (_fBold == 0) _isBold = false;
            //else _isBold = true;
            //int _fItalic = int.Parse(strArr.Single(x => x.IndexOf("italic=") > 0).Split('=')[1]);
            //if (_fItalic == 0) _isItalic = false;
            //else _isItalic = true;
            //string sPadding = strArr.Single(x => x.IndexOf("padding=") >= 0);
            //sPadding = sPadding.Split('=')[1];
            //string[] aPadding = sPadding.Split(',');
            //_charPadding.Left = double.Parse(aPadding[0]);
            //_charPadding.Top = double.Parse(aPadding[1]);
            //_charPadding.Right = double.Parse(aPadding[2]);
            //_charPadding.Bottom = double.Parse(aPadding[3]);

            //string sSpacing = strArr.Single(x => x.IndexOf("spacing=") >= 0);
            //sSpacing = sSpacing.Split('=')[1];
            //string[] aSpacing = sSpacing.Split(',');
            //_spacing.X = double.Parse(aSpacing[0]);
            //_spacing.Y = double.Parse(aSpacing[1]);
        }

        private void LoadCommon(string str2)
        {
            List<string> sArr = str2.Split(' ', '\t').ToList();
            string sLineHeight = sArr.Single(x => x.IndexOf("lineHeight") >= 0);
            _lineHeight = int.Parse(sLineHeight.Split('=')[1]);
            string sBase = sArr.Single(x => x.IndexOf("base=") >= 0);
            _baseheight = int.Parse(sBase.Split('=')[1]);
        }
        private void LoadCharCount(string str3)
        {
            List<string> sArr = str3.Split(' ', '\t').ToList();
            string sCount = sArr.Single(x => x.IndexOf("count=") >= 0);
            _charCount = int.Parse(sCount.Split('=')[1]);
        }

        public WriteableBitmap GetImageFromText(string text, int maxWidth = 800)
        {
            if (string.IsNullOrEmpty(text) || CharCount == 0) return null;
            else
            {
                int textWidth = 0;
                int actualWidth = 0;
                int textLine = 1;
                WriteableBitmap resultBmp = null;


                foreach (char c in text)
                {
                    textWidth += BitmapCharDic[c].CharImage.PixelWidth;
                }

                if(textWidth <= maxWidth)
                {
                    resultBmp = GetSubStringBitmap(text);
                }
                
                else
                {
                    int i = 0;
                    int j = 0;
                    int minWidth = 0;
                    textWidth = 0;
                    List<string> listSubString = new List<string>();
                    for(i=0;i<text.Length;i++)
                    {
                        actualWidth = textWidth;
                        textWidth += BitmapCharDic[text[i]].CharImage.PixelWidth;
                        if(textWidth > maxWidth)
                        {
                            string s = text.Substring(j, i - j - 1);
                            j = i-1;
                            listSubString.Add(s);
                            if (actualWidth > minWidth) minWidth = actualWidth;
                            textWidth = BitmapCharDic[text[i]].CharImage.PixelWidth;
                        }
                    }

                    string lastSubStr = text.Substring(j);
                    listSubString.Add(lastSubStr);
                    textLine = listSubString.Count;

                    resultBmp = new WriteableBitmap(minWidth, textLine *LineHeight);

                    for(i = 0; i< listSubString.Count; i++)
                    {
                        string s = listSubString[i];
                        WriteableBitmap wrTmp = GetSubStringBitmap(s);
                        int offset = (minWidth - wrTmp.PixelWidth) / 2;
                        BitmapUtils.PasteBitmap(ref resultBmp, offset, i*LineHeight, wrTmp);
                    }
                }


                return resultBmp;
            }
        }

        private WriteableBitmap GetSubStringBitmap(string subStr)
        {
            int imgWidth = 0;
            int imgHeight = LineHeight;
            foreach (char c in subStr)
            {
                imgWidth += BitmapCharDic[c].CharImage.PixelWidth;
            }

            WriteableBitmap wrTmp = new WriteableBitmap(imgWidth, imgHeight);
            imgWidth = 0;
            foreach (char c in subStr)
            {
                WriteableBitmap img = BitmapCharDic[c].CharImage;
                //wrTmp.Blit(new Rect(imgWidth, 0, img.PixelWidth, imgHeight), img, new Rect(0, 0, img.PixelWidth, imgHeight));
                BitmapUtils.PasteBitmap(ref wrTmp, imgWidth, 0, img);
                imgWidth += img.PixelWidth;
            }
            return wrTmp;
        }

    }
}

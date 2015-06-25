# BitmapFonts
Library Render Bitmap Fonts in windows phone 8
use manual:
step 1: Put fnt and png file with same name and place in your project(ex: Assets/BitmapFont)
step 2: Load Font(should do when start app): Use static variable to storage font and load on start app. ex:
  Config.ExFntFont = new BitmapFonts();
  Config.ExFntFont.Load(path_to_fnt_file)
step 3: use static variable to create image from text, ex:
  WriableBitmap img = Config.ExFntFont.GetImageFromText("Windows phone 8");

Note: careful, if Bitmap Font not contain char in inputtext, in will be crash, i will fix this problem soon

using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Compunet.YoloV8.Plotting
{
    public static class FontManager
    {
        #region CreateFont
        internal static Font CreateFont(string fontName,float fontSize,FontStyle fontStyle = FontStyle.Regular)
        {
            Font font;
            if (LoadedFontCollection.TryGet(fontName, out FontFamily fontFamily))
            {

                font = fontFamily.CreateFont(fontSize, fontStyle);

            }
            else
            {

                font = SystemFonts.CreateFont(fontName, fontSize, fontStyle);
            
            }

            return font;
        }
        #endregion

        #region Private Methods
        private static FontCollection GetDefaultFont()
        {

            FontCollection fontCollection = new FontCollection();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("Android")))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("/system/fonts/");

                FileInfo[] files = directoryInfo.GetFiles().ToArray();

                foreach (FileInfo file in files)
                {
                    if (file.Extension.ToLower() == ".ttf" || file.Extension.ToLower() == ".otf" )
                    {

                        fontCollection.Add(file.FullName);

                    }
                    else if(file.Extension.ToLower() == ".ttc")
                    {

                        fontCollection.AddCollection(file.FullName);

                    }

                }
            }

            return fontCollection;
        }
        #endregion
        internal static FontCollection LoadedFontCollection { get; set; }

        #region constructor
        static FontManager()
        {
            LoadedFontCollection = GetDefaultFont();
        }
        #endregion
    }
}

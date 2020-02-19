using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MinimapGen.MapGenerator
{
    public class MapHelper
    {
        public static Color[] customColor = new[]
        {
            Color.FromArgb(94, 95, 53),
            Color.FromArgb(151, 130, 85),
            Color.FromArgb(176, 158, 113),
            Color.FromArgb(181, 186, 145)
        };

        public static Color[] getColors(int style)
        {
            switch (style)
            {
                case 1:
                    return new[]
                    {
                        Color.FromArgb(94, 95, 53),
                        Color.FromArgb(151, 130, 85),
                        Color.FromArgb(176, 158, 113),
                        Color.FromArgb(181, 186, 145)
                    };
                case 2:
                    return new[]
                    {
                        Color.FromArgb(145, 146, 151),
                        Color.FromArgb(162, 165, 174),
                        Color.FromArgb(183, 188, 191)
                    };
                case 3:
                    return new[]
                    {
                        Color.FromArgb(88, 98, 123),
                        Color.FromArgb(116, 131, 152)
                    };
                case 4:
                    return customColor;
            }

            throw new Exception("未知错误");
        }


        public static Color parseColor(string colorcode)
        {
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromArgb(argb);
            return clr;
        }
    }
}
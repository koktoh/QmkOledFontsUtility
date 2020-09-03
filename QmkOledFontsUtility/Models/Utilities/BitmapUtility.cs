using System;
using System.Drawing;

namespace QmkOledFontsUtility.Models.Utilities
{
    public static class BitmapUtility
    {
        public static Bitmap Expand(Bitmap src, int magnification)
        {
            if (magnification < 1)
            {
                throw new ArgumentException($"{nameof(magnification)} should be at least 1.");
            }

            var destWidth = src.Width * magnification;
            var destHeight = src.Height * magnification;

            var destBmp = new Bitmap(destWidth, destHeight);

            using (var g = Graphics.FromImage(destBmp))
            {
                for (int y = 0; y < src.Height; y++)
                {
                    for (int x = 0; x < src.Width; x++)
                    {
                        var color = src.GetPixel(x, y);

                        var brush = new SolidBrush(color);

                        g.FillRectangle(brush, x * magnification, y * magnification, magnification, magnification);
                    }
                }
            }

            return destBmp;
        }
    }
}

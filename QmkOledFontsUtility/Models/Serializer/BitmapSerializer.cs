using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using QmkOledFontsUtility.Extensions;
using QmkOledFontsUtility.Models.Common;
using QmkOledFontsUtility.Models.QmkFont;

namespace QmkOledFontsUtility.Models.Serializer
{
    public class BitmapSerializer
    {
        private readonly QmkFontContext _fontContext;
        private readonly QmkFontImageContext _imgContext;

        public BitmapSerializer(QmkFontContext fontContext, QmkFontImageContext imgContext)
        {
            this._fontContext = fontContext;
            this._imgContext = imgContext;
        }

        public Bitmap Serialize(IEnumerable<QmkLetter> letters)
        {
            var letterMatrix = letters.GroupByCount(this._imgContext.LettersParRow);

            var letterRowCount = letterMatrix.Count();
            var letterColCount = letterMatrix.First().Count();

            var bmpWidth = letterMatrix.First().Count() * this._fontContext.Width;
            var bmpHeight = letterMatrix.Count() * this._fontContext.Height;

            var bmp = new Bitmap(bmpWidth, bmpHeight);

            using (var canvas = Graphics.FromImage(bmp))
            {
                for (int rowIndex = 0; rowIndex < letterRowCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < letterColCount; colIndex++)
                    {
                        var letter = letterMatrix.ElementAt(rowIndex).ElementAt(colIndex);

                        this.DrawLetter(canvas, letter, rowIndex, colIndex);
                    }
                }
            }

            return bmp;
        }

        private void DrawLetter(Graphics canvas, QmkLetter letter, int row, int col)
        {
            var data = letter.GetLetterData();

            var originX = col * this._fontContext.Width;
            var originY = row * this._fontContext.Height;

            var rectSize = 1;

            for (int letterRow = 0; letterRow < this._fontContext.Height; letterRow++)
            {
                for (int letterCol = 0; letterCol < this._fontContext.Width; letterCol++)
                {
                    var bit = data.ElementAt(letterRow).ElementAt(letterCol);

                    var brush = bit ? Brushes.Black : Brushes.White;

                    if (this._imgContext.ShowSeparator
                        && !bit
                        && (letterRow == this._fontContext.Height - 1 || letterCol == this._fontContext.Width - 1))
                    {
                        brush = Brushes.SkyBlue;
                    }

                    var x = originX + letterCol;
                    var y = originY + letterRow;

                    canvas.FillRectangle(brush, x, y, rectSize, rectSize);
                }
            }
        }

        public IEnumerable<QmkLetter> Deserialize(string path)
        {
            using var bmp = new Bitmap(path);
            var width = bmp.Width;
            var height = bmp.Height;

            foreach (var letterBmp in this.ExtractLetterBitmap(bmp))
            {
                yield return this.ConvertToOledLetter(letterBmp, this._imgContext.ImgDeserializeThreshold);

                letterBmp.Dispose();
            }
        }

        private IEnumerable<Bitmap> ExtractLetterBitmap(Bitmap bmp)
        {
            var width = bmp.Width;
            var height = bmp.Height;

            for (int y = 0; y < height; y += this._fontContext.Height)
            {
                for (int x = 0; x < width; x += this._fontContext.Width)
                {
                    var rect = new Rectangle(x, y, this._fontContext.Width, this._fontContext.Height);

                    yield return bmp.Clone(rect, bmp.PixelFormat);
                }
            }
        }

        private QmkLetter ConvertToOledLetter(Bitmap bmp, int threshold)
        {
            var hexList = new List<HexString>();

            for (int x = 0; x < this._fontContext.Width; x++)
            {
                var bitBuilder = new StringBuilder();

                for (int y = this._fontContext.Height - 1; y >= 0; y--)
                {
                    var pixel = bmp.GetPixel(x, y);

                    var bit = pixel.R < threshold && pixel.G < threshold && pixel.B < threshold;
                    bitBuilder.Append(bit ? 1 : 0);
                }

                hexList.Add(new HexString(bitBuilder.ToString()));
            }

            return new QmkLetter(hexList);
        }
    }
}

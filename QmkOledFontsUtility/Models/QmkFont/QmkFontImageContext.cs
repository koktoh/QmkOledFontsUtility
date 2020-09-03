namespace QmkOledFontsUtility.Models.QmkFont
{
    public class QmkFontImageContext
    {
        public int LettersParRow { get; set; } = 32;
        public bool ShowSeparator { get; set; } = true;
        public int ImgDeserializeThreshold { get; set; } = 128;
    }
}

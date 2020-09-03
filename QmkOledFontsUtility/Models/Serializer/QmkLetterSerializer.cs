using System.Collections.Generic;
using System.Linq;
using QmkOledFontsUtility.Extensions;
using QmkOledFontsUtility.Models.Common;
using QmkOledFontsUtility.Models.QmkFont;

namespace QmkOledFontsUtility.Models.Serializer
{
    public class QmkLetterSerializer
    {
        private readonly QmkFontContext _context;

        public QmkLetterSerializer(QmkFontContext context)
        {
            this._context = context;
        }

        public IEnumerable<QmkLetter> Serialize(IEnumerable<HexString> hexStrings)
        {
            var letters = hexStrings.GroupByCount(this._context.Width)
                .Select(x => new QmkLetter(x));

            return letters;
        }

        public IEnumerable<HexString> Deserialize(IEnumerable<QmkLetter> letters)
        {
            return letters.SelectMany(x => x.Raw);
        }
    }
}

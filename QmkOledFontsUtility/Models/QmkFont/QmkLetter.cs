using System.Collections.Generic;
using System.Linq;
using QmkOledFontsUtility.Extensions;
using QmkOledFontsUtility.Models.Common;

namespace QmkOledFontsUtility.Models.QmkFont
{
    public class QmkLetter
    {
        public IEnumerable<HexString> Raw { get; }

        public QmkLetter(IEnumerable<HexString> raw)
        {
            this.Raw = raw;
        }

        public IEnumerable<IEnumerable<bool>> GetLetterData()
        {
            var converted = this.Convert();
            var transformed = this.Transform(converted);

            return transformed;
        }

        private IEnumerable<IEnumerable<bool>> Convert()
        {
            return this.Raw
                .Select(x => x.Value.HexToBitArray())
                .Select(x =>
                {
                    var result = new List<bool>();

                    foreach (var bit in x)
                    {
                        result.Add((bool)bit);
                    }

                    return result;
                });
        }

        private IEnumerable<IEnumerable<bool>> Transform(IEnumerable<IEnumerable<bool>> source)
        {
            var srcRowLength = source.Count();
            var srcColLength = source.First().Count();

            var dest = new List<IEnumerable<bool>>();

            for (int srcCol = 0; srcCol < srcColLength; srcCol++)
            {
                var destRows = new List<bool>();

                for (int srcRow = 0; srcRow < srcRowLength; srcRow++)
                {
                    destRows.Add(source.ElementAt(srcRow).ElementAt(srcCol));
                }

                dest.Add(destRows);
            }

            return dest;
        }
    }
}

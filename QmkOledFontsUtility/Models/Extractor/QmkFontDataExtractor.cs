using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QmkOledFontsUtility.Extensions;
using QmkOledFontsUtility.Models.Common;

namespace QmkOledFontsUtility.Models.Extractor
{
    public class QmkFontDataExtractor
    {
        private const string REX_LABEL = "hex";
        private readonly string _pattern = $@"(static )*const unsigned char font\[\] PROGMEM.*\{{\s*(?<{REX_LABEL}>(.*\s*)*)\s*\}}.*";
        private readonly HttpClient _client;
        private readonly Regex _hexRex;

        public QmkFontDataExtractor() : this(new HttpClient()) { }

        public QmkFontDataExtractor(HttpClient client)
        {
            this._client = client;
            this._hexRex = new Regex(this._pattern);
        }

        public async Task<IEnumerable<HexString>> GetFontDataAsync(Uri uri)
        {
            var res = await this._client.GetAsync(uri);
            var content = await res.Content.ReadAsStringAsync();

            return this.ExtractHex(content);
        }

        public async Task<IEnumerable<HexString>> GetFontDataAsync(FileInfo fileInfo)
        {
            var raw = await fileInfo.OpenText()
                .ReadToEndAsync();

            return this.ExtractHex(raw);
        }

        private IEnumerable<HexString> ExtractHex(string source)
        {
            var raw = this._hexRex.Match(source).Groups[REX_LABEL].Value;

            return raw.SplitNewLine()
                .Select(x => x.RexRemove(@"//.*$"))
                .SelectMany(x => x.SplitCommma())
                .Where(x => x.HasMeaningfulValue())
                .Select(x => x.Trim())
                .Select(x => new HexString(x));
        }
    }
}

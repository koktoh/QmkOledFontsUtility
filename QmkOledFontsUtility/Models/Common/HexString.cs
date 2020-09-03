using QmkOledFontsUtility.Extensions;
using System;

namespace QmkOledFontsUtility.Models.Common
{
    public class HexString
    {
        public string Value { get; }

        public HexString(string raw)
        {
            if (raw.IsHex())
            {
                this.Value = raw;
                return;
            }
            else if (raw.IsBinary())
            {
                var num = Convert.ToInt32(raw.ToString(), 2);

                this.Value = num.ToHexString();
                return;
            }

            throw new ArgumentException($@"Argument ""{nameof(raw)}"" should be HEX or Binary format. (e.g. ""0x10"", ""1010"", etc...): ""{raw}""");
        }

        public HexString(string raw, int digit)
        {
            if (raw.IsHex())
            {
                var num = raw.HexToInt32();

                this.Value = num.ToHexString(digit);
            }
            else if (raw.IsBinary())
            {
                var num = Convert.ToInt32(raw.ToString(), 2);

                this.Value = num.ToHexString(digit);
            }

            throw new ArgumentException($@"Argument ""{nameof(raw)}"" should be HEX or Binary format. (e.g. ""0x10"", ""1010"", etc...)");
        }

        public HexString(short num)
        {
            this.Value = num.ToHexString();
        }

        public HexString(short num, int digit)
        {
            this.Value = num.ToHexString(digit);
        }

        public HexString(ushort num)
        {
            this.Value = num.ToHexString();
        }

        public HexString(ushort num, int digit)
        {
            this.Value = num.ToHexString(digit);
        }

        public HexString(int num)
        {
            this.Value = num.ToHexString();
        }

        public HexString(int num, int digit)
        {
            this.Value = num.ToHexString(digit);
        }

        public HexString(uint num)
        {
            this.Value = num.ToHexString();
        }

        public HexString(uint num, int digit)
        {
            this.Value = num.ToHexString(digit);
        }

        public HexString(long num)
        {
            this.Value = num.ToHexString();
        }

        public HexString(long num, int digit)
        {
            this.Value = num.ToHexString(digit);
        }
    }
}

using System;
using System.Collections;

namespace QmkOledFontsUtility.Extensions
{
    public static class ParsingExtensions
    {
        public static string ToHexString(this short value)
        {
            return $"0x{Convert.ToString(value, 16).ToUpper()}";
        }

        public static string ToHexString(this short value, int digit)
        {
            return $"0x{value.ToString($"x{digit}").ToUpper()}";
        }

        public static string ToHexString(this ushort value)
        {
            return $"0x{Convert.ToString(value, 16).ToUpper()}";
        }

        public static string ToHexString(this ushort value, int digit)
        {
            return $"0x{value.ToString($"x{digit}").ToUpper()}";
        }

        public static string ToHexString(this int value)
        {
            return $"0x{Convert.ToString(value, 16).ToUpper()}";
        }

        public static string ToHexString(this int value, int digit)
        {
            return $"0x{value.ToString($"x{digit}").ToUpper()}";
        }

        public static string ToHexString(this uint value)
        {
            return $"0x{Convert.ToString(value, 16).ToUpper()}";
        }

        public static string ToHexString(this uint value, int digit)
        {
            return $"0x{value.ToString($"x{digit}").ToUpper()}";
        }

        public static string ToHexString(this long value)
        {
            return $"0x{Convert.ToString(value, 16).ToUpper()}";
        }

        public static string ToHexString(this long value, int digit)
        {
            return $"0x{value.ToString($"x{digit}").ToUpper()}";
        }

        public static Int16 HexToInt16(this string value)
        {
            return Convert.ToInt16(value.RexRemove("0x"), 16);
        }

        public static Int32 HexToInt32(this string value)
        {
            return Convert.ToInt32(value.RexRemove("0x"), 16);
        }

        public static Int64 HexToInt64(this string value)
        {
            return Convert.ToInt64(value.RexRemove("0x"), 16);
        }

        public static BitArray HexToBitArray(this string value)
        {
            var hex = value.RexRemove("0x");
            var hexDigit = hex.Length;
            var bitLength = hexDigit * 4;

            var dest = new BitArray(bitLength, false);

            var bits = new BitArray(BitConverter.GetBytes(value.HexToInt64()));

            for (int i = 0; i < bitLength; i++)
            {
                dest[i] = bits[i];
            }

            return dest;
        }

        public static BitArray HexToBitArrayDescending(this string value)
        {
            var bits = value.HexToBitArray();
            var length = bits.Length;

            var dest = new BitArray(length, false);

            for (int i = 0; i < length; i++)
            {
                dest[length - 1 - i] = bits[i];
            }

            return dest;
        }
    }
}

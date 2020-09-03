using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace QmkOledFontsUtility.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static bool HasValue(this string source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool HasMeaningfulValue(this string source)
        {
            return !source.IsNullOrWhiteSpace();
        }

        public static bool IsHex(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return false;
            }

            return Regex.IsMatch(source, @"^0x[0-9a-fA-F]+$");
        }

        public static bool IsBinary(this string source)
        {
            if(source.IsNullOrWhiteSpace())
            {
                return false;
            }

            return Regex.IsMatch(source, @"^[0-1]+$");
        }

        public static string RexRemove(this string source, string pattern)
        {
            return Regex.Replace(source, pattern, string.Empty);
        }

        public static IEnumerable<string> SplitNewLine(this string source, StringSplitOptions options = StringSplitOptions.None)
        {
            return source.Split(Environment.NewLine, options);
        }

        public static IEnumerable<string> SplitCommma(this string source, StringSplitOptions options = StringSplitOptions.None)
        {
            return source.Split(",", options);
        }

    }
}

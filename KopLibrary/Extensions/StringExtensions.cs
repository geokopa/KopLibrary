using System;
using System.Collections.Generic;
using System.Text;

namespace KopLibrary.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertToEnglish(this string georgian)
        {
            String georgianAlphabet = @"აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
            string[] englishAlphabet = { "a", "b", "g", "d", "e", "v", "z", "t", "i", "k", "l", "m", "n", "o", "p", "zh", "r", "s", "t", "u", "f", "k", "g", "k", "sh", "ch", "ts", "dz", "ts", "tch", "kh", "j", "h" };

            if (georgian == null)
                return null;

            if (georgian.Length <= georgianAlphabet.Length)
            {
                for (int i = 0; i < georgian.Length; i++)
                {
                    int Index = georgianAlphabet.IndexOf(georgian[i].ToString(), StringComparison.Ordinal);

                    if (Index != -1)
                    {
                        georgian = georgian.Replace(georgian[i].ToString(), englishAlphabet[Index]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < georgianAlphabet.Length; i++)
                {
                    georgian = georgian.Replace(georgianAlphabet[i].ToString(), englishAlphabet[i]);
                }
            }

            return georgian;
        }

        /// <summary>
        /// Truncates the string to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="text">string that will be truncated</param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength, bool includeDots = true)
        {
            // replaces the truncated string to a ...
            string suffix = includeDots ? "..." : string.Empty;
            string truncatedString = text;

            if (maxLength <= 0) return truncatedString;
            int strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (text == null || text.Length <= maxLength) return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        public static string MakeUrlSlug(this string input)
        {
            return input.Truncate(120).ConvertToEnglish().ToUrlSlug();
        }
    }
}

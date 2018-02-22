using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialLoans.Util
{
    public static class StringExtensions
    {
        public static bool IsNumberic(this string s)
        {
            return !s.Any(c => !char.IsNumber(c) && !char.IsWhiteSpace(c));
        }

        public static string RemoveSpecailCharacters(this string s)
        {
            return s.Replace("`", "")
                    .Replace("~", "")
                    .Replace("!", "")
                    .Replace("@", "")
                    .Replace("#", "")
                    .Replace("$", "")
                    .Replace("%", "")
                    .Replace("^", "")
                    .Replace("&", "")
                    .Replace("*", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("-", "")
                    .Replace("_", "")
                    .Replace("=", "")
                    .Replace("+", "")
                    .Replace("[", "")
                    .Replace("{", "")
                    .Replace("]", "")
                    .Replace("}", "")
                    .Replace("\\", "")
                    .Replace("|", "")
                    .Replace(";", "")
                    .Replace(":", "")
                    .Replace("'", "")
                    .Replace("\"", "")
                    .Replace(",", "")
                    .Replace("<", "")
                    .Replace(".", "")
                    .Replace(">", "")
                    .Replace("/", "")
                    .Replace("?", "");
                    
        }

        public static string RemoveWhiteSpaceCharacters(this string s)
        {
            return s.Replace(" ","");
        }

        public static string ReplaceHtmlEscapeTags(this string s)
        {
            return s.Replace("&amp;", "&").Replace("&nbsp;", " ");
        }
    }
}

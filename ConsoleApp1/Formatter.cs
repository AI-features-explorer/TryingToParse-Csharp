using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public static class Formatter
    {
        public static string RemoveSpaces(string text)
        {
            text = Regex.Replace(text, @"/\r?\n|\r/g", "\n");
            return Regex.Replace(text, @"\s+", " ");
        }
        public static string RemoveTabs(string text)
        {
            return text.Replace("\t", "");
        }
        public static string RemoveUnifiers(string text)
        {
            text = text.Replace("&nbsp;", " ");
            text = text.Replace("&quot", "");
            text = text.Replace("&amp;", "&");
            text = text.Replace("&#34;", "");
            text = text.Replace("&#160;", " ");
            text = text.Replace("&#171;", "\"");
            text = text.Replace("&#187;", "\"");
            return text.Replace("&ndash;", "-");
        }
        public static string FormatString(string text)
        {
            text = RemoveUnifiers(text);
            text = RemoveSpaces(text);
            text = RemoveTabs(text);
            return text;
        }
        public static string DateToString(DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }
    }
}

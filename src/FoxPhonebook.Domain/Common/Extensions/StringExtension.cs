using System.Text.RegularExpressions;

namespace FoxPhonebook.Domain.Common.Extensions
{
    public static class StringExtension
    {
        public static string ReplaceSpecialCharacter(this string str)
        {
            return Regex.Replace(str, Utility.ReplaceSpecialCharacterRegexPattern, "").ToString();
        }

        public static string ReplaceMoreThanOneSpace(this string str)
        {
            return Regex.Replace(str, Utility.ReplaceMoreThanOneSpaceRegexPattern, " ").ToString();
        }
    }
}

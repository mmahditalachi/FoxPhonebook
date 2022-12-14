using System.Text.RegularExpressions;

namespace FoxPhonebook.Domain.Common.Extensions
{
    public static class GuardClauseExtesion
    {
        public static string InvalidNameInput(this IGuardClause guardClause, string input, string parameterName)
        {
            guardClause.NullOrEmpty(input, parameterName);

            input = input.ToLower()
                    .ReplaceSpecialCharacter()
                    .ReplaceMoreThanOneSpace();

            if (Regex.Match(input, "\b([a-zà-ÿ][-,a-z. ']+[ ]*)+").Success)
                throw new ArgumentException($"invalid argument parameter name: {parameterName}");

            return input;
        }
    }
}

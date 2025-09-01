
using System.Text.RegularExpressions;

namespace Domain.Extensions;

public static class DateTimeOffsetExtensions
{
    public static DateTimeOffset ConvertRelativeTimeToDate(this string relativeTimeDescription)
    {
        // Exemple : "il y a 3 mois", "il y a 2 semaines", "il y a 1 jour", etc.
        var match = Regex.Match(relativeTimeDescription, @"il y a (\d+) (mois|semaine|jour|heure|année)");
        if (!match.Success)
            return DateTimeOffset.MinValue;

        int quantity = int.Parse(match.Groups[1].Value);
        string unit = match.Groups[2].Value;

        return unit switch
        {
            "mois" => DateTimeOffset.Now.AddMonths(-quantity),
            "semaine" => DateTimeOffset.Now.AddDays(-7 * quantity),
            "jour" => DateTimeOffset.Now.AddDays(-quantity),
            "heure" => DateTimeOffset.Now.AddHours(-quantity),
            "année" => DateTimeOffset.Now.AddYears(-quantity),
            _ => DateTimeOffset.Now,
        };
    }
}

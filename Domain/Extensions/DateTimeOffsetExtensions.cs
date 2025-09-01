
using System.Text.RegularExpressions;

namespace Domain.Extensions;

public static class DateTimeOffsetExtensions
{
    public static DateTimeOffset ConvertRelativeTimeToDate(this string relativeTimeDescription)
    {
        // Dictionnaire pour convertir les mots en chiffres
        var wordToNumber = new Dictionary<string, int>
        {
            { "un", 1 }, 
            { "une", 1 }
        };

        // Expression régulière pour capturer les cas comme "il y a un an" ou "il y a 3 mois"
        var match = Regex.Match(relativeTimeDescription.ToLower(), @"il y a (un|une|\d+) (mois|semaine|jour|heure|année|ans|an)");
        if (!match.Success)
            return DateTimeOffset.MinValue;

        // Récupérer la quantité (soit un mot, soit un chiffre)
        string quantityStr = match.Groups[1].Value;
        int quantity;
        if (int.TryParse(quantityStr, out quantity))
        {
            // Cas où la quantité est déjà un chiffre (ex: "il y a 3 mois")
        }
        else
        {
            // Cas où la quantité est un mot (ex: "il y a un an")
            if (wordToNumber.TryGetValue(quantityStr, out quantity))
            {
                // Conversion réussie
            }
            else
            {
                return DateTimeOffset.MinValue; // Mot non reconnu
            }
        }

        string unit = match.Groups[2].Value;
        return unit switch
        {
            "mois" => DateTimeOffset.Now.AddMonths(-quantity),
            "semaine" => DateTimeOffset.Now.AddDays(-7 * quantity),
            "jour" => DateTimeOffset.Now.AddDays(-quantity),
            "heure" => DateTimeOffset.Now.AddHours(-quantity),
            "année" or "ans" or "an" => DateTimeOffset.Now.AddYears(-quantity),
            _ => DateTimeOffset.Now,
        };
    }
}

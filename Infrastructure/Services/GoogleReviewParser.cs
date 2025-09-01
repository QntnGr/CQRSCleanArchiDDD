
using Application.Common.Interfaces.Services;
using Domain.Entities;
using Domain.Extensions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services;

public class GoogleReviewParser(ILogger<GoogleReviewParser> logger) : IGoogleReviewParser
{
    private readonly ILogger<GoogleReviewParser> _logger = logger;
    public List<Review> ParseReviews(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var reviews = new List<Review>();

        // Sélectionne tous les conteneurs d'avis (par exemple, les divs avec data-review-id ou une classe commune)
        var reviewContainers = doc.DocumentNode.SelectNodes("//div[@data-review-id] | //div[contains(@class, 'GHT2ce')]");

        if (reviewContainers != null)
        {
            foreach (var container in reviewContainers)
            {
                var review = new Review();

                // ID de l'avis (depuis l'attribut data-review-id ou id)
                var reviewId = container.GetAttributeValue("data-review-id", container.GetAttributeValue("id", ""));
                review.Id = string.IsNullOrEmpty(reviewId) ? Guid.NewGuid() : Guid.NewGuid(); // ou stocker reviewId dans une propriété

                // Nom de l'auteur (depuis le bouton "Partager" ou le div d4r55)
                var authorNameNode = container.SelectSingleNode(".//div[contains(@class, 'd4r55')]") ??
                                     container.SelectSingleNode(".//button[contains(@aria-label, 'Partager l')]");
                review.AuthorName = authorNameNode != null ?
                    (authorNameNode.Name == "div" ? authorNameNode.InnerText.Trim() :
                     authorNameNode.GetAttributeValue("aria-label", string.Empty).Replace("Partager l'avis de ", string.Empty).Trim(new[] { '.' })) :
                    "Auteur inconnu";

                // Photo de profil (si disponible)
                var profilePhotoNode = container.SelectSingleNode(".//img[contains(@class, 'NBa7we')]");
                review.ProfilePhotoUrl = profilePhotoNode?.GetAttributeValue("src", "") ?? "";

                // Note (depuis la span kvMYJc)
                var ratingText = container.SelectSingleNode(".//span[contains(@class, 'kvMYJc')]")?
                    .GetAttributeValue("aria-label", "")?.Replace("&nbsp;", " ");
                review.Rating = int.TryParse(ratingText?.Split(' ')[0], out int rating) ? rating : 0;

                // Texte de l'avis
                review.Text = container?.SelectSingleNode(".//span[contains(@class, 'wiI7pd')]")?.InnerText?.Trim() ?? string.Empty;

                // Date relative
                var relativeTimeNode = container.SelectSingleNode(".//span[contains(@class, 'rsqaWe')]")?.InnerText?.Replace("&nbsp;", " ").Trim() ?? string.Empty;
                review.RelativeTimeDescription = relativeTimeNode;

                // Langue
                var languageNode = container.SelectSingleNode(".//div[@lang]");
                review.Language = languageNode?.GetAttributeValue("lang", "fr") ?? "fr";

                // Date absolue
                review.Date = review.RelativeTimeDescription.ConvertRelativeTimeToDate();

                // IsTranslated (par défaut false)
                review.IsTranslated = false;

                if (string.IsNullOrEmpty(review.Text)
                    || reviews.Any(rv => rv.Text == review.Text))
                    continue;

                reviews.Add(review);
            }
        }

        _logger.LogInformation($"Nombre d'avis parsés: {reviews.Count}");
        return reviews;
    }

}

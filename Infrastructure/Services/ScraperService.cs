
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace Infrastructure.Services;

public class ScraperService(ILogger<IScraperService> logger) : IScraperService
{
    private readonly ILogger<IScraperService> _logger = logger;

    public async Task<string> GetHtmlAsync(string search)
    {
        string html = string.Empty;
        _logger.LogInformation("chargement de la place google reviews avec Playwright");
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false // mettre à true pour exécution en arrière-plan
        });

        var page = await browser.NewPageAsync();
        // Remplace par l'URL Google Maps du lieu
        await page.GotoAsync($"https://www.google.com/maps");

        // Vérifier si le bouton RGPD est présent
        var rgpdButton = page.Locator("button:has-text(\"Tout accepter\")").First;
        if (await rgpdButton.IsVisibleAsync())
        {
            await rgpdButton.ClickAsync();
            await page.WaitForTimeoutAsync(1000); // petite pause après le clic
        }

        try
        {
            //attendre la barre de recherche
            await page.WaitForSelectorAsync("input#searchboxinput");

            //entrer la recherche
            await page.FillAsync("input#searchboxinput", search);
            await page.Keyboard.PressAsync("Enter");
            await page.WaitForTimeoutAsync(3000); // attendre le chargement

            // Cliquer pour ouvrir les avis
            await page.Locator("button[aria-label^=\"Plus d\\'avis\"]").ClickAsync();
            await page.WaitForTimeoutAsync(3000);

            var boutonsPlus = page.Locator("button[aria-label^=\"Voir plus\"]");
            int countMore = await boutonsPlus.CountAsync();
            for (int i = 0; i < countMore; i++)
            {
                try
                {
                    var btn = boutonsPlus.Nth(0); // Toujours prendre le premier bouton
                    await btn.ScrollIntoViewIfNeededAsync();
                    await btn.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
                    await btn.ClickAsync();
                    await page.WaitForTimeoutAsync(1000); // Attendre après le clic
                    boutonsPlus = page.Locator("button[aria-label^=\"Voir plus\"]"); // Rafraîchir la liste
                    countMore = await boutonsPlus.CountAsync(); // Mettre à jour le compte
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Erreur sur le bouton {i}: {ex.Message}");
                }
            }

            // Récupérer le HTML contenant les avis
            var reviewDivs = page.Locator("div[data-review-id]");
            int countReviews = await reviewDivs.CountAsync();

            for (int i = 0; i < countReviews; i++)
            {
                html += await reviewDivs.Nth(i).InnerHTMLAsync();
            }

            await browser.CloseAsync();

            return html;
        }
        catch (Exception ex)
        {
            await browser.CloseAsync();
            return ex.Message;
        }
    }
}

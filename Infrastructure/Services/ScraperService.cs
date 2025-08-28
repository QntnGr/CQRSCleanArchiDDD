
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace Infrastructure.Services;

public class ScraperService : IScraperService
{
    private readonly ILogger<IScraperService> _logger;
    public ScraperService(ILogger<IScraperService> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetHtmlAsync(string endPoint)
    {
        string html = string.Empty;
        _logger.LogInformation("chargement de la place google reviews avec Playwright");
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false // mettre à true pour exécution en arrière-plan
        });

        var page = await browser.NewPageAsync();
        //endPoint : Estelle+Priou+-+Avocate/@44.8343079,-0.5797895,17z/data=!3m1!4b1!4m14!1m7!3m6!1s0x854cacc7b3e691ad:0x24cfc866716803ce!2sEstelle+Priou+-+Avocate!8m2!3d44.8343079!4d-0.5772146!16s%2Fg%2F11y53w4j4g!3m5!1s0x854cacc7b3e691ad:0x24cfc866716803ce!8m2!3d44.8343079!4d-0.5772146!16s%2Fg%2F11y53w4j4g?entry=ttu&g_ep=EgoyMDI1MDgyNS4wIKXMDSoASAFQAw%3D%3D
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
            await page.FillAsync("input#searchboxinput", endPoint);
            await page.Keyboard.PressAsync("Enter");
            await page.WaitForTimeoutAsync(3000); // attendre le chargement

            // Cliquer pour ouvrir les avis
            await page.Locator("button[aria-label^=\"Plus d\\'avis\"]").ClickAsync();
            await page.WaitForTimeoutAsync(2000);

            // Récupérer le HTML contenant les avis
            var reviewDivs = page.Locator("div[data-review-id]");
            int count = await reviewDivs.CountAsync();

            for (int i = 0; i < count; i++)
            {
                html += await reviewDivs.Nth(i).InnerHTMLAsync();
            }

            await browser.CloseAsync();

            return html;
        }
        catch(Exception ex)
        {
            return ex.Message;
        }
    }
}

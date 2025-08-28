
namespace Application.Common.Interfaces.Services;

public interface IScraperService
{
    Task<string> GetHtmlAsync(string endPoint);
}

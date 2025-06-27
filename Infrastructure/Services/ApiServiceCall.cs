using Application.Common.Interfaces.Services;
using Flurl;
using Flurl.Http;

namespace Infrastructure.Services;

public class ApiServiceCall<T> : IApiServiceCall<T> where T : class, new()
{
    private readonly string _apiBaseUrl;
    private readonly string _apiKey;

    public ApiServiceCall(string apiBaseUrl, string apiKey)
    {
        _apiBaseUrl = apiBaseUrl;
        _apiKey = apiKey;
    }

    public async Task<T> GetAsync(string id)
    {
        return await GetUri(id).GetJsonAsync<T>();
    }

    private string GetUri(string id)
    {
        return _apiBaseUrl
        .AppendPathSegment(id)
        .SetQueryParams(new
        {
            api_key = _apiKey,
            max_results = 20,
        })
        .SetFragment("after-hash");
    }
}

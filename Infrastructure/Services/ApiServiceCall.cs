using Flurl.Http;

namespace Infrastructure.Services;

public class ApiServiceCall<T> where T : class, new()
{
    private readonly string _apiBaseUrl;

    public ApiServiceCall(string apiBaseUrl)
    {
        _apiBaseUrl = apiBaseUrl;
    }

    public async Task<T> GetAsync(int id)
    {
        return await $"{_apiBaseUrl}/{typeof(T).Name.ToLower()}/{id}"
            .GetJsonAsync<T>();
    }
}

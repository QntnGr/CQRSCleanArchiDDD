using Application.Common.Interfaces.Services;
using Flurl;
using Flurl.Http;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Services;

public class ApiServiceCall<T> : IApiServiceCall<T> where T : class
{
    private readonly Url _url;

    public ApiServiceCall(IOptions<ApiSettings> options)
    {
        _url = options.Value.BaseUrl;
        _url.SetQueryParam("key", options.Value.ApiKey);
    }

    public void AddQueryParameter(string parameterName, string value)
    {
        _url.SetQueryParam(parameterName, value);
    }

    public async Task<T> GetAsync(string id)
    {
        return await _url.GetJsonAsync<T>();
    }
}

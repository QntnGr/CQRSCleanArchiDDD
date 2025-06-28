using Application.Common.Interfaces.Services;
using Flurl;
using Flurl.Http;
using Infrastructure.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class ApiServiceCall<T> : IApiServiceCall<T> where T : class
{
    private readonly Url _url;
    private readonly ILogger<ApiServiceCall<T>> _logger;

    public ApiServiceCall(IOptions<ApiSettings> options, ILogger<ApiServiceCall<T>> logger)
    {
        _url = options.Value.BaseUrl;
        _url.SetQueryParam("key", options.Value.ApiKey);
        _logger = logger;
    }

    public void AddQueryParameter(string parameterName, string value)
    {
        _url.SetQueryParam(parameterName, value);
    }

    public async Task<T> GetAsync()
    {
        try
        {
            return await _url.GetJsonAsync<T>();
        }
        catch (FlurlHttpException ex)
        {
            _logger.LogError("Error returned from {0}: {1}", ex.Call.Request.Url, ex.Message);
            return await ex.GetResponseJsonAsync<T>();
        }
    }
}

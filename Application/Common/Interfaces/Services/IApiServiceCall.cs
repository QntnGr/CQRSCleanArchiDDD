

namespace Application.Common.Interfaces.Services;

public interface IApiServiceCall<T> where T : class
{
    void AddQueryParameter(string parameterName, string value);
    Task<T> GetAsync(string id);
}

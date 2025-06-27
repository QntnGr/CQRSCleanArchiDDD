

namespace Application.Common.Interfaces.Services;

public interface IApiServiceCall<T> where T : class
{
    Task<T> GetAsync(string id);
}

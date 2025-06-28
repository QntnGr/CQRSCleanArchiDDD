
using Domain.Entities;

namespace Application.Common.Interfaces.Services;

public interface IUserService
{
    Task<User> AuthenticateUser(string username, string password);
}

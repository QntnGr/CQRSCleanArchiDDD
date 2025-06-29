
using Domain.Entities;

namespace Application.Common.Interfaces.Services;

public interface IUserService
{
    string HashPassword(string password);
    Task<User> AuthenticateUser(string username, string password);
}

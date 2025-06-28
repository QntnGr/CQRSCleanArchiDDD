

using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Domain.Entities;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// identify user and return it
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<User> AuthenticateUser(string username, string password)
    {
        var user = await _userRepository.FindOneAsync(user => user.Name == username);

        if (user == null || !VerifyPassword(password, user.Password))
        {
            return null;
        }

        return user;
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        // logique de vérification du mot de passe, BCrypt ou autre algorithme de hachage
        return inputPassword == storedPassword;
    }
}

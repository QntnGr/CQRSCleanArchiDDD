
using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _user = new();
    public void Add(User user)
    {
        _user.Add(user);
    }
}

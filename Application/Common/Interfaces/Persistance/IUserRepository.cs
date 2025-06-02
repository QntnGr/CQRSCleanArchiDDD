
using Domain.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    public void Add(User user);
}

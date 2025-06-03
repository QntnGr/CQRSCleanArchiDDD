
using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(PlaceDbContext placeDbContext)
        : base(placeDbContext)
    {
    }
}

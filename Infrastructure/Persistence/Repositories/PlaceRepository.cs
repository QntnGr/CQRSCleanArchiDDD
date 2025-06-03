
using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class PlaceRepository : Repository<Place>, IPlaceRepository
{
    public PlaceRepository(PlaceDbContext dbContext)
        : base(dbContext)
    {
    }
}

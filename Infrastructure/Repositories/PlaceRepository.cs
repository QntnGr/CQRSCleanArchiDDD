
using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class PlaceRepository : IPlaceRepository
{
    private readonly PlaceDbContext _bContext;

    public PlaceRepository(PlaceDbContext dbContext)
    {
        _bContext = dbContext;
    }

    public void Add(Place place)
    {
        _bContext.Add(place);
    }
}

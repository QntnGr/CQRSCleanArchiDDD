
using Domain.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IPlaceRepository
{
    public void Add(Place place);
}

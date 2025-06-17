
using Application.Dto;

namespace Application.Common.Interfaces.Services;

public interface IPlaceService
{
    Task AddOnePlaceAsync(PlaceDto place);

    Task<IEnumerable<PlaceDto>> GetAllPlacesAsync();
}

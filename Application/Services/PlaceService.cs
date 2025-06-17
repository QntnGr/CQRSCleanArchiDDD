
using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Dto;
using Application.Mappings;

namespace Application.Services
{
    public class PlaceService(IPlaceRepository placeRepository) : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository = placeRepository;

        public async Task AddOnePlaceAsync(PlaceDto place)
        {
            await _placeRepository.AddAsync(place.ToEntity());
        }

        public async Task<IEnumerable<PlaceDto>> GetAllPlacesAsync()
        {
            var res = await _placeRepository.GetAllAsync();
            return res.Select(p => p.ToDto());
        }
    }
}

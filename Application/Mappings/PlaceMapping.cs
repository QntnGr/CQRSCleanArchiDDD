

using Application.Dto;
using Domain.Entities;

namespace Application.Mappings;

internal static class PlaceMapping
{
    internal static PlaceDto ToDto(this Place place)
    {
        return new PlaceDto
        {
            Description = place.Description,
            Name = place.Name,
            PlaceId = place.PlaceId,
        };
    }
}

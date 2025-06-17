

using Domain.Entities;

namespace Application.Dto;

public class PlaceDto
{
    public string PlaceId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public Place ToEntity()
    {
        return new Place
        {
            PlaceId = PlaceId,
            Name = Name,
            Description = Description
        };
    }
}

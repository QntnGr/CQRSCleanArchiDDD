
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Place
{
    public Guid Id { get; set; } = Guid.Empty;

    [Required]
    public string PlaceId { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    public ICollection<Review>? Reviews { get; set; } = null;
}

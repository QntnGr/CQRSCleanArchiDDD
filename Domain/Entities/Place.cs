
namespace Domain.Entities;

public class Place
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = null!;
}

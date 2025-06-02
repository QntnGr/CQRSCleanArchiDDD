using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class PlaceDbContext: DbContext
{
    public PlaceDbContext(DbContextOptions<PlaceDbContext> options)
        : base(options)
    {

    }

    public DbSet<Place> Places { get; set; } = null!;
}

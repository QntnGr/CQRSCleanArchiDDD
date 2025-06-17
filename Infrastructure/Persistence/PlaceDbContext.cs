using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class PlaceDbContext : DbContext
{
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;

    public PlaceDbContext(DbContextOptions<PlaceDbContext> options)
        : base(options)
    {
    }
}

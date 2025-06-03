using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

public class PlaceDbContextFactory : IDesignTimeDbContextFactory<PlaceDbContext>
{
    public PlaceDbContext CreateDbContext(string[] args)
    {
        // Nécessaire pour SQLite EF Core en design-time
        SQLitePCL.Batteries_V2.Init();

        var optionsBuilder = new DbContextOptionsBuilder<PlaceDbContext>();

        // Reconstruire ici ce que tu fais dans OnConfiguring (chemin vers places.db)
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "places.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new PlaceDbContext(optionsBuilder.Options);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public class PlaceDbContextFactory : IDesignTimeDbContextFactory<PlaceDbContext>
{
    public PlaceDbContext CreateDbContext(string[] args)
    {
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CQRSCleanArchiDDD");

        // Construire la configuration
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        // Configurer DbContext avec la chaîne de connexion
        var builder = new DbContextOptionsBuilder<PlaceDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseSqlServer(connectionString);

        return new PlaceDbContext(builder.Options);
    }
}


using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("La chaîne de connexion ne peut pas être vide ou nulle.", nameof(connectionString));
        }

        services.AddDbContext<PlaceDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IPlaceRepository, PlaceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        services.AddTransient(typeof(IApiServiceCall<>), typeof(ApiServiceCall<>));
        
        return services;
    }
}

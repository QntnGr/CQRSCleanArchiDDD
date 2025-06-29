
using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Infrastructure.Configurations;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services
        , string connectionString
        , JwtSettings settings)
    {

        // Configuration de l'authentification JWT
        services.AddScoped<JwtTokenService>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = settings.ValidIssuer,
                ValidAudience = settings.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey))
            };
        });

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
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}

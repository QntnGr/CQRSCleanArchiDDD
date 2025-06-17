
using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IPlaceService, PlaceService>();

        return services;
    }
}

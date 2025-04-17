using Brainbay.DataRelay.Application.Services;
using Brainbay.DataRelay.Caching;
using Microsoft.Extensions.DependencyInjection;
using Domain = Brainbay.DataRelay.Domain.Models;

namespace Brainbay.DataRelay;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<ICharacterService, CharacterService>()
            .AddScoped<ILocationService, LocationService>()
            .AddMemoryCache()
            .AddSingleton<ICacheProvider<Domain::Character>, MemoryCacheProvider<Domain::Character>>()
            .AddSingleton<ICacheProvider<Domain::Location>, MemoryCacheProvider<Domain::Location>>();

        return serviceCollection;
    }
}
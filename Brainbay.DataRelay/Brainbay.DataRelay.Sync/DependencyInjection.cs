using Brainbay.DataRelay.Sync.Mapping;
using Brainbay.DataRelay.Sync.ServiceClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Domain = Brainbay.DataRelay.Domain;

namespace Brainbay.DataRelay.Sync;

public static class DependencyInjection
{
    public static IServiceCollection AddSync(this IServiceCollection serviceCollection
        , IConfigurationSection configurationSection)
    {
        serviceCollection
            .Configure<ApiClientConfiguration>(configurationSection)
            .AddHttpClient<IAPIClient, APIClient>();

        serviceCollection
            .AddSingleton<ISynchronizer<Domain::Models.Location, Location>, Synchronizer<Domain::Models.Location, Location>>()
            .AddSingleton<IMapper<Domain::Models.Location, Location>, LocationMapper>();

        serviceCollection
            .AddSingleton<ISynchronizer<Domain::Models.Character, Character>, Synchronizer<Domain::Models.Character, Character>>()
            .AddSingleton<IMapper<Domain::Models.Character, Character>, CharacterMapper>();

        serviceCollection
            .AddSingleton<ISynchronizer<Domain::Models.Episode, Episode>, Synchronizer<Domain::Models.Episode, Episode>>()
            .AddSingleton<IMapper<Domain::Models.Episode, Episode>, EpisodeMapper>();

        return serviceCollection;
    }
}
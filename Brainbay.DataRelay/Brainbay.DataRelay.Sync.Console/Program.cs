using Brainbay.DataRelay.DataAccess.SQL;
using Brainbay.DataRelay.Sync.ServiceClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Brainbay.DataRelay.Sync.Console;

internal class Program
{
    public static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            }).ConfigureServices((context, collection) =>
            {
                var configuration = context.Configuration;

                collection
                    .AddDataAccessSql(configuration.GetSection("DataDestination:SqlServer"), recreate: true)
                    .AddDomainDataAccessSql()
                    .AddSync(configuration.GetSection("DataSource:RestApiClient"))
                    .AddLogging(builder => builder.AddConsole());
            }).Build();

        var syncedLocations = new Dictionary<int, Guid>();
        await host
            .Services
            .GetRequiredService<ISynchronizer<Domain.Models.Location, Location>>()
            .SyncAsync(afterSave: (domain, _) =>
            {
                if (domain.ExternalId.HasValue)
                {
                    syncedLocations.Add(domain.ExternalId.Value, domain.Id);
                }
            });

        var services = host.Services;

        var syncedEpisodes = new Dictionary<int, Guid>();

        await services
            .GetRequiredService<ISynchronizer<Domain.Models.Episode, Episode>>()
            .SyncAsync(afterSave: (domain, _) =>
            {
                if (domain.ExternalId.HasValue)
                {
                    syncedEpisodes.Add(domain.ExternalId.Value, domain.Id);
                }
            });

        await services
            .GetRequiredService<ISynchronizer<Domain.Models.Character, Character>>()
            .SyncAsync(beforeSave: (domain, api) =>
            {
                if (api.Location.ExternalId.HasValue)
                {
                    var locationId = syncedLocations[api.Location.ExternalId.Value];
                    domain.AssignToLocation(locationId);
                }

                if (api.Origin.ExternalId.HasValue)
                {
                    var originId = syncedLocations[api.Origin.ExternalId.Value];
                    domain.AssignToOrigin(originId);
                }

                foreach (var episodeExternalId in api.EpisodeIds)
                {
                    var episodeId = syncedEpisodes[episodeExternalId];
                    domain.AddToEpisode(episodeId);
                }
            }, filterApiResourcePredicate: character => character.Status.ToLower() == "alive");
    }
}
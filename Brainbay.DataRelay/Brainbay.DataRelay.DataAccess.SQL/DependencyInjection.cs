using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Brainbay.DataRelay.DataAccess.SQL.Mapping;
using Brainbay.DataRelay.DataAccess.SQL.Repositories;
using Brainbay.DataRelay.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Brainbay.DataRelay.Domain;
using Character = Brainbay.DataRelay.DataAccess.SQL.Entities.Character;
using Domain = Brainbay.DataRelay.Domain.Models;
using Location = Brainbay.DataRelay.DataAccess.SQL.Entities.Location;

namespace Brainbay.DataRelay.DataAccess.SQL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessSql(this IServiceCollection serviceCollection,
        IConfigurationSection section,
        bool recreate = false)
    {
        serviceCollection
            .AddOptionsWithValidateOnStart<DatabaseConfiguration>()
            .ValidateDataAnnotations()
            .Bind(section);

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddDbContext<RickAndMortyDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider
                    .GetRequiredService<IOptions<DatabaseConfiguration>>()
                    .Value;

                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.ConnectionString);
            });

        var dbContext = serviceCollection.BuildServiceProvider().GetRequiredService<RickAndMortyDbContext>().Database;

        if (recreate)
        {
            dbContext.EnsureDeleted();
        }

        dbContext.Migrate();

        return serviceCollection;
    }

    public static IServiceCollection AddDomainDataAccessSql(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IMapper<Domain::Location, Location>, LocationMapper>()
            .AddScoped<IRepository<Domain::Location>, Repository<Domain::Location, Location>>();

        serviceCollection
            .AddSingleton<IMapper<Domain::Character, Character>, CharacterMapper>()
            .AddScoped<IRepository<Domain::Character>, CharacterRepository>();

        serviceCollection
            .AddSingleton<IMapper<Domain::Episode, Episode>, EpisodeMapper>()
            .AddScoped<IRepository<Domain::Episode>, Repository<Domain::Episode, Episode>>();

        return serviceCollection;
    }

    public static IServiceCollection AddApplicationDataAccessSql(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<ICharacterQueryRepository, CharacterQueryRepository>()
            .AddScoped<ILocationQueryRepository, LocationQueryRepository>(); ;

        return serviceCollection;
    }
}
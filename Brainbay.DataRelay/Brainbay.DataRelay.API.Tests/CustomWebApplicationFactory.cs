using Brainbay.DataRelay.DataAccess.SQL;
using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Brainbay.DataRelay.API.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    private readonly string _connectionString;
    public IEnumerable<Location>? Locations { get; private set; }

    public IEnumerable<Character>? Characters { get; private set; }

    public CustomWebApplicationFactory(string connectionString,
        IEnumerable<Location>? locations = null,
        IEnumerable<Character>? characters = null)
    {
        _connectionString = connectionString;
        Locations = locations;
        Characters = characters;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<RickAndMortyDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<RickAndMortyDbContext>(options =>
            {
                options.UseSqlServer(_connectionString);
            });
        });
    }

    public void SeedTestData(RickAndMortyDbContext context)
    {
        if (Locations == null)
        {
            Locations = new List<Location>
                {
                    new() {Dimension = "Test",Type="Type1", Name = "Earth" },
                    new() {Dimension = "Test",Type="Type1", Name = "Citadel of Ricks" },
                    new() {Dimension = "Test",Type="Type1", Name = "Gazorpazorp" }
                };
        }

        context.Locations.AddRange(Locations);
        context.SaveChanges();

        var firstLocation = context.Locations.First();
        var secondLocation = context.Locations.Skip(1).First();

        if (Characters == null)
        {
            Characters = new List<Character>
                {
                    new() { Name = "Rick Sanchez", LocationId = secondLocation.Id},
                    new() { Name = "Morty Smith", LocationId = firstLocation.Id},
                    new() { Name = "Summer Smith", LocationId = firstLocation.Id},
                    new() { Name = "Beth Smith", LocationId = firstLocation.Id},
                    new() { Name = "Jerry Smith", LocationId = firstLocation.Id },
                    new() { Name = "Rick Sanchez1", LocationId = secondLocation.Id},
                    new() { Name = "Morty Smith1", LocationId = firstLocation.Id},
                    new() { Name = "Summer Smith1", LocationId = firstLocation.Id},
                    new() { Name = "Beth Smith1", LocationId = firstLocation.Id},
                    new() { Name = "Jerry Smith1", LocationId = firstLocation.Id }
                };
        }

        context.Characters.AddRange(Characters);
        context.SaveChanges();
    }
}
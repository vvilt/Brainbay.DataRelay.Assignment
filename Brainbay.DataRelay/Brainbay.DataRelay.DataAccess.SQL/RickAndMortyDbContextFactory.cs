using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Brainbay.DataRelay.DataAccess.SQL;

public class RickAndMortyDbContextFactory : IDesignTimeDbContextFactory<RickAndMortyDbContext>
{
    public RickAndMortyDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.migration.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<RickAndMortyDbContext>();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

        return new RickAndMortyDbContext(optionsBuilder.Options);
    }
}
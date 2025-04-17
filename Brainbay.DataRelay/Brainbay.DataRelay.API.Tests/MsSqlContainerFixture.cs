using System.Text.RegularExpressions;
using Testcontainers.MsSql;

namespace Brainbay.DataRelay.API.Tests;

public class MsSqlContainerFixture : IAsyncLifetime
{
    public MsSqlContainer Container { get; }
    public MsSqlContainerFixture()
    {
        Container = new MsSqlBuilder()
            .WithPassword("yourStrong(!)Password")
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();
    }

    private static readonly Regex _databaseConnectionStringRegex = new Regex("Database=[^;]+", RegexOptions.Compiled);


    public async Task InitializeAsync() => await Container.StartAsync();

    public async Task DisposeAsync() => await Container.StopAsync();

    public string GetConnectionString(bool randomDbName)
    {
        if (randomDbName)
        {
            return _databaseConnectionStringRegex.Replace(Container.GetConnectionString(), $"Database={Guid.NewGuid():N}");
        }

        return Container.GetConnectionString();
    }
}
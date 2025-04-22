using System.Text;
using System.Text.Json;
using Brainbay.DataRelay.API.Middlewares;
using Brainbay.DataRelay.API.Models;
using Brainbay.DataRelay.Application.DTOs;
using Brainbay.DataRelay.DataAccess.SQL;
using Microsoft.Extensions.DependencyInjection;

namespace Brainbay.DataRelay.API.Tests;

[Collection("IntegrationTests")]
public class CharacterRetrievalIntegrationTests : IAsyncLifetime
{
    private readonly MsSqlContainerFixture _msSqlFixture;
    private HttpClient _client;
    private CustomWebApplicationFactory<Program> _factory;

    public CharacterRetrievalIntegrationTests(MsSqlContainerFixture msSqlFixture)
    {
        _msSqlFixture = msSqlFixture;
    }

    [Fact]
    public async Task GetAllCharacters_ReturnsExpectedCount()
    {
        // Arrange
        int expectedNumberOfItems = _factory.Characters.Count();
        var url = $"/api/character?page=1&pageSize={expectedNumberOfItems}";

        // Act
        var pagedResult = await HttpGetResult<PagedResult<CharacterSummary>>(url);

        // Assert
        Assert.NotNull(pagedResult);
        Assert.NotNull(pagedResult.Items);
        Assert.True(pagedResult.Items.Count() <= expectedNumberOfItems,
            $"Returned {pagedResult.Items.Count()} characters, but expected no more than {expectedNumberOfItems}.");
    }

    [Fact]
    public async Task GetAllCharacters_First_Call_From_Db_Header_True()
    {
        // Arrange
        int pageSize = 20;
        var url = $"/api/character?page=1&pageSize={pageSize}";

        // Act
        using var response1 = await _client.GetAsync(url);
        response1.EnsureSuccessStatusCode();

        // Assert
        var fromDbHeader = response1.Headers.GetValues(CachedHeaderConstants.FromDatabase).Single();
        Assert.Equal("true", fromDbHeader);
    }

    [Fact]
    public async Task GetAllCharacters_First_Call_FromDb_Header_False()
    {
        // Arrange
        int pageSize = 20;
        var url = $"/api/character?page=1&pageSize={pageSize}";

        // Act
        using var response1 = await _client.GetAsync(url);
        response1.EnsureSuccessStatusCode();

        using var response2 = await _client.GetAsync(url);
        response2.EnsureSuccessStatusCode();

        // Assert
        var fromDbHeader = response2.Headers.GetValues(CachedHeaderConstants.FromDatabase).Single();
        Assert.Equal("false", fromDbHeader);
    }

    [Fact]
    public async Task Add_New_Character_Returns_Created_Character_Id()
    {
        // Arrange
        var newCharacter = new CreateCharacterRequest
        {
            Name = "Test Character"
        };

        var createdCharacter = await HttpPost<CreateCharacterRequest, CreateResponse>(newCharacter, "/api/character");

        // Assert
        Assert.NotNull(createdCharacter);
        Assert.True(createdCharacter.CreatedObjectId != Guid.Empty, "Expected a new Id on created character");
    }

    [Fact]
    public async Task Newly_Added_Character_Available_In_GetAllCharacters()
    {
        var location = _factory.Locations.Take(1).First();
        var origin = _factory.Locations.Skip(1).Take(1).First();
        // Arrange
        var newCharacter = new CreateCharacterRequest
        {
            Name = "Test Character",
            LocationId = location.Id,
            OriginId = origin.Id
        };

        var originalCharactesCount = _factory.Characters.Count();
        var updatedCharactersCount = originalCharactesCount + 1;

        var createdCharacter = await HttpPost<CreateCharacterRequest, CreateResponse>(newCharacter, "/api/character");

        // Act
        var url = $"/api/character?page=1&pageSize={updatedCharactersCount}";
        var pagedResult = await HttpGetResult<PagedResult<CharacterSummary>>(url);

        // Assert
        Assert.Equal(updatedCharactersCount, pagedResult.TotalCount);

        var item = pagedResult.Items.Single(i => i.Id == createdCharacter.CreatedObjectId);
        Assert.NotNull(item);
        Assert.Equal(newCharacter.Name, item.Name);
        Assert.Equal(location.Name, item.Location);
        Assert.Equal(origin.Name, item.Origin);
    }

    private async Task<TResponse> HttpPost<T, TResponse>(T newCharacter, string uri)
    {
        string jsonPayload = JsonSerializer.Serialize(newCharacter);
        using var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<TResponse>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return responseObject;
    }

    private async Task<T> HttpGetResult<T>(string url)
    {
        using var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var contentResponse = await response.Content.ReadAsStringAsync();
        var pagedResult = JsonSerializer.Deserialize<T>(contentResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return pagedResult;
    }

    public async Task InitializeAsync()
    {
        string connectionString = _msSqlFixture.GetConnectionString(true);
        _factory = new CustomWebApplicationFactory<Program>(connectionString);
        _client = _factory.CreateClient();
        await using var scope = _factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RickAndMortyDbContext>();
        _factory.SeedTestData(dbContext);
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}
using Brainbay.DataRelay.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Brainbay.DataRelay.DataAccess.SQL.Repositories;

public class LocationQueryRepository : ILocationQueryRepository
{
    private readonly RickAndMortyDbContext _rickAndMortyDbContext;

    public LocationQueryRepository(RickAndMortyDbContext rickAndMortyDbContext)
    {
        _rickAndMortyDbContext = rickAndMortyDbContext;
    }

    public async Task<IEnumerable<LocationNameId>> GetLocationsNameIdAsync(CancellationToken cancellationToken = default)
    {
        var locations = _rickAndMortyDbContext
            .Locations
            .AsNoTracking();

        return await locations.Select(l => new LocationNameId
        {
            Id = l.Id,
            Name = l.Name
        }).ToListAsync(cancellationToken);
    }
}
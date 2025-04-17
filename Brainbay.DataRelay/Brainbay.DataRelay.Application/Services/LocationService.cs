using Brainbay.DataRelay.Application.DTOs;
using Brainbay.DataRelay.Caching;
using Brainbay.DataRelay.Domain.Models;
using Domain = Brainbay.DataRelay.Domain.Models;

namespace Brainbay.DataRelay.Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationQueryRepository _locationQueryRepository;
    private readonly ICacheProvider<Location> _cacheProvider;

    public LocationService(ILocationQueryRepository locationQueryRepository, ICacheProvider<Domain::Location> cacheProvider)
    {
        _locationQueryRepository = locationQueryRepository;
        _cacheProvider = cacheProvider;
    }

    public async Task<CachedResult<IEnumerable<LocationNameId>>> GetLocationsNameIdAsync(
        CancellationToken cancellationToken)
    {
        var cacheKey = $"${nameof(GetLocationsNameIdAsync)}";

        return await _cacheProvider.GetOrSetAsync(cacheKey,
            async () => await _locationQueryRepository.GetLocationsNameIdAsync(cancellationToken));
    }
}
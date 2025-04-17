using Brainbay.DataRelay.Application.DTOs;

namespace Brainbay.DataRelay.Application.Services;

public interface ILocationService
{
    Task<CachedResult<IEnumerable<LocationNameId>>> GetLocationsNameIdAsync(CancellationToken cancellationToken);
}
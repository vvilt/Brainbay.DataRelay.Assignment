using System.Collections;
using Brainbay.DataRelay.Application.DTOs;

namespace Brainbay.DataRelay;

public interface ILocationQueryRepository
{
    Task<IEnumerable<LocationNameId>> GetLocationsNameIdAsync(CancellationToken cancellationToken = default);
}
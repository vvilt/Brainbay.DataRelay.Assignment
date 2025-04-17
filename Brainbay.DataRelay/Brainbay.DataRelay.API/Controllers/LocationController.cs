using Brainbay.DataRelay.API.Filters;
using Brainbay.DataRelay.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brainbay.DataRelay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController: ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    [UnwrapCachedResult]
    public async Task<IActionResult> GetLocations(CancellationToken cancellationToken = default)
    {
        var locations = await _locationService.GetLocationsNameIdAsync(cancellationToken);

        return Ok(locations);
    }
}
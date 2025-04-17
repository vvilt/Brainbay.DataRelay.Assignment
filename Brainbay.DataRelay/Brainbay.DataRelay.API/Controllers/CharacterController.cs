using System.ComponentModel.DataAnnotations;
using Brainbay.DataRelay.API.Filters;
using Brainbay.DataRelay.API.Models;
using Brainbay.DataRelay.Application.DTOs;
using Brainbay.DataRelay.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brainbay.DataRelay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController( ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter([FromBody] CreateCharacterApiRequest request, CancellationToken cancellationToken = default)
        {
            var command = new CreateCharacterRequest
            {
                Name = request.Name,
                LocationId = request.LocationId,
                OriginId = request.OriginId,
            };

            var createdObjectId = await _characterService.AddCharacterAsync(command, cancellationToken);

            return Ok(new CreateResponse
            {
                CreatedObjectId = createdObjectId
            });
        }

        [HttpGet]
        [UnwrapCachedResult]
        public async Task<IActionResult> GetCharacters([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var pagedSummary = await _characterService.GetAllCharacterSummaryAsync(page, pageSize, cancellationToken);

            return Ok(pagedSummary);
        }

        [HttpGet("origin/{origin?}")]
        [UnwrapCachedResult]
        public async Task<IActionResult> GetCharactersByOrigin(string origin = "", [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var pagedSummary = await _characterService.GetCharactersByOriginAsync(origin, page, pageSize, cancellationToken);

            return Ok(pagedSummary);
        }
    }
}

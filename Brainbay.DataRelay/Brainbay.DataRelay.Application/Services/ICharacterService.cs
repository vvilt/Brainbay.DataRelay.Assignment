using Brainbay.DataRelay.Application.DTOs;

namespace Brainbay.DataRelay.Application.Services;

public interface ICharacterService
{
    Task<Guid> AddCharacterAsync(CreateCharacterRequest request, CancellationToken cancellationToken = default);
    Task<CachedResult<PagedResult<CharacterSummary>>> GetAllCharacterSummaryAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<CachedResult<PagedResult<CharacterSummary>>> GetCharactersByOriginAsync(string origin, int page, int pageSize, CancellationToken cancellationToken);
}
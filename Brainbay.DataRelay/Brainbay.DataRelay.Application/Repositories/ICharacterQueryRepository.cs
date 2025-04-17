using Brainbay.DataRelay.Application.DTOs;

namespace Brainbay.DataRelay;

public interface ICharacterQueryRepository
{
    Task<PagedResult<CharacterSummary>> GetCharactersAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedResult<CharacterSummary>> GetCharactersByOriginAsync(string origin, int page, int pageSize, CancellationToken cancellationToken);
}
using Brainbay.DataRelay.Application.DTOs;
using Brainbay.DataRelay.Caching;
using Brainbay.DataRelay.Domain;
using Brainbay.DataRelay.Domain.Repositories;
using Domain = Brainbay.DataRelay.Domain.Models;

namespace Brainbay.DataRelay.Application.Services;

public class CharacterService : ICharacterService
{
    private readonly IRepository<Domain::Character> _characterRepository;
    private readonly ICharacterQueryRepository _characterQueryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheProvider<Domain::Character> _cacheProvider;

    public CharacterService(IRepository<Domain::Character> characterRepository,
        ICharacterQueryRepository characterQueryRepository,
        IUnitOfWork unitOfWork,
        ICacheProvider<Domain::Character> cacheProvider)
    {
        _characterRepository = characterRepository;
        _characterQueryRepository = characterQueryRepository;
        _unitOfWork = unitOfWork;
        _cacheProvider = cacheProvider;
    }

    public async Task<Guid> AddCharacterAsync(CreateCharacterRequest request, CancellationToken cancellationToken)
    {
        var character = new Domain::Character
        {
            Name = request.Name
        };

        if (request.LocationId.HasValue)
        {
            character.AssignToLocation(request.LocationId.Value);
        }

        if (request.OriginId.HasValue)
        {
            character.AssignToOrigin(request.OriginId.Value);
        }

        var addedCharacter = await _characterRepository.InsertAsync(character, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        _cacheProvider.Clear();

        return addedCharacter;
    }

    public async Task<CachedResult<PagedResult<CharacterSummary>>> GetAllCharacterSummaryAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var cacheKey = $"${nameof(GetAllCharacterSummaryAsync)}:{page}:{pageSize}";

        return await _cacheProvider.GetOrSetAsync(cacheKey,
            async () => await _characterQueryRepository.GetCharactersAsync(page, pageSize, cancellationToken));
    }

    public async Task<CachedResult<PagedResult<CharacterSummary>>> GetCharactersByOriginAsync(string origin, int page, int pageSize, CancellationToken cancellationToken)
    {
        var cacheKey = $"${nameof(GetCharactersByOriginAsync)}:{origin}:{page}:{pageSize}";

        return await _cacheProvider.GetOrSetAsync(cacheKey,
            async () => await _characterQueryRepository.GetCharactersByOriginAsync(origin, page, pageSize, cancellationToken));
    }
}
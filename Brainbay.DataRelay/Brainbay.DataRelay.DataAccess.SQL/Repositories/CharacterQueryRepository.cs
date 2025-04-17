using System.Linq.Expressions;
using Brainbay.DataRelay.Application.DTOs;
using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brainbay.DataRelay.DataAccess.SQL.Repositories;

public class CharacterQueryRepository : ICharacterQueryRepository
{
    private readonly RickAndMortyDbContext _dbContext;

    public CharacterQueryRepository(RickAndMortyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<CharacterSummary>> GetCharactersAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var characters = _dbContext.Characters
            .AsNoTracking()
            .Include(c => c.Location);

        var count = await characters.CountAsync(cancellationToken);

        var charactersResult = await characters
            .ApplyPaging(page, pageSize, c => c.Id)
            .Select(CharacterSummarySelector()).ToListAsync(cancellationToken);

        return new PagedResult<CharacterSummary>
        {
            Items = charactersResult,
            TotalCount = count,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<PagedResult<CharacterSummary>> GetCharactersByOriginAsync(string origin, int page, int pageSize, CancellationToken cancellationToken)
    {
        var characters = _dbContext.Characters
            .AsNoTracking();
        if (String.IsNullOrWhiteSpace(origin))
        {
            characters = characters.Where(c => c.OriginId == null);
        }
        else
        {
            characters = characters
                .Where(c => c.Origin.Name == origin).Include(c => c.Origin);
        }

        var count = await characters.CountAsync(cancellationToken);

        var charactersResult = await characters
            .ApplyPaging(page, pageSize, c => c.Id)
            .Select(CharacterSummarySelector()).ToListAsync(cancellationToken);

        return new PagedResult<CharacterSummary>
        {
            Items = charactersResult,
            TotalCount = count,
            Page = page,
            PageSize = pageSize
        };
    }

    private static Expression<Func<Character, CharacterSummary>> CharacterSummarySelector()
    {
        return c => new CharacterSummary
        {
            Id = c.Id,
            Name = c.Name,
            Location = c.Location == null ? null : c.Location.Name,
            Origin = c.Origin == null ? null : c.Origin.Name,
        };
    }
}
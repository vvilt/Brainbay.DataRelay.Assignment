using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Brainbay.DataRelay.DataAccess.SQL.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Brainbay.DataRelay.DataAccess.SQL.Repositories;
using Domain = Domain.Models;
public class CharacterRepository : Repository<Domain::Character, Character>
{
    public CharacterRepository(RickAndMortyDbContext rickAndMortyDbContext, IMapper<Domain.Character, Character> mapper)
        : base(rickAndMortyDbContext, mapper)
    {
    }

    public override async Task<Guid> InsertAsync(Domain.Character domain, CancellationToken cancellationToken)
    {
        domain.Id = Guid.NewGuid();
        domain.Created = DateTime.UtcNow;

        var entity = _mapper.Map(domain);

        foreach (var domainEpisodeId in domain.EpisodeIds)
        {
            var episode = _rickAndMortyDbContext.Episodes.Local.FindEntry(domainEpisodeId)?.Entity;

            if (episode == null)
            {
                episode = new Episode { Id = domainEpisodeId };
                var episodeEntry = _rickAndMortyDbContext.Episodes.Attach(episode);
                episodeEntry.State = EntityState.Unchanged;
            }

            entity.Episodes.Add(episode);
        }

        await _dbSet.AddAsync(entity, cancellationToken);

        return domain.Id;
    }
}
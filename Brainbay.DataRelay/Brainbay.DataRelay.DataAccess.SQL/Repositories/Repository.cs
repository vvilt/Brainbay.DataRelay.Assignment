using Brainbay.DataRelay.DataAccess.SQL.Mapping;
using Brainbay.DataRelay.Domain.Models;
using Brainbay.DataRelay.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brainbay.DataRelay.DataAccess.SQL.Repositories;

public class Repository<TDomain, TEntity> : IRepository<TDomain>
    where TDomain : class, IIdentifiable
    where TEntity : class
{
    protected readonly RickAndMortyDbContext _rickAndMortyDbContext;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly IMapper<TDomain, TEntity> _mapper;

    public Repository(RickAndMortyDbContext rickAndMortyDbContext, IMapper<TDomain, TEntity> mapper)
    {
        _rickAndMortyDbContext = rickAndMortyDbContext;
        _dbSet = _rickAndMortyDbContext.Set<TEntity>();
        _mapper = mapper;
    }

    public virtual async Task<Guid> InsertAsync(TDomain domain, CancellationToken cancellationToken)
    {
        domain.Id = Guid.NewGuid();
        var entity = _mapper.Map(domain);
        await _dbSet.AddAsync(entity, cancellationToken);
        
        return domain.Id;
    }
}
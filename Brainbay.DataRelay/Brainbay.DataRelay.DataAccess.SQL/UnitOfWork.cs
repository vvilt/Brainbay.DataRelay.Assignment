using Brainbay.DataRelay.Domain;
using Microsoft.EntityFrameworkCore;

namespace Brainbay.DataRelay.DataAccess.SQL;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable, IDisposable
{
    private readonly RickAndMortyDbContext _dbContext;

    public UnitOfWork(RickAndMortyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task RollbackAsync(CancellationToken cancellationToken)
    {
        foreach (var entity in _dbContext.ChangeTracker.Entries())
        {
            entity.State = entity.State switch
            {
                EntityState.Added => EntityState.Deleted,
                EntityState.Modified => EntityState.Unchanged,
                EntityState.Deleted => EntityState.Unchanged,
                _ => entity.State
            };
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}
namespace Brainbay.DataRelay.Domain;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}
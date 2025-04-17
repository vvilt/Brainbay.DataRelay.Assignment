namespace Brainbay.DataRelay.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<Guid> InsertAsync(T domain, CancellationToken cancellationToken = default);
}
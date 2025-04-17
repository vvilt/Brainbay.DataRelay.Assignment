using Brainbay.DataRelay.Application.DTOs;
using System.Threading.Tasks;

namespace Brainbay.DataRelay.Caching;

public interface ICacheProvider<TCachedGroup>
{
    bool TryGetValue<T>(string key, out T value);
    void Set<T>(string key, T value, int expirationInMinutes = 5);
    void Remove(string key);
    void Clear();

    ValueTask<CachedResult<T>> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, int expirationInMinutes = 5);
}
using Brainbay.DataRelay.Application.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace Brainbay.DataRelay.Caching;

public class MemoryCacheProvider<TCachedGroup> : ICacheProvider<TCachedGroup>
{
    private readonly IMemoryCache _cache;

    public MemoryCacheProvider(IMemoryCache cache)
    {
        _cache = cache;
    }

    public bool TryGetValue<T>(string key, out T value)
    {
        return _cache.TryGetValue(key, out value);
    }

    public void Set<T>(string key, T value, int expirationInMinutes = 5)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationInMinutes)
        };

        _cache.Set(key, value, options);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public void Clear()
    {
        if (_cache is MemoryCache memoryCache)
        {
            memoryCache.Clear();
        }
    }

    public async ValueTask<CachedResult<T>> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, int expirationInMinutes = 5)
    {
        if (_cache.TryGetValue(key, out T value))
        {
            return new CachedResult<T>
            {
                Data = value,
                FromDatabase = false,
            };
        }

        T newValue = await valueFactory();
        Set(key, newValue, 5);

        return new CachedResult<T>
        {
            Data = newValue,
            FromDatabase = true
        };
    }
}

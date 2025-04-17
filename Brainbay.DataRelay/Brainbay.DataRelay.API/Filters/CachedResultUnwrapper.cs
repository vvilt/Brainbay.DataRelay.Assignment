using System.Collections.Concurrent;
using System.Linq.Expressions;
using Brainbay.DataRelay.Application.DTOs;

namespace Brainbay.DataRelay.API.Filters;

public static class CachedResultUnwrapper
{
    private static readonly ConcurrentDictionary<Type, Func<object, (object value, bool fromDatabase)>> _cache =
        new ConcurrentDictionary<Type, Func<object, (object, bool)>>();

    public static (object value, bool fromDatabase) Unwrap(object cacheResult)
    {
        if (cacheResult == null)
        {
            throw new ArgumentNullException(nameof(cacheResult));
        }

        Type type = cacheResult.GetType();

        var func = _cache.GetOrAdd(type, BuildDelegate);
        return func(cacheResult);
    }

    private static Func<object, (object, bool)> BuildDelegate(Type type)
    {
        var param = Expression.Parameter(typeof(object), "cr");

        var cast = Expression.Convert(param, type);

        var valueProperty = Expression.Property(cast, nameof(CachedResult<object>.Data));

        var fromDatabaseProperty = Expression.Property(cast, nameof(CachedResult<object>.FromDatabase));

        var tupleConstructor = typeof(ValueTuple<object, bool>).GetConstructor(new[] { typeof(object), typeof(bool) });
        var tupleNew = Expression.New(tupleConstructor, Expression.Convert(valueProperty, typeof(object)), fromDatabaseProperty);
        var lambda = Expression.Lambda<Func<object, (object, bool)>>(tupleNew, param);
        return lambda.Compile();
    }
}
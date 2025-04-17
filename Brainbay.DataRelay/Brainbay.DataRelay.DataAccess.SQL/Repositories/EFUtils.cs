using System.Linq.Expressions;

namespace Brainbay.DataRelay.DataAccess.SQL.Repositories;

public static class EFUtils
{
    public static IQueryable<T> ApplyPaging<T, TKey>(this IQueryable<T> query, int page, int pageSize, Expression<Func<T, TKey>> orderBy)
    {
        return query.OrderBy(orderBy).Skip((page - 1) * pageSize).Take(pageSize);
    }
}
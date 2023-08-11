using Microsoft.EntityFrameworkCore;

namespace BBR.Community.API.Other.EFCore
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> IgnoreQueryFilters<TEntity>(this IQueryable<TEntity> query, bool ignoreQueryFilters) where TEntity : class
            => ignoreQueryFilters ? query.IgnoreQueryFilters() : query;
    }
}

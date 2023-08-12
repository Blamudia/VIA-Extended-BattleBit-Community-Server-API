using CommunityServerAPI.VIA.Other.EFCore;
using Microsoft.EntityFrameworkCore;

namespace CommunityServerAPI.VIA.Other.EFCore
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> IgnoreQueryFilters<TEntity>(this IQueryable<TEntity> query, bool ignoreQueryFilters) where TEntity : class
            => ignoreQueryFilters ? query.IgnoreQueryFilters() : query;
    }
}

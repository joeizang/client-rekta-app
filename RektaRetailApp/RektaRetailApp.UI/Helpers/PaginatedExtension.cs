using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RektaRetailApp.UI.Helpers
{
    public static class PaginatedExtension
    {
        public static async Task<PagedList<T>> PaginatedListAsync<T>(this IQueryable<T> queryable,
            int pageNumber, int pageSize, CancellationToken token)
            where T : class
            => await PagedList<T>.CreatePagedList(queryable, pageNumber, pageSize, token)
                .ConfigureAwait(false);

        public static PagedList<TDestination> PaginatedList<TDestination>(this IQueryable<TDestination> queryable, 
            int pageNumber, int pageSize)
            where TDestination : class
            => new PagedList<TDestination>(queryable.Count(),pageSize,pageNumber,queryable.ToList());
    }
}

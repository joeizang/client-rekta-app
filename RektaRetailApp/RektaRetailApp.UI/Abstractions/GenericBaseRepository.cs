using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Abstractions;
using RektaRetailApp.Domain.Data;

namespace RektaRetailApp.UI.Abstractions
{
    public class GenericBaseRepository : IRepository
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly RektaContext _db;

        public GenericBaseRepository(IHttpContextAccessor accessor, RektaContext db)
        {
            _accessor = accessor;
            _db = db;
        }

        public async Task Commit<T>(CancellationToken token) where T : BaseDomainModel
        {
            var user = _accessor?.HttpContext?.User?.Identity?.Name ?? "Anonymous User";
            foreach (var entity in _db.ChangeTracker.Entries<T>())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTimeOffset.Now.LocalDateTime;
                    entity.Entity.UpdatedAt = DateTimeOffset.Now.LocalDateTime;
                    if (string.IsNullOrEmpty(entity.Entity.CreatedBy))
                    {
                        entity.Entity.CreatedBy = user;
                        entity.Entity.UpdatedBy = user;
                    }
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = DateTimeOffset.Now.LocalDateTime;
                    if (string.IsNullOrEmpty(entity.Entity.UpdatedBy))
                    {
                        entity.Entity.UpdatedBy = user;
                    }
                }
            }

            await _db.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<T> GetOneBy<T>(CancellationToken token, Expression<Func<T, object>>[]? includes = null,
            params Expression<Func<T, bool>>[] searchTerms) where T : BaseDomainModel
        {
            var query = _db.Set<T>().AsNoTracking();

            if (includes is null || includes.Length == 0)
            {
                query = searchTerms.Aggregate(query, (current, term) => current.Where(term));
                var result = await query.SingleOrDefaultAsync(token).ConfigureAwait(false);
                return result;
            }

            query = includes.Aggregate(query, (current, include) => current.Include(include));
            query = searchTerms.Aggregate(query, (current, term) => current.Where(term));
            var altResult = await query.SingleOrDefaultAsync(token).ConfigureAwait(false);
            return altResult;
        }

        // public Task<PagedList<T>> GetPagedResult<T>(IRequest<T> query, CancellationToken token,
        //     Expression<Func<T, object>>[]? includes = null) where T : class
        // {
        //     IQueryable<T> queryable = null;
        //     if (includes != null && includes.Length > 0)
        //     {
        //         queryable = includes.Aggregate(queryable, (currentSequence, includable) =>
        //             currentSequence.Include(includable));
        //         if
        //     }
        //
        //
        // }
    }
}

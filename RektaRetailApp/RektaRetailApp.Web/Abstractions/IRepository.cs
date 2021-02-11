using System.Threading;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Web.Abstractions
{
    public interface IRepository
    {
        Task<T> Commit<T>(CancellationToken token) where T : BaseDomainModel;

        Task<T> GetOneBy<T>(CancellationToken token, Expression<Func<T, object>>[]? includes = null,
         params Expression<Func<T, bool>>[] searchTerms ) where T : BaseDomainModel;

        //Task<PagedList<T>> GetPagedResult<T>(IRequest<T> query, CancellationToken token,Expression<Func<T, object>>[]? includes = null) where T : class;

    }
}

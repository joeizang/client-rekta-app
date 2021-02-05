using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Abstractions;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.Abstractions
{
    public interface IRepository
    {
        Task Commit<T>(CancellationToken token) where T : BaseDomainModel;

        Task<T> GetOneBy<T>(CancellationToken token, Expression<Func<T, object>>[]? includes = null,
         params Expression<Func<T, bool>>[] searchTerms ) where T : BaseDomainModel;

        //Task<PagedList<T>> GetPagedResult<T>(IRequest<T> query, CancellationToken token,Expression<Func<T, object>>[]? includes = null) where T : class;

    }
}

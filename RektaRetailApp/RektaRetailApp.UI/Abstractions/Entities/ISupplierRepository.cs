using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.ApiModel.Supplier;
using RektaRetailApp.UI.Commands.Supplier;
using RektaRetailApp.UI.Helpers;
using RektaRetailApp.UI.Queries.Supplier;

namespace RektaRetailApp.UI.Abstractions.Entities
{
    public interface ISupplierRepository : IRepository
    {
        Task SaveAsync(CancellationToken token);

        Task<PagedList<Supplier>> GetSuppliersAsync(GetAllSuppliersQuery query, CancellationToken token);

        Task<Supplier> GetSupplierById(int id, CancellationToken token);

        Task<Supplier> GetSupplierBy(CancellationToken token, Expression<Func<Supplier, object>>[]? includes = null,
            params Expression<Func<Supplier, bool>>[] searchTerms);

        void CreateSupplier(CreateSupplierCommand command);

        void UpdateSupplier(UpdateSupplierCommand command);
        
        Task DeleteSupplier(int id, CancellationToken token);
    }
}

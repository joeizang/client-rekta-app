﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ViewModel.Supplier;
using RektaRetailApp.Web.Commands.Supplier;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Supplier;

namespace RektaRetailApp.Web.Abstractions.Entities
{
    public interface ISupplierRepository : IRepository
    {
        Task SaveAsync(CancellationToken token);

        Task<PagedList<Supplier>> SuppliersAsync(GetAllSuppliersQuery query, CancellationToken token);

        Task<Supplier> SupplierById(int id, CancellationToken token);

        Task<Supplier> SupplierBy(CancellationToken token, Expression<Func<Supplier, object>>[]? includes = null,
            params Expression<Func<Supplier, bool>>[] searchTerms);

        void CreateSupplier(CreateSupplierCommand command);

        void UpdateSupplier(UpdateSupplierCommand command);
        
        Task DeleteSupplier(int id, CancellationToken token);
    }
}

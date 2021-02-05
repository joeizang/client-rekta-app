using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.Commands.Supplier;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Supplier;

namespace RektaRetailApp.Web.Services
{
  public class SupplierRepository : GenericBaseRepository, ISupplierRepository
  {
    private readonly RektaContext _db;

    private readonly IMapper _mapper;
    private readonly DbSet<Supplier> _set;

    public SupplierRepository([NotNull] IHttpContextAccessor accessor,
        RektaContext db,
        IMapper mapper) : base(accessor, db)
    {
      _db = db;
      _set = _db.Set<Supplier>();
      _mapper = mapper;
    }

    public Task SaveAsync(CancellationToken token)
    {
      return Commit<Supplier>(token);
    }
    
    /// <summary>
    /// Returns a customized PagedList of ViewModels that will form
    /// part of the result of GetSupplierAsync Method.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<PagedList<Supplier>> SuppliersAsync(GetAllSuppliersQuery query, CancellationToken token)
    {
        var suppliers = _set.AsNoTracking();

        if (string.IsNullOrEmpty(query.SearchTerm))
        {
            if (query.PageSize is null || query.PageNumber is null)
                return await _set.PaginatedListAsync(1, 10, token);
            var result = await _set.PaginatedListAsync(query.PageNumber.Value, query.PageSize.Value, token);
            return result;
        }

        suppliers = suppliers.Where(s => s.MobileNumber != null && s.Name != null &&
                                         s.Name.Equals(query.SearchTerm) &&
                                         s.MobileNumber.Equals(query.SearchTerm));
        if (query.PageSize == null && query.PageNumber == null)
            return await suppliers.PaginatedListAsync(1, 10, token);
        var supplierResults = await suppliers.PaginatedListAsync(query.PageNumber!.Value, 
            query.PageSize!.Value, token);
        return supplierResults;
    }

    public async Task<Supplier> SupplierById(int id, CancellationToken token)
    {
      return await _set.AsNoTracking()
          .Include(s => s.ProductsSupplied)
          .SingleOrDefaultAsync(s => s.Id == id, token)
          .ConfigureAwait(false);
    }

    public async Task<Supplier> SupplierBy(CancellationToken token, Expression<Func<Supplier, object>>[]? includes = null,
        params Expression<Func<Supplier, bool>>[] searchTerms)
    {
        var supplier = await GetOneBy<Supplier>(token, includes, searchTerms).ConfigureAwait(false);
      return supplier;
    }

    public void CreateSupplier(CreateSupplierCommand command)
    {
      var supplier = _mapper.Map<CreateSupplierCommand, Supplier>(command);
      supplier.Name = supplier.Name!.Trim().ToUpperInvariant();
      supplier.MobileNumber = supplier.MobileNumber!.Trim().ToUpperInvariant();
      supplier.Description = supplier.Description!.Trim().ToUpperInvariant();
      _set.Attach(supplier);
    }

    public void UpdateSupplier(UpdateSupplierCommand command)
    {
      var exists = _set.AsNoTracking().SingleOrDefault(x => x.Id == command.Id);
      if (exists is null) return;
      exists.Name = exists.Name?.Trim().ToUpperInvariant();
      exists.MobileNumber = exists.MobileNumber?.Trim().ToUpperInvariant();
      exists.Description = exists.Description?.Trim().ToUpperInvariant();
      exists.CreatedBy = exists.CreatedBy.Trim().ToUpperInvariant();
      _db.Entry(exists).State = EntityState.Modified;
    }

    public async Task DeleteSupplier(int id, CancellationToken token)
    {
        var supplier = await _set.SingleOrDefaultAsync(x => x.Id == id, token)
            .ConfigureAwait(false);
        supplier.IsDeleted = true;
        _db.Entry(supplier).State = EntityState.Modified;
    }
  }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel.Inventory;
using RektaRetailApp.Web.Commands.Inventory;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Inventory;

namespace RektaRetailApp.Web.Services
{
  public class InventoryRepository : GenericBaseRepository, IInventoryRepository
  {
    private readonly RektaContext _db;
    private readonly IMapper _mapper;
    private readonly DbSet<Inventory> _set;

    public InventoryRepository(IHttpContextAccessor accessor, RektaContext db, IMapper mapper) : base(accessor,db)
    {
      _db = db;
      _set = _db.Set<Inventory>();
      _mapper = mapper;
    }

    public async Task<PagedList<Inventory>> GetAllInventories(GetAllInventoriesQuery request, CancellationToken token)
    {
      IQueryable<Inventory> orderedQuery;
      var query = _set.AsNoTracking()
                  .Include(i => i.Category)
                  .Include(i => i.InventoryItems);
      if (!string.IsNullOrEmpty(request.SearchString))
      {
        IQueryable<Inventory> newQuery = query.Where(x =>
            x.BatchNumber != null && x.BatchNumber!.Equals(request.SearchString) && x.Name!.Equals(request.SearchString));
        if (request.Ascending == false)
          orderedQuery = newQuery.OrderByDescending(x => x.SupplyDate).ThenByDescending(x => x.Name)
              .ThenByDescending(x => x.BatchNumber);
      }

      if (request.Ascending == false)
        orderedQuery = query.OrderByDescending(x => x.SupplyDate).ThenByDescending(x => x.Name)
            .ThenByDescending(x => x.BatchNumber);
      orderedQuery = query.OrderBy(x => x.SupplyDate).ThenBy(x => x.Name).ThenBy(x => x.BatchNumber);

      var result = await orderedQuery.PaginatedListAsync(request.PageNumber, request.PageSize, token)
          .ConfigureAwait(false);
      return result;
    }

    public async Task<InventoryDetailViewModel> GetInventoryById(int id)
    {
      var result = await _set.AsNoTracking()
          .Where(i => i.Id == id).ProjectTo<InventoryDetailViewModel>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync().ConfigureAwait(false);
      return result;
    }

    public async Task<Inventory> GetInventoryById(UpdateInventoryCommand command)
    {
      var result = await _set.AsNoTracking()
          .Where(i => i.Id == command.InventoryId)
          .SingleOrDefaultAsync().ConfigureAwait(false);
      return result;
    }

    public async Task<InventoryViewModel> GetInventoryBy(params Expression<Func<Inventory, bool>>[] searchTerms)
    {
      IQueryable<Inventory>? query = null;
      foreach (var term in searchTerms)
      {
        query = _set.AsNoTracking().Where(term);
      }

      var result = await query
          .ProjectTo<InventoryViewModel>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync().ConfigureAwait(false);
      return result;
    }

    public async Task<IEnumerable<InventoryViewModel>> GetInventoriesBy(params Expression<Func<Inventory, bool>>[] searchTerms)
    {
      IQueryable<Inventory>? query = null;
      foreach (var term in searchTerms)
      {
        query = _set.AsNoTracking().Where(term);
      }

      var result = await query
          .ProjectTo<InventoryViewModel>(_mapper.ConfigurationProvider)
          .ToListAsync().ConfigureAwait(false);
      return result;
    }

    public void CreateInventory(CreateInventoryCommand command)
    {
      var inventory = _mapper.Map<CreateInventoryCommand, Inventory>(command);
      inventory.BatchNumber = inventory.BatchNumber?.ToUpperInvariant().Trim();
      inventory.Description = inventory.Description?.ToUpperInvariant().Trim();
      inventory.Name = inventory.Name.Trim().ToUpperInvariant();

      //get the category to add the inventory
      inventory.CategoryId = _db.Categories.AsNoTracking().SingleOrDefault(x => x.Name.Equals(command.CategoryName.ToUpperInvariant()))!.Id;

      _set.Add(inventory);
    }

    public async Task UpdateInventory(UpdateInventoryCommand command)
    {
      var target = await GetInventoryById(command);
      target.BatchNumber = command.BatchNumber.Trim().ToUpperInvariant();
      target.Name = command.Name.Trim().ToUpperInvariant();
      target.Description = command.Description.Trim().ToUpperInvariant();

      _db.Entry(target).State = EntityState.Modified;
    }

    public async Task DeleteInventory(DeleteInventoryCommand command)
    {
        var result = await _set.FindAsync(command.Id).ConfigureAwait(false);

        _set.Remove(result);
    }

    public async Task SaveAsync(CancellationToken token)
    {
      await Commit<Inventory>(token).ConfigureAwait(false);
    }
  }
}

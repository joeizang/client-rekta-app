using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.ApiModel.Inventory;
using RektaRetailApp.UI.Commands.Inventory;
using RektaRetailApp.UI.Data;
using RektaRetailApp.UI.Helpers;
using RektaRetailApp.UI.Queries.Inventory;

namespace RektaRetailApp.UI.Abstractions.Entities
{
  public interface IInventoryRepository : IRepository
  {
    Task<PagedList<Inventory>> GetAllInventories(GetAllInventoriesQuery request, CancellationToken token);

    Task<InventoryDetailApiModel> GetInventoryById(int id);

    Task<Inventory> GetInventoryById(UpdateInventoryCommand id);

    Task<InventoryApiModel> GetInventoryBy(params Expression<Func<Inventory, bool>>[] searchTerms);

    Task<IEnumerable<InventoryApiModel>> GetInventoriesBy(params Expression<Func<Inventory, bool>>[] searchTerms);

    void CreateInventory(CreateInventoryCommand command);

    Task UpdateInventory(UpdateInventoryCommand command);

    Task DeleteInventory(DeleteInventoryCommand command);

    Task SaveAsync(CancellationToken token);
  }
}

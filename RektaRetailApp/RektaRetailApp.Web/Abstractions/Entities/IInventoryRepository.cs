using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ViewModel.Inventory;
using RektaRetailApp.Web.Commands.Inventory;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Inventory;

namespace RektaRetailApp.Web.Abstractions.Entities
{
  public interface IInventoryRepository : IRepository
  {
    Task<PagedList<Inventory>> GetAllInventories(GetAllInventoriesQuery request, CancellationToken token);

    Task<InventoryDetailViewModel> GetInventoryById(int id);

    Task<Inventory> GetInventoryById(UpdateInventoryCommand id);

    Task<InventoryViewModel> GetInventoryBy(params Expression<Func<Inventory, bool>>[] searchTerms);

    Task<IEnumerable<InventoryViewModel>> GetInventoriesBy(params Expression<Func<Inventory, bool>>[] searchTerms);

    void CreateInventory(CreateInventoryCommand command);

    Task UpdateInventory(UpdateInventoryCommand command);

    Task DeleteInventory(DeleteInventoryCommand command);

    Task SaveAsync(CancellationToken token);
  }
}

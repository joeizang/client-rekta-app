using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using RektaGraphQLServer.DataLoader;
using RektaGraphQLServer.Extensions;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;
using System.Collections.Generic;

namespace RektaGraphQLServer.Inventories
{
    [ExtendObjectType(Name = "Query")]
    public class InventoryQueries
    {

        [UseRektaContext]
        [UsePaging]
        public IQueryable<Inventory> GetInventories(
            [ScopedService] RektaContext context) =>
            context.Inventories
            .OrderBy(i => i.Name)
            .ThenBy(i => i.SupplyDate);

        public Task<Inventory> GetInventoryById(
            [ID(nameof(Inventory))] int id,
            InventoryByIdDataLoader dataLoader,
            CancellationToken token) => dataLoader.LoadAsync(id, token);

        public async Task<IEnumerable<Inventory>> GetInventoriesByIdAsync(
            [ID(nameof(Inventory))] int[] InventoryIds,
            InventoryByIdDataLoader dataLoader,
            CancellationToken token) => await dataLoader.LoadAsync(InventoryIds, token);

    }
}
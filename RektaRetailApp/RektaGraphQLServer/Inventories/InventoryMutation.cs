using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using RektaGraphQLServer.Common;
using RektaGraphQLServer.Extensions;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Inventories
{
    [ExtendObjectType(Name = "Mutation")]
    public class InventoryMutation
    {
        [UseRektaContext]
        public async Task<AddInventoryPayload> AddInventoryAsync(
            AddInventoryInput input,
            [ScopedService] RektaContext context,
            CancellationToken token)
        {
            var inventory = new Inventory
            {
                Name = input.Name,
                SupplyDate = input.SupplyDate,
                Quantity = input.Quantity
            };
            if (input.BatchNumber.HasValue)
                inventory.BatchNumber = input.BatchNumber;

            var categoryExists = await context.Categories.AsNoTracking()
                .AnyAsync(x => x.Id == input.CategoryId, token)
                .ConfigureAwait(false);
            if (!categoryExists)
                return new AddInventoryPayload(
                    new UserError("The Id provided is not a valid category ID", "FALSE CATEGORYID"));
            inventory.Category = new Category
            {
                Id = input.CategoryId
            };

            context.Inventories.Add(inventory);
            await context.SaveChangesAsync(token).ConfigureAwait(false);

            return new AddInventoryPayload(inventory);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using RektaGraphQLServer.DataLoader;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Types
{
    public class InventoryType : ObjectType<Inventory>
    {
        protected override void Configure(IObjectTypeDescriptor<Inventory> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<InventoryByIdDataLoader>()
                        .LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(i => i.InventoryItems)
                .ResolveWith<InventoryResolvers>(ir =>
                    ir.GetProductsAsync(default!, default!, default!, default));
        }
    }

    class InventoryResolvers
    {
        public async Task<IEnumerable<Product>> GetProductsAsync(
            Inventory inventory,
            [ScopedService] RektaContext context,
            ProductByIdDataLoader productById,
            CancellationToken token)
        {
            var productsIds = await context.Products
                .Where(i => i.Id == inventory.Id)
                .Select(i => i.Id)
                .ToArrayAsync()
                .ConfigureAwait(false);

            return await productById.LoadAsync(productsIds, token).ConfigureAwait(false);
        }
    }
}
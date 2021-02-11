using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using RektaGraphQLServer.DataLoader;
using RektaGraphQLServer.Extensions;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Types
{
    public class SaleType : ObjectType<Sale>
    {
        protected override void Configure(IObjectTypeDescriptor<Sale> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(s => s.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<SalesByIdDataLoader>()
                    .LoadAsync(id, ctx.RequestAborted));
            descriptor
                .Field(t => t.ProductsForSale)
                .ResolveWith<SaleResolvers>(t => 
                    t.GetSalesAsync(default!, default!, default!, default))
                .UseDbContext<RektaContext>()
                .Name("sales");
        }
    }

    class SaleResolvers
    {
        public async Task<IEnumerable<Product>> GetSalesAsync(
            Sale sale,
            [ScopedService] RektaContext context,
            ProductByIdDataLoader productById,
            CancellationToken token)
        {
            var soldProducts = await context.Sales
                .Where(s => s.Id == sale.Id)
                .Include(s => s.ProductsForSale)
                .SelectMany(s => s.ProductsForSale.Select(p => p.Id))
                .ToArrayAsync(token);
            return await productById.LoadAsync(soldProducts, token);
        }
    }
}
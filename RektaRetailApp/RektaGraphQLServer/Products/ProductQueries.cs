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

namespace RektaGraphQLServer.Products
{
    [ExtendObjectType(Name = "Query")]
    public class ProductQueries
    {
        [UseRektaContext]
        [UsePaging]
        public IQueryable<Product> GetProducts(
            [ScopedService] RektaContext context) =>
            context.Products.OrderBy(p => p.Name)
            .ThenBy(p => p.SupplyDate);

        public Task<Product> GetProductByIdAsync(
            [ID(nameof(Product))] int id,
            ProductByIdDataLoader dataLoader,
            CancellationToken token) => dataLoader.LoadAsync(id, token);

        public async Task<IEnumerable<Product>> GetProductsByIdAsync(
            [ID(nameof(Product))] int[] ids,
            ProductByIdDataLoader dataLoader,
            CancellationToken token) => await dataLoader.LoadAsync(ids, token);
    }
}
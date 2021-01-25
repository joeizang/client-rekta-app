using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using RektaGraphQLServer.DataLoader;
using RektaGraphQLServer.Extensions;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer
{
    public class Query
    {
        [UseRektaContext]
        public Task<List<Sale>> GetSales([ScopedService] RektaContext context) =>
            context.Sales.ToListAsync();

        public Task<Sale> GetSaleAsync(
            [ID(nameof(Sale))]int id,
            SalesByIdDataLoader dataLoader,
            CancellationToken token) => dataLoader.LoadAsync(id, token);
    }
}
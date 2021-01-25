using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.DataLoader
{
    public class ProductPriceByIdDataLoader : BatchDataLoader<int, ProductPrice>
    {
        private readonly IDbContextFactory<RektaContext> _dbContextFactory;

        public ProductPriceByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<RektaContext> dbContextFactory) : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, ProductPrice>> 
            LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using var db = _dbContextFactory.CreateDbContext();
            return await db.ProductPrices
                .Where(p => keys.Contains(p.Id))
                .Include(p => p.Product)
                .ToDictionaryAsync(p => p.Id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
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
    public class InventoryByIdDataLoader : BatchDataLoader<int, Inventory>
    {
        private readonly IDbContextFactory<RektaContext> _dbContextFactory;

        public InventoryByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<RektaContext> dbContextFactory) : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Inventory>> 
            LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using RektaContext db = _dbContextFactory.CreateDbContext();
            return await db.Inventories
                .Where(i => keys.Contains(i.Id))
                .Include(i => i.InventoryItems)
                .Include(i => i.Category)
                .ToDictionaryAsync(type => type.Id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
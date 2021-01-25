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
    public class SupplierByIdDataLoader : BatchDataLoader<int, Supplier>
    {
        private readonly IDbContextFactory<RektaContext> _dbContextFactory;

        public SupplierByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<RektaContext> dbContextFactory) : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Supplier>> 
            LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using var db = _dbContextFactory.CreateDbContext();
            return await db.Suppliers
                .Where(s => keys.Contains(s.Id))
                .Include(s => s.ProductsSupplied)
                .ToDictionaryAsync(type => type.Id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
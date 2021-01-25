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
    public class CustomerByIdDataLoader : BatchDataLoader<int, Customer>
    {
        private readonly IDbContextFactory<RektaContext> _dbContextFactory;

        public CustomerByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<RektaContext> dbContextFactory) : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Customer>> 
            LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using var db = _dbContextFactory.CreateDbContext();
            return await db.Customers
                .Where(c => keys.Contains(c.Id))
                .ToDictionaryAsync(type => type.Id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
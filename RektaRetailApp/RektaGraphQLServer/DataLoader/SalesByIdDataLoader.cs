using System;
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
    public class SalesByIdDataLoader : BatchDataLoader<int, Sale>
    {
        private readonly IDbContextFactory<RektaContext> _dbContextFactory;
        public SalesByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<RektaContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Sale>> LoadBatchAsync(IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using RektaContext dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Sales.Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
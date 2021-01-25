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
    public class ApplicationUserByIdDataLoader : BatchDataLoader<string, ApplicationUser>
    {
        private readonly IDbContextFactory<RektaContext> _dbContextFactory;

        public ApplicationUserByIdDataLoader(IBatchScheduler batchScheduler,
            IDbContextFactory<RektaContext> dbContextFactory) : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<string, ApplicationUser>> LoadBatchAsync
            (IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            await using var db = _dbContextFactory.CreateDbContext();
            return await db.ApplicationUsers
                .Where(a => keys.Contains(a.Id))
                .Include(a => a.SalesYouOwn)
                .ToDictionaryAsync(type => type.Id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
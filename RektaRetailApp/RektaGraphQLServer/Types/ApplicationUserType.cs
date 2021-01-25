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
    public class ApplicationUserType : ObjectType<ApplicationUser>
    {
        protected override void Configure(IObjectTypeDescriptor<ApplicationUser> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<ApplicationUserByIdDataLoader>()
                        .LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(a => a.SalesYouOwn)
                .ResolveWith<ApplicationUserResolvers>(r => 
                    r.GetSalesYouOwnAsync(default!, default!, default!, default))
                .Name("salesYouOwn");
        }
    }

    public class ApplicationUserResolvers
    {
        public async Task<IEnumerable<Sale>> GetSalesYouOwnAsync(
            ApplicationUser user,
            [ScopedService] RektaContext context,
            SalesByIdDataLoader salesById,
            CancellationToken token)
        {
            var salesIds = await context.Sales
                .Where(s => s.Id.ToString() == user.Id)
                .Select(s => s.Id)
                .ToArrayAsync()
                .ConfigureAwait(false);
            return await salesById.LoadAsync(salesIds, token);
        }
    }
}
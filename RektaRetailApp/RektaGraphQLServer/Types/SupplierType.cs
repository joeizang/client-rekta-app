using HotChocolate.Resolvers;
using HotChocolate.Types;
using RektaGraphQLServer.DataLoader;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Types
{
    public class SupplierType : ObjectType<Supplier>
    {
        protected override void Configure(IObjectTypeDescriptor<Supplier> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<SupplierByIdDataLoader>()
                        .LoadAsync(id, ctx.RequestAborted));
        }
    }
}
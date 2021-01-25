using HotChocolate.Resolvers;
using HotChocolate.Types;
using RektaGraphQLServer.DataLoader;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Types
{
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<CustomerByIdDataLoader>()
                        .LoadAsync(id, ctx.RequestAborted));
        }
    }
}
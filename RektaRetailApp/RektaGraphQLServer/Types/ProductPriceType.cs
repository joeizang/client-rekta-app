using HotChocolate.Resolvers;
using HotChocolate.Types;
using RektaGraphQLServer.DataLoader;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Types
{
    public class ProductPriceType : ObjectType<ProductPrice>
    {
        protected override void Configure(IObjectTypeDescriptor<ProductPrice> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<ProductPriceByIdDataLoader>()
                        .LoadAsync(id, ctx.RequestAborted));
        }
    }
}
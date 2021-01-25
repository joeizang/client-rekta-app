using HotChocolate.Resolvers;
using HotChocolate.Types;
using RektaGraphQLServer.DataLoader;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Types
{
    public class ProductCategoryType : ObjectType<ProductCategory>
    {
        protected override void Configure(IObjectTypeDescriptor<ProductCategory> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<ProductCategoryByIdDataLoader>()
                        .LoadAsync(id, ctx.RequestAborted));
        }
    }
}
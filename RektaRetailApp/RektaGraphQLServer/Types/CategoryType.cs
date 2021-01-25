using HotChocolate.Resolvers;
using HotChocolate.Types;
using RektaGraphQLServer.DataLoader;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Types
{
    public class CategoryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<CategoryByIdDataLoader>()
                        .LoadAsync(id, ctx.RequestAborted));
        }
    }
}
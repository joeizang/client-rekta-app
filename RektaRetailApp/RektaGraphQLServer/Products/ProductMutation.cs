using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HotChocolate;
using HotChocolate.Types;
using RektaGraphQLServer.Common;
using RektaGraphQLServer.Extensions;
using RektaRetailApp.Domain.Data;

namespace RektaGraphQLServer.Products
{
    [ExtendObjectType(Name="Mutation")]
    public class ProductMutation
    {
        [UseRektaContext]
        public async Task<AddProductPayload> AddProductAsync(
            AddProductInput input,
            [ScopedService] RektaContext context,
            CancellationToken token)
        {
            Guard.Against.NullOrEmpty(input.Name, nameof(input.Name));
            Guard.Against.Default(input.SupplyDate, nameof(input.SupplyDate));

            if (input.ProductCategoryIds.Count == 0)
                return new AddProductPayload(
                    new UserError("Products must have a category before they are created!", "NO_PRODUCT_CATEGORY"));
        }
        
    }
}
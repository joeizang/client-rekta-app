using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HotChocolate;
using HotChocolate.Types;
using RektaGraphQLServer.Common;
using RektaGraphQLServer.Extensions;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Products
{
    [ExtendObjectType(Name = "Mutation")]
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

            var product = new Product
            {
                Name = input.Name,
                Brand = input.Brand ?? "",
                Comments = input.Comments ?? "",
                Quantity = input.Quantity,
                ImageUrl = input.ImageUrl ?? "",
                SupplyDate = input.SupplyDate,
                SupplierId = input.SupplierId,
                InventoryId = input.InventoryId,
                ProductPriceId = input.ProductPriceId
            };

            foreach (var categoryId in input.ProductCategoryIds)
            {
                product.ProductCategories?.Add(new ProductCategory { Id = categoryId });
            }

            context.Products.Add(product);
            await context.SaveChangesAsync(token).ConfigureAwait(false);
            return new AddProductPayload(product);
        }

        [UseRektaContext]
        public async Task<UpdateProductPayload> UpdateProductAsync(
            UpdateProductInput input,
            [ScopedService] RektaContext context,
            CancellationToken token)
        {
            var targetProduct = await context.Products.FindAsync(input.Id, token).ConfigureAwait(false);
            if (!input.SupplyDate.HasValue)
            {
                return new UpdateProductPayload(
                    new UserError("Every Product must have a date", "DATE_DEFAULT_VALUE")
                );
            }

            if (!input.Name.HasValue)
            {
                return new UpdateProductPayload(
                    new UserError("Name cannot be emtpy", "NAME_NULL")
                );
            }

            if (!input.Quantity.HasValue)
            {
                return new UpdateProductPayload(
                    new UserError("Quantity must be a positive number", "NEGATIVE_NUMBER")
                );
            }

            if (input.Brand.HasValue) targetProduct.Brand = input.Brand;
            if (input.Comments.HasValue) targetProduct.Comments = input.Comments;
            if (input.ImageUrl.HasValue) targetProduct.ImageUrl = input.ImageUrl;

            await context.SaveChangesAsync(token).ConfigureAwait(false);

            return new UpdateProductPayload(targetProduct);
        }

    }
}
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
    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode((ctx, id) =>
                    ctx.DataLoader<ProductByIdDataLoader>()
                        .LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(type => type.Inventory)
                .ResolveWith<ProductResolvers>(p => p.GetInventoryAsync(default!,
                    default!, default!));
            
            descriptor
                .Field(type => type.Supplier)
                .ResolveWith<ProductResolvers>(p => p.GetSupplierAsync(default!,
                    default!, default!));
            
            descriptor
                .Field(type => type.Price)
                .ResolveWith<ProductResolvers>(p => p.GetProductPriceAsync(default!,
                    default!, default!));

            descriptor
                .Field(type => type.ProductCategories)
                .ResolveWith<ProductResolvers>(p => p.GetProductCategoriesAsync(default!,
                    default!, default!, default));
        }

        class ProductResolvers
        {
            public async Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync(
                Product product,
                [ScopedService] RektaContext context,
                ProductCategoryByIdDataLoader productCategoryById,
                CancellationToken token)
            {
                var categoryIds = await context.ProductCategories
                    .Where(p => p.Id == product.Id)
                    .Select(p => p.Id)
                    .ToArrayAsync()
                    .ConfigureAwait(false);
                return await productCategoryById.LoadAsync(categoryIds, token);
            }

        public async Task<Inventory?> GetInventoryAsync(
                Product product,
                InventoryByIdDataLoader inventoryByIdDataLoader,
                CancellationToken token)
            {
                return await inventoryByIdDataLoader.LoadAsync(product.InventoryId, token);
            }
            
            public async Task<Supplier?> GetSupplierAsync(
                Product product,
                SupplierByIdDataLoader supplierById,
                CancellationToken token)
            {
                return await supplierById.LoadAsync(product.SupplierId, token);
            }
            
            public async Task<ProductPrice> GetProductPriceAsync(
                Product product,
                ProductPriceByIdDataLoader productPriceById,
                CancellationToken token)
            {
                return await productPriceById.LoadAsync(product.ProductPriceId, token);
            }
        }
    }
}
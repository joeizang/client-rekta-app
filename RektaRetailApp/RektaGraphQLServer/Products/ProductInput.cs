using System;
using System.Collections.Generic;
using HotChocolate.Types.Relay;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Products
{
    public record AddProductInput(
        string Name, DateTimeOffset SupplyDate,
        float Quantity, string? Brand, string? ImageUrl,
        string? Comments, [ID(nameof(Inventory))] int InventoryId,
        [ID(nameof(Supplier))] int SupplierId,
        [ID(nameof(ProductPrice))] int ProductPriceId,
        [ID(nameof(ProductCategory))] IReadOnlyList<int> ProductCategoryIds
    );
}
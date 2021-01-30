using System;
using System.Collections.Generic;
using HotChocolate;
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

    public record UpdateProductInput(
        [ID(nameof(Product))] int Id,
        Optional<string?> Name, Optional<DateTimeOffset?> SupplyDate,
        Optional<float?> Quantity, Optional<string?> Brand, Optional<string?> ImageUrl,
        Optional<string?> Comments);
}
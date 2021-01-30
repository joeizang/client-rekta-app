using System;
using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types.Relay;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Inventories
{
    public record AddInventoryInput(
        string Name, Optional<string?> BatchNumber,
        DateTimeOffset SupplyDate, float Quantity,
        [ID(nameof(Category))] int CategoryId);

    public record UpdateInventoryInput(
        Optional<string?> Name, Optional<string?> BatchNumber,
        Optional<DateTimeOffset?> SupplyDate, Optional<float> Quantity,
        Optional<string?> Description);
}
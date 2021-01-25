using System;
using System.Collections.Generic;
using HotChocolate.Types.Relay;
using RektaGraphQLServer.ApiModels;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.MutationTypes
{
    public record AddSalesInput(
        string CustomerName,
        string CustomerPhoneNumber,
        decimal GrandTotal,
        decimal SubTotal,
        DateTimeOffset SaleDate,
        string SalesPersonId,
        SaleType TypeOfSale,
        PaymentType TypeOfPayment,
        [ID(nameof(Product))]IReadOnlyList<int> ProductIds
    );
}
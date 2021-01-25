using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using RektaGraphQLServer.Extensions;
using RektaGraphQLServer.MutationTypes;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Sales
{
    [ExtendObjectType(Name = "Mutation")]
    public class SaleMutation
    {
        [UseRektaContext]
        public async Task<AddSalesPayload> AddSaleAsync(
            AddSalesInput input, [ScopedService] RektaContext context, CancellationToken token)
        {
            var products = new List<Product>();

            foreach (var product in input.ProductSold)
            {
                products.Add(new Product());
            }
            var sale = new Sale
            {
                SaleDate = input.SaleDate,
                GrandTotal = input.GrandTotal,
                SubTotal = input.SubTotal,
                CustomerName = input.CustomerName,
                CustomerPhoneNumber = input.CustomerPhoneNumber,
                SalesPersonId = input.SalesPersonId,
                ModeOfPayment = input.TypeOfPayment,
                TypeOfSale = input.TypeOfSale,
                ProductSold = products
            };

            context.Sales.Add(sale);
            await context.SaveChangesAsync(token).ConfigureAwait(false);
            return new AddSalesPayload(sale);
        }
    }
}
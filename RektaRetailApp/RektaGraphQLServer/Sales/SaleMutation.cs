using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using RektaGraphQLServer.Common;
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
            var products = new List<ProductForSale>();

            foreach (var productId in input.ProductIds)
            {
                products.Add(new ProductForSale{ Id = productId });
            }

            var sale = new Sale
            {
                SaleDate = input.SaleDate,
                Total = input.GrandTotal,
                SubTotal = input.SubTotal,
                CustomerName = input.CustomerName,
                CustomerPhoneNumber = input.CustomerPhoneNumber,
                SalesPersonId = input.SalesPersonId,
                ModeOfPayment = input.TypeOfPayment,
                TypeOfSale = input.TypeOfSale,
                //ProductsForSale = products
            };

            context.Sales.Add(sale);
            await context.SaveChangesAsync(token).ConfigureAwait(false);
            return new AddSalesPayload(sale);
        }
    }
}
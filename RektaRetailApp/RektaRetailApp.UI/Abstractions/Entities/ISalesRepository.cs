﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.ApiModel.Sales;
using RektaRetailApp.UI.Commands.Sales;
using RektaRetailApp.UI.Helpers;
using RektaRetailApp.UI.Queries.Sales;

namespace RektaRetailApp.UI.Abstractions.Entities
{
    public interface ISalesRepository : IRepository
    {
        Task CreateSale(CreateSaleCommand command, CancellationToken token);

        Task UpdateSale(UpdateSaleCommand command, CancellationToken token);

        Task CancelASale(CancelSaleCommand command, CancellationToken token);

        Task<PagedList<Sale>> GetAllSales(GetAllSalesQuery query, CancellationToken token);

        Task<Sale> GetSaleById(GetSaleByIdQuery query, CancellationToken token);

        Task SaveAsync(CancellationToken token);
    }
}

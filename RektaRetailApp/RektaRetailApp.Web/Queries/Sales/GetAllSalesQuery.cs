using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Sales;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Queries.Sales
{
    public class GetAllSalesQuery : IRequest<PaginatedResponse<SaleViewModel>>, IRequest<Response<SaleDetailViewModel>>
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string? SearchTerm { get; set; }

        /// <summary>
        /// Default OrderBy term is "Date" unless otherwise stated
        /// </summary>
        public string? OrderByTerm { get; set; } = "Date";
    }



    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, PaginatedResponse<SaleViewModel>>
    {
        private readonly ISalesRepository _repo;

        public GetAllSalesQueryHandler(ISalesRepository repo)
        {
            _repo = repo;
        }
        public async Task<PaginatedResponse<SaleViewModel>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sales = await _repo.GetAllSales(request, cancellationToken).ConfigureAwait(false);

                var salesViewModel = new List<SaleViewModel>();
                var items = new List<ItemSoldViewModel>();
                foreach (var sale in sales)
                {
                    var saleViewModel = new SaleViewModel
                    {
                        GrandTotal = sale.Total,
                        Id = sale.Id,
                        SalesPerson = sale.SalesPersonId,
                        SaleDate = sale.SaleDate,
                        TypeOfPayment = sale.ModeOfPayment,
                        TypeOfSale = sale.TypeOfSale
                    };

                    List<ProductForSale>? productsForSale = sale?.OrderCart?.OrderedItems;
                    for (int i = 0; i < productsForSale?.Count; i++)
                    {
                        ProductForSale? product = productsForSale[i];
                        var item = new ItemSoldViewModel
                        {
                            Id = product.Id,
                            ItemName = product.ProductName,
                            Price = product.Price,
                            Quantity = product.Quantity
                        };
                        items.Add(item);
                    }
                    saleViewModel.ProductsBought.AddRange(items);
                    salesViewModel.Add(saleViewModel);
                }

                

                var result = new PaginatedResponse<SaleViewModel>(salesViewModel,
                    sales.TotalCount, sales.PageSize, sales.CurrentPage,"","", ResponseStatus.Success);

                return result;
            }
            catch (Exception e)
            {
                return new PaginatedResponse<SaleViewModel>(
                    new PagedList<SaleViewModel>(),
                    0, 10, 1, "", "", 
                    ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }

}

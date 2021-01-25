using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.UI.Abstractions;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Sales;
using RektaRetailApp.UI.Helpers;

namespace RektaRetailApp.UI.Queries.Sales
{
        public class GetAllSalesQuery : IRequest<PaginatedResponse<SaleApiModel>>, IRequest<Response<SaleDetailApiModel>>
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string? SearchTerm { get; set; }
        
        /// <summary>
        /// Default OrderBy term is "Date" unless otherwise stated
        /// </summary>
        public string? OrderByTerm { get; set; } = "Date";
    }



    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery,PaginatedResponse<SaleApiModel>>
    {
        private readonly ISalesRepository _repo;
        private readonly IUriGenerator _uriGen;

        public GetAllSalesQueryHandler(ISalesRepository repo, IUriGenerator uriGen)
        {
            _repo = repo;
            _uriGen = uriGen;
        }
        public async Task<PaginatedResponse<SaleApiModel>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var temp = _uriGen.BaseUri;
                var finalUri = $"{temp}/api/sales";
                var sales = await _repo.GetAllSales(request, cancellationToken).ConfigureAwait(false);
                
                var prev = _uriGen
                    .AddQueryStringParams(finalUri, "pageNumber", (request.PageNumber - 1).ToString()!);
                prev.AddQueryStringParams(finalUri,"pageSize", request.PageSize.ToString()!);
                
                var nextL = _uriGen.AddQueryStringParams(finalUri,"pageNumber", (request.PageNumber + 1).ToString()!);
                nextL.AddQueryStringParams(finalUri,"pageSize", request.PageSize.ToString()!);
                
                var prevLink = sales.HasPrevious
                    ? prev.GenerateUri() : null;
                var nextLink = sales.HasNext
                    ? nextL.GenerateUri() : null;
                var salesApiModel = new List<SaleApiModel>();
                var items = new List<ItemSoldApiModel>();
                for (var i = 0; i < sales.Count; i++)
                {
                    var apiModel = new SaleApiModel
                    {
                        GrandTotal = sales[i].GrandTotal,
                        Id = sales[i].Id,
                        SalesPerson = sales[i].SalesPersonId,
                        SaleDate = sales[i].SaleDate,
                        TypeOfPayment = sales[i].ModeOfPayment,
                        TypeOfSale = sales[i].TypeOfSale
                    };

                    for (var j = 0; j < sales[i].ProductSold.Count; j++)
                    {
                        var item = new ItemSoldApiModel
                        {
                            Id = sales[i].ProductSold[j].Id,
                            ItemName = sales[i].ProductSold[j].Name,
                            Price = sales[i].ProductSold[j].Price.RetailPrice,
                            Quantity = sales[i].ProductSold[j].Quantity
                        };
                        items.Add(item);
                    }
                    apiModel.ProductsBought.AddRange(items);
                    salesApiModel.Add(apiModel);
                }
                
                var result = new PaginatedResponse<SaleApiModel>(salesApiModel,
                    sales.TotalCount, sales.PageSize, sales.CurrentPage,
                    prevLink?.PathAndQuery, nextLink?.PathAndQuery, ResponseStatus.Success);

                return result;
            }
            catch (Exception e)
            {
                return new PaginatedResponse<SaleApiModel>(new PagedList<SaleApiModel>(), 0, 10, 1, "","",ResponseStatus.Error, new { ErrorMessage = e.Message});
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Product;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Queries.Product
{
    public class GetAllProductsQuery : IRequest<PaginatedResponse<ProductViewModel>>
    {
        public string SearchTerm { get; set; } = string.Empty;

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string? Uri { get; set; }

        public string? OrderBy { get; set; }
    }


    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResponse<ProductViewModel>>
    {
        private readonly IProductRepository _repo;
        private readonly IUriGenerator _uriGen;

        public GetAllProductsQueryHandler(IProductRepository repo, IUriGenerator uriGen)
        {
            _repo = repo;
            _uriGen = uriGen;
        }
        public async Task<PaginatedResponse<ProductViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var products = await _repo.GetAllProducts(request, cancellationToken).ConfigureAwait(false);

                var temp = _uriGen.BaseUri;
                var finalUri = $"{temp}/api/products";

                var prev = _uriGen.AddQueryStringParams(finalUri, "pageNumber", (request.PageNumber - 1).ToString()!);
                prev.AddQueryStringParams(finalUri, "pageSize", request.PageSize.ToString()!);

                var nextL = _uriGen.AddQueryStringParams(finalUri, "pageNumber", (request.PageNumber + 1).ToString()!);
                nextL.AddQueryStringParams(finalUri, "pageSize", request.PageSize.ToString()!);

                var prevLink = products.HasPrevious
                    ? prev.GenerateUri() : null;
                var nextLink = products.HasNext
                    ? nextL.GenerateUri() : null;

                var projectedProducts = new List<ProductViewModel>();

                for (var i = 0; i < products.Count; i++)
                {
                    var projected = new ProductViewModel(
                        products[i].Name, products[i].SupplierId, products[i].Quantity, products[i].Price!.CostPrice,
                        products[i].Price!.UnitPrice, products[i].Price!.RetailPrice, products[i].Id);
                    projectedProducts.Add(projected);
                }

                var result = new PaginatedResponse<ProductViewModel>(projectedProducts,
                    products.TotalCount, products.PageSize, products.CurrentPage,
                    prevLink?.PathAndQuery, nextLink?.PathAndQuery, ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new PaginatedResponse<ProductViewModel>(new PagedList<ProductViewModel>(),
                    0, 10, 1, "", "",
                    ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }
}

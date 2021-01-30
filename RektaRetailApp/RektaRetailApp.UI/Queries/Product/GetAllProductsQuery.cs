using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MediatR;
using RektaRetailApp.UI.Abstractions;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Product;
using RektaRetailApp.UI.Helpers;

namespace RektaRetailApp.UI.Queries.Product
{
    public class GetAllProductsQuery : IRequest<PaginatedResponse<ProductApiModel>>
    {
        public string SearchTerm { get; set; } = string.Empty;

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string? Uri { get; set; }

        public string? OrderBy { get; set; }
    }


    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResponse<ProductApiModel>>
    {
        private readonly IProductRepository _repo;
        private readonly IUriGenerator _uriGen;

        public GetAllProductsQueryHandler(IProductRepository repo, IUriGenerator uriGen)
        {
            _repo = repo;
            _uriGen = uriGen;
        }
        public async Task<PaginatedResponse<ProductApiModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
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

                var projectedProducts = new List<ProductApiModel>();

                for (var i = 0; i < products.Count; i++)
                {
                    var projected = new ProductApiModel(
                        products[i].Name, products[i].SupplierId, products[i].Quantity, products[i].Price!.CostPrice,
                        products[i].Price!.UnitPrice, products[i].Price!.RetailPrice, products[i].Id);
                    projectedProducts.Add(projected);
                }

                var result = new PaginatedResponse<ProductApiModel>(projectedProducts,
                    products.TotalCount, products.PageSize, products.CurrentPage,
                    prevLink?.PathAndQuery, nextLink?.PathAndQuery, ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new PaginatedResponse<ProductApiModel>(new PagedList<ProductApiModel>(),
                    0, 10, 1, "", "",
                    ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }
}

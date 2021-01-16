using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Product;

namespace RektaRetailApp.UI.Queries.Product
{
public class ProductDetailQuery : IRequest<ApiModel.Response<ProductDetailApiModel>>
    {
        public int Id { get; set; }
    }


    public class ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, ApiModel.Response<ProductDetailApiModel>>
    {
        private readonly IProductRepository _repo;

        public ProductDetailQueryHandler(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<ApiModel.Response<ProductDetailApiModel>> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
        {
            var includes = new Expression<Func<Domain.DomainModels.Product, object>>[]
            {
                p => p.ProductCategories,
                p => p.Supplier!
            };
            var product = await _repo.GetProductByAsync(cancellationToken, includes, p => p.Id == request.Id)
                .ConfigureAwait(false);
            //var data = _mapper.Map<Domain.DomainModels.Product, ProductDetailApiModel>(product);
            var data = new ProductDetailApiModel(retailPrice: product.RetailPrice, name: product.Name, quantity:product.Quantity,
                suppliedPrice: product.CostPrice,supplyDate:product.SupplyDate, id: product.Id, unitPrice: product.UnitPrice,
                supplierName: product.Supplier?.Name, imageUrl: product.ImageUrl, mobileNumber: product.Supplier?.MobileNumber);
            var result = new ApiModel.Response<ProductDetailApiModel>(data, ResponseStatus.Success);
            return result;
        }
    }
}

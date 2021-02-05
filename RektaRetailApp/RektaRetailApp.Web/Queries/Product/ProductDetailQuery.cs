using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Product;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Queries.Product
{
    public class ProductDetailQuery : IRequest<Response<ProductDetailViewModel>>
    {
        public int Id { get; set; }
    }


    public class ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, Response<ProductDetailViewModel>>
    {
        private readonly IProductRepository _repo;

        public ProductDetailQueryHandler(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<Response<ProductDetailViewModel>> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
        {
            var includes = new Expression<Func<Domain.DomainModels.Product, object>>[]
            {
                p => p.ProductCategories!,
                p => p.Supplier!,
                p => p.Price!
            };
            var product = await _repo.GetProductByAsync(cancellationToken, includes, p => p.Id == request.Id)
                .ConfigureAwait(false);
            //var data = _mapper.Map<Domain.DomainModels.Product, ProductDetailViewModel>(product);
            var data = new ProductDetailViewModel(retailPrice: product.Price!.RetailPrice, name: product.Name, quantity: product.Quantity,
                suppliedPrice: product.Price.CostPrice, supplyDate: product.SupplyDate, id: product.Id, unitPrice: product.Price.UnitPrice,
                supplierName: product.Supplier?.Name, imageUrl: product.ImageUrl, mobileNumber: product.Supplier?.MobileNumber);
            var result = new Response<ProductDetailViewModel>(data, ResponseStatus.Success);
            return result;
        }
    }
}

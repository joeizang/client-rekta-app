using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Product;
using RektaRetailApp.UI.Data;

namespace RektaRetailApp.UI.Queries.Product
{
    public class GetProductsForSaleQuery : IRequest<ApiModel.Response<IEnumerable<ProductsForSaleApiModel>>>
    {
    }


    public class GetProductsForSaleQueryHandler : IRequestHandler<GetProductsForSaleQuery, ApiModel.Response<IEnumerable<ProductsForSaleApiModel>>>
    {
        private readonly RektaContext _db;

        public GetProductsForSaleQueryHandler(RektaContext db)
        {
            _db = db;
        }
        public async Task<ApiModel.Response<IEnumerable<ProductsForSaleApiModel>>> Handle(GetProductsForSaleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var work = await _db.Products.AsNoTracking()
                    .Select(p => new ProductsForSaleApiModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.RetailPrice
                    }).ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
                var result = new ApiModel.Response<IEnumerable<ProductsForSaleApiModel>>(work, ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new ApiModel.Response<IEnumerable<ProductsForSaleApiModel>>(
                    new List<ProductsForSaleApiModel>(), ResponseStatus.Error, new { ErrorMessage = e.Message} );
            }
        }
    }

}

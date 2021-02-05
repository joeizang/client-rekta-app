using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Product;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Queries.Product
{
    public class GetProductsForSaleQuery : IRequest<Response<IEnumerable<ProductsForSaleViewModel>>>
    {
    }


    public class GetProductsForSaleQueryHandler : IRequestHandler<GetProductsForSaleQuery, Response<IEnumerable<ProductsForSaleViewModel>>>
    {
        private readonly RektaContext _db;

        public GetProductsForSaleQueryHandler(RektaContext db)
        {
            _db = db;
        }
        public async Task<Response<IEnumerable<ProductsForSaleViewModel>>> Handle(GetProductsForSaleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var work = await _db.Products.AsNoTracking()
                    .Select(p => new ProductsForSaleViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price!.RetailPrice
                    }).ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
                var result = new Response<IEnumerable<ProductsForSaleViewModel>>(work, ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ProductsForSaleViewModel>>(
                    new List<ProductsForSaleViewModel>(), ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }

}

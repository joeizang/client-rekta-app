using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Sales;

namespace RektaRetailApp.UI.Queries.Sales
{
    public class GetSaleByIdQuery : IRequest<Response<SaleDetailApiModel>>
    {
        public int Id { get; set; }
    }


    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Response<SaleDetailApiModel>>
    {
        private readonly ISalesRepository _repo;

        public GetSaleByIdQueryHandler(ISalesRepository repo)
        {
            _repo = repo;
        }
        public async Task<Response<SaleDetailApiModel>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _repo.GetSaleById(request, cancellationToken).ConfigureAwait(false);
                var result = new Response<SaleDetailApiModel>(
                    new SaleDetailApiModel(sale.Id, sale.SalesPersonId, sale.SaleDate, sale.TypeOfSale!, sale.ModeOfPayment!),
                    ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new Response<SaleDetailApiModel>(new SaleDetailApiModel(), ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }

}

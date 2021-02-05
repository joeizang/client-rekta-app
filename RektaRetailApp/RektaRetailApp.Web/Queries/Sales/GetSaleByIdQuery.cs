using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Sales;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Queries.Sales
{
    public class GetSaleByIdQuery : IRequest<Response<SaleDetailViewModel>>
    {
        public int Id { get; set; }
    }


    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Response<SaleDetailViewModel>>
    {
        private readonly ISalesRepository _repo;

        public GetSaleByIdQueryHandler(ISalesRepository repo)
        {
            _repo = repo;
        }
        public async Task<Response<SaleDetailViewModel>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _repo.GetSaleById(request, cancellationToken).ConfigureAwait(false);
                var result = new Response<SaleDetailViewModel>(
                    new SaleDetailViewModel(sale.Id, sale.SalesPersonId, sale.SaleDate, sale.TypeOfSale!, sale.ModeOfPayment!),
                    ResponseStatus.Success);
                return result;
            }
            catch (Exception e)
            {
                return new Response<SaleDetailViewModel>(new SaleDetailViewModel(), ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }

}

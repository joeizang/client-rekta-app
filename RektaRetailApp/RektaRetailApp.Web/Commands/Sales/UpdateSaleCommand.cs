using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Sales;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Commands.Sales
{
    public class UpdateSaleCommand : IRequest<Response<SaleDetailViewModel>>
    {
    }



    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Response<SaleDetailViewModel>>
    {
        public Task<Response<SaleDetailViewModel>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Sales;

namespace RektaRetailApp.UI.Commands.Sales
{
    public class UpdateSaleCommand : IRequest<Response<SaleDetailApiModel>>
    {
    }



    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Response<SaleDetailApiModel>>
    {
        public Task<Response<SaleDetailApiModel>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

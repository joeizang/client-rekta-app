using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RektaRetailApp.Web.Commands.Sales
{
    public class CancelSaleCommand : IRequest
    {
    }



    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, Unit>
    {
        public Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

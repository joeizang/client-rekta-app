using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;

namespace RektaRetailApp.Web.Commands.Supplier
{
    public class DeleteSupplierCommand : IRequest
    {
        public int Id { get; set; }
    }


    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand,Unit>
    {
        private readonly ISupplierRepository _repo;

        public DeleteSupplierCommandHandler(ISupplierRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteSupplier(request.Id, cancellationToken).ConfigureAwait(false);
            await _repo.SaveAsync(cancellationToken);
            return default;
        }
    }
}

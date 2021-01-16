using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.UI.Abstractions.Entities;

namespace RektaRetailApp.UI.Commands.Inventory
{
    public class DeleteInventoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, Unit>
    {
        private readonly IInventoryRepository _repo;

        public DeleteInventoryCommandHandler(IInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<Unit> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteInventory(request);
            return default;
        }
    }
}

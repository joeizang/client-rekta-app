using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel.Inventory;

namespace RektaRetailApp.Web.Queries.Inventory
{
    public class GetInventoryDetailQuery : IRequest<InventoryDetailViewModel>
    {
        public int Id { get; set; }
    }

    public class GetInventoryDetailQueryHandler : IRequestHandler<GetInventoryDetailQuery, InventoryDetailViewModel>
    {
        private readonly IInventoryRepository _repo;

        public GetInventoryDetailQueryHandler(IInventoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<InventoryDetailViewModel> Handle(GetInventoryDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetInventoryById(request.Id).ConfigureAwait(false);
            return result;
        }
    }
}

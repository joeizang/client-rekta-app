using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel.Inventory;

namespace RektaRetailApp.Web.Commands.Inventory
{
  public class CreateInventoryCommand : IRequest<InventoryViewModel>
  {
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? BatchNumber { get; set; }

    public string CategoryName { get; set; } = null!;

    public float ProductQuantity { get; set; }

  }


  public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, InventoryViewModel>
  {
    private readonly IInventoryRepository _repo;

    public CreateInventoryCommandHandler(IInventoryRepository repo)
    {
      _repo = repo;
    }
    public async Task<InventoryViewModel> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
      _repo.CreateInventory(request);
      await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
      var result = await _repo
          .GetInventoryBy(x => x.Name!.Equals(request.Name.ToUpperInvariant()) &&
              x.Description!.Equals(request.Description!.ToUpperInvariant()));
      return result;
    }
  }
}

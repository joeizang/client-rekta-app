using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Inventory;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Queries.Inventory
{
    public class GetAllInventoriesQuery : IRequest<PaginatedResponse<InventoryViewModel>>
    {
        public string? SearchString { get; set; }

        public bool Ascending { get; set; } = true;

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

    }


    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoriesQuery, PaginatedResponse<InventoryViewModel>>
    {
        private readonly IInventoryRepository _repo;
        private readonly IUriGenerator _uriGen;

        public GetAllInventoryQueryHandler(IInventoryRepository repo, IUriGenerator uriGen)
        {
            _repo = repo;
            _uriGen = uriGen;
        }

        public async Task<PaginatedResponse<InventoryViewModel>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.GetAllInventories(request,cancellationToken)
                    .ConfigureAwait(false);
                var temp = _uriGen.BaseUri;
                var finalUri = $"{temp}/api/inventories";
            
                var prev = _uriGen.AddQueryStringParams(finalUri,"pageNumber", (request.PageNumber - 1).ToString()!);
                prev.AddQueryStringParams(finalUri,"pageSize", request.PageSize.ToString()!);
                
                var nextL = _uriGen.AddQueryStringParams(finalUri,"pageNumber", (request.PageNumber + 1).ToString()!);
                nextL.AddQueryStringParams(finalUri,"pageSize", request.PageSize.ToString()!);

                var prevLink = result.HasPrevious
                    ? prev.GenerateUri() : null;
                var nextLink = result.HasNext
                    ? nextL.GenerateUri() : null;

                var newInventories = new List<InventoryViewModel>();

                for (var i = 0; i < result.Count; i++)
                {
                    var invent = new InventoryViewModel
                    {
                        CategoryId = result[i].CategoryId,
                        CategoryName = result[i].Category.Name,
                        Id = result[i].Id,
                        Name = result[i].Name,
                        NumberOfProductsInStock = result[i].InventoryItems.Count,
                        SupplyDate = result[i].SupplyDate
                    };
                    newInventories.Add(invent);
                }

                var processedInventories = new PaginatedResponse<InventoryViewModel>(newInventories, result.TotalCount,
                    result.PageSize, result.CurrentPage, prevLink?.PathAndQuery, nextLink?.PathAndQuery,
                    ResponseStatus.Success);
                return processedInventories;
            }
            catch (Exception e)
            {
                return new PaginatedResponse<InventoryViewModel>(new PagedList<InventoryViewModel>(), 0, 10, 1, "", "",
                ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }
}

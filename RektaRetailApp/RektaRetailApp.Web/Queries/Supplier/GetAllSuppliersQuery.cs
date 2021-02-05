using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Supplier;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Queries.Supplier
{
  public class GetAllSuppliersQuery : IRequest<PaginatedResponse<SupplierViewModel>>
  {
    public string? SearchTerm { get; set; }

    public int? PageSize { get; set; }

    public int? PageNumber { get; set; }

    public string? OrderBy { get; set; }

    public string? UriName { get; set; }
  }

  public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, PaginatedResponse<SupplierViewModel>>
  {
    private readonly ISupplierRepository _repo;
    private readonly IUriGenerator _ugen;

    public GetAllSuppliersQueryHandler(ISupplierRepository repo, IUriGenerator ugen)
    {
      _repo = repo;
      _ugen = ugen;
    }
    public async Task<PaginatedResponse<SupplierViewModel>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
      try
      {
            var pagedResult = await _repo.SuppliersAsync(request, cancellationToken)
                        .ConfigureAwait(false);
            var temp = _ugen.BaseUri;
            var updated = $"{temp}/api/suppliers";

            var prev = _ugen.AddQueryStringParams(updated,"pageNumber", (request.PageNumber - 1).ToString()!);
            prev.AddQueryStringParams(updated,"pageSize", request.PageSize.ToString()!);
            var nextL = _ugen.AddQueryStringParams(updated,"pageNumber", (request.PageNumber + 1).ToString()!);
            nextL.AddQueryStringParams(updated,"pageSize", request.PageSize.ToString()!);

            var prevLink = pagedResult.HasPrevious
                ? prev.GenerateUri() : null;
            var nextLink = pagedResult.HasNext
                ? nextL.GenerateUri() : null;
            var suppliers = new List<SupplierViewModel>();
            for (var i = 0; i < pagedResult.Count; i++)
            {
                var supplier =
                    new SupplierViewModel(pagedResult[i].Name, pagedResult[i].MobileNumber, pagedResult[i].Id);
                suppliers.Add(supplier);
            }
            var result = new PaginatedResponse<SupplierViewModel>(suppliers,
                pagedResult.TotalCount, pagedResult.PageSize, pagedResult.CurrentPage,
                prevLink?.AbsolutePath, nextLink?.AbsolutePath, ResponseStatus.Success);
            return result;
      }
      catch (System.Exception e)
      {
          return new PaginatedResponse<SupplierViewModel>(
              new PagedList<SupplierViewModel>(),ResponseStatus.Error, new { ErrorMessage = e.Message});
      }
    }
  }
}

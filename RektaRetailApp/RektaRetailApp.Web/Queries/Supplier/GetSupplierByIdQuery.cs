using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Supplier;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Queries.Supplier
{
    public class GetSupplierByIdQuery : IRequest<Response<SupplierDetailViewModel>>
    {
        public int Id { get; set; }
    }


    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, Response<SupplierDetailViewModel>>
    {
        private readonly ISupplierRepository _repo;
        private readonly IMapper _mapper;

        public GetSupplierByIdQueryHandler(ISupplierRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Response<SupplierDetailViewModel>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _repo.SupplierById(request.Id, cancellationToken).ConfigureAwait(false);
            var model = _mapper.Map<SupplierDetailViewModel>(supplier);
            var result = new Response<SupplierDetailViewModel>(model, ResponseStatus.Success);
            return result;
        }
    }
}

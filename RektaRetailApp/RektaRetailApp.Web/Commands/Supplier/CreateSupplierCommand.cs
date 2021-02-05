using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.DomainEvents.Supplier;
using RektaRetailApp.Web.ViewModel.Supplier;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Commands.Supplier
{
    public class CreateSupplierCommand : IRequest<Response<SupplierViewModel>>
    {
        public string Name { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        [CanBeNull] public string? Description { get; set; }
    }


    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Response<SupplierViewModel>>
    {
        private readonly ISupplierRepository _repo;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateSupplierCommandHandler(ISupplierRepository repo, IMediator mediator, IMapper mapper)
        {
            _repo = repo;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<Response<SupplierViewModel>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            _repo.CreateSupplier(request);
            await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
            var supplier = await _repo
                .SupplierBy(cancellationToken,null,s => s.MobileNumber!.Equals(request.PhoneNumber),
                    s => s.Name!.Equals(request.Name!.Trim().ToUpperInvariant())).ConfigureAwait(false);
            var model = _mapper.Map<SupplierViewModel>(supplier);

            var result = new Response<SupplierViewModel>(model, ResponseStatus.Success);

            await _mediator.Publish(new SupplierCreatedEvent(model?? 
                                                             new SupplierViewModel(string.Empty,string.Empty,int.MinValue)),
                                                             cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}

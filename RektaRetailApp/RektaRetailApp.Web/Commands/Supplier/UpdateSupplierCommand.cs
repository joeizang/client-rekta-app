using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.DomainEvents.Supplier;
using RektaRetailApp.Web.ViewModel.Supplier;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Commands.Supplier
{
    public class UpdateSupplierCommand : IRequest<Response<SupplierDetailViewModel>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? MobileNumber { get; set; }

        public string? Description { get; set; }
    }


    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Response<SupplierDetailViewModel>>
    {
        private readonly ISupplierRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateSupplierCommandHandler(ISupplierRepository repo,IMapper mapper, IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Response<SupplierDetailViewModel>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _repo.UpdateSupplier(request);
                await _repo.SaveAsync(cancellationToken);
                var temp = await _repo.SupplierById(request.Id, cancellationToken);
                var target = _mapper.Map<SupplierDetailViewModel>(temp);
                var supplierUpdated = new Response<SupplierDetailViewModel>(target, ResponseStatus.Success);
                await _mediator.Publish(new SupplierUpdatedEvent(request.Name, request.MobileNumber, request.Description),cancellationToken);
                return supplierUpdated;
            }
            catch (System.Exception e)
            {
                return new Response<SupplierDetailViewModel>(ResponseStatus.Error, new { ErrorMessage = e.Message });
            }
        }
    }
}

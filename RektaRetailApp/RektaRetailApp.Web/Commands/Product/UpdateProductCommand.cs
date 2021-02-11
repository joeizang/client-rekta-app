using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Product;
using RektaRetailApp.Web.DomainEvents.Product;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Commands.Product
{
    public class UpdateProductCommand : IRequest<Response<ProductDetailViewModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public decimal RetailPrice { get; set; }

        public decimal UnitPrice { get; set; }

        public float ReorderPoint { get; set; }

        public decimal CostPrice { get; set; }

        public DateTimeOffset SupplyDate { get; set; }

        public float Quantity { get; set; }

        public string? Brand { get; set; }

        public string? Comments { get; set; }

        public UnitMeasure? UnitMeasure { get; set; }

        public bool Verified { get; set; }

    }



    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<ProductDetailViewModel>>
    {
        private readonly IProductRepository _repo;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repo, IMediator mediator, IMapper mapper)
        {
            _repo = repo;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<Response<ProductDetailViewModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _repo.UpdateProductAsync(request, cancellationToken).ConfigureAwait(false);
            await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);

            var model = _mapper.Map<ProductDetailViewModel>(await _repo.GetProductByIdAsync(request.Id, cancellationToken));

            var result = new Response<ProductDetailViewModel>(model, ResponseStatus.Success);

            var updateEvent = new ProductUpdateEvent(model);

            await _mediator.Publish(updateEvent, cancellationToken);

            return result;
        }
    }
}

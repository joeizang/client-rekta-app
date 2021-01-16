using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Product;

using RektaRetailApp.UI.DomainEvents.Product;


namespace RektaRetailApp.UI.Commands.Product
{
    public class CreateProductCommand : IRequest<Response<ProductDetailApiModel>>
    {
         public string Name { get; set; } = null!;

        public decimal RetailPrice { get; set; }

        public decimal UnitPrice { get; set; }

        public float Quantity { get; set; }

        public decimal CostPrice { get; set; }

        public string? Comments { get; set; }

        public string? Brand { get; set; }

        public string? ImageUrl { get; set; }
        public DateTimeOffset SupplyDate { get; set; }

        public int InventoryId { get; set; }

        public UnitMeasure UnitMeasure { get; set; }

        public bool Verified { get; set; }

        public string? CategoryName { get; set; }

        public int SupplierId { get; set; }

    }


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductDetailApiModel>>
    {
        private readonly IProductRepository _repo;
        private readonly IMediator _mediator;

        public CreateProductCommandHandler(IProductRepository repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }
        public async Task<Response<ProductDetailApiModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var includes = new Expression<Func<Domain.DomainModels.Product, object>>[]
                {
                    p => p.ProductCategories,
                    p => p.Supplier!
                };
                await _repo.CreateProductAsync(request, cancellationToken).ConfigureAwait(false);

                await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);

                var product = await _repo.GetProductByAsync(cancellationToken, includes, p => p.Name.Equals(request.Name.ToUpperInvariant()),
                    p => p.RetailPrice == request.RetailPrice, p => p.CostPrice == request.CostPrice);

                var model = new ProductDetailApiModel(product.RetailPrice, product.UnitPrice, product.Name, product.Quantity,
                    product.CostPrice, product.Supplier?.Name, product.Supplier?.MobileNumber,
                    product.ImageUrl,  product.SupplyDate, product.Id);
                var result = new Response<ProductDetailApiModel>(model, ResponseStatus.Success);
                var createEvent = new ProductCreateEvent(model);
                await _mediator.Publish(createEvent, cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                return new Response<ProductDetailApiModel>(new ProductDetailApiModel(), ResponseStatus.Failure, new
                {
                    e.Message,
                    Time = DateTimeOffset.Now
                });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Sales;
using RektaRetailApp.UI.DomainEvents.Sales;

namespace RektaRetailApp.UI.Commands.Sales
{
    public class CreateSaleCommand : IRequest<Response<SaleApiModel>>
    {
        public DateTimeOffset SaleDate { get; set; }

        public List<ItemSoldApiModel> ProductsSold { get; set; } = new List<ItemSoldApiModel>();

        public string? SalesPerson { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GrandTotal { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SaleType SaleType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentType PaymentType { get; set; }
    }



    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Response<SaleApiModel>>
    {
        private readonly ISalesRepository _repo;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateSaleCommandHandler(ISalesRepository repo, IMediator mediator, IMapper mapper)
        {
            _repo = repo;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<Response<SaleApiModel>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var include = new Expression<Func<Sale, object>>[]
                {
                    x => x.ProductSold
                };
                await _repo.CreateSale(request, cancellationToken).ConfigureAwait(false);
                await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
                var thisSale = await _repo.GetOneBy(cancellationToken,include, s => s.SaleDate.Equals(request.SaleDate),
                    s => s.GrandTotal.Equals(request.GrandTotal), s => s.SubTotal.Equals(request.SubTotal)).ConfigureAwait(false);
                var sale = _mapper.Map<SaleApiModel>(thisSale);
                var result = new Response<SaleApiModel>(sale, ResponseStatus.Success);
                var createSaleEvent = new SaleCreateEvent(sale);
                await _mediator.Publish(createSaleEvent, cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                return new Response<SaleApiModel>(new SaleApiModel(), ResponseStatus.Failure, new
                {
                    e.Message,
                    Time = DateTimeOffset.Now
                });
            }
        }
    }

}

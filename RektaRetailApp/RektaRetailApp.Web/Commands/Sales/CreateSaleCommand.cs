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
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Sales;
using RektaRetailApp.Web.DomainEvents.Sales;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Commands.Sales
{
    public class CreateSaleCommand : IRequest<Response<SaleViewModel>>
    {
        public DateTimeOffset SaleDate { get; set; }

        public List<ItemSoldViewModel> ProductsSold { get; set; } = new List<ItemSoldViewModel>();

        public string? SalesPerson { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GrandTotal { get; set; }

        public SaleType? SaleType { get; set; }

        public PaymentType? PaymentType { get; set; }
    }



    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Response<SaleViewModel>>
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
        public async Task<Response<SaleViewModel>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var include = new Expression<Func<Sale, object>>[]
                {
                    x => x.OrderCart!
                };
                await _repo.CreateSale(request, cancellationToken).ConfigureAwait(false);
                await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
                var thisSale = await _repo.GetOneBy(cancellationToken, include, s => s.SaleDate.Equals(request.SaleDate),
                    s => s.Total.Equals(request.GrandTotal), s => s.SubTotal.Equals(request.SubTotal))
                    .ConfigureAwait(false);
                var sale = _mapper.Map<SaleViewModel>(thisSale);
                var result = new Response<SaleViewModel>(sale, ResponseStatus.Success);
                var createSaleEvent = new SaleCreateEvent(sale);
                await _mediator.Publish(createSaleEvent, cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                return new Response<SaleViewModel>(new SaleViewModel(), ResponseStatus.Failure, new
                {
                    e.Message,
                    Time = DateTimeOffset.Now
                });
            }
        }
    }

}

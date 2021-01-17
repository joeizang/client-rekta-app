using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Sales;
using RektaRetailApp.UI.Commands.Sales;
using RektaRetailApp.UI.Queries.Sales;

namespace RektaRetailApp.UI.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<SalesController>
        [HttpGet(Name="GetAllSales")]
        public async Task<ActionResult<PaginatedResponse<SaleApiModel>>> Get([FromQuery]GetAllSalesQuery query,
            CancellationToken token)
        {
            var result = await _mediator.Send(query, token).ConfigureAwait(false);
            return Ok(result);
        }

        // GET api/<SalesController>/5
        [HttpGet("{id}", Name = "GetSaleById")]
        public async Task<ActionResult<Response<SaleDetailApiModel>>> Get([FromQuery]GetSaleByIdQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query, token).ConfigureAwait(false);
            if (result.CurrentResponseStatus.Equals(ResponseStatus.Error))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST api/<SalesController>
        [HttpPost]
        public async Task<ActionResult<Response<SaleApiModel>>> Post([FromBody] CreateSaleCommand command, CancellationToken token)
        {
            var result = await _mediator.Send(command, token).ConfigureAwait(false);
            return CreatedAtRoute("GetSaleById", new { result.Data.Id }, result);
        }

        // PUT api/<SalesController>/5
        //[HttpPut("{id}")]
        //public void Put([FromBody] UpdateSaleCommand command)
        //{
        //}

        //// DELETE api/<SalesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
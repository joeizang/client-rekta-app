using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RektaRetailApp.Web.ViewModel;
using RektaRetailApp.Web.ViewModel.Product;
using RektaRetailApp.Web.Commands.Product;
using RektaRetailApp.Web.Queries.Product;
using RektaRetailApp.Web.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RektaRetailApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProductsController>
        [HttpGet(Name = "GetAllProducts")]
        public async Task<ActionResult<PaginatedResponse<ProductViewModel>>> GetAllProducts([FromQuery] GetAllProductsQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query, token)
                .ConfigureAwait(false);
            return Ok(result);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Response<ProductDetailViewModel>>> GetProductById(int id)
        {
            var result = await _mediator.Send(new ProductDetailQuery{ Id = id });
            return Ok(result);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<ActionResult<Response<ProductDetailViewModel>>> CreateProduct(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetProductById", new { id = result.Data.Id}, result);
        }

        //// PUT api/<ProductsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

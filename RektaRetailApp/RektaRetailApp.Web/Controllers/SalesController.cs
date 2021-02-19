using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RektaRetailApp.Web.ViewModel.Sales;
using RektaRetailApp.Web.Commands.Sales;
using RektaRetailApp.Web.Queries.Sales;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.Controllers
{

    public class SalesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SalesController> _logger;

        public SalesController(IMediator mediator, ILogger<SalesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        // GET: Sales
        
        public async Task<IActionResult> Index(GetAllSalesQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query, token).ConfigureAwait(false);
            return View(result);
        }

        // GET: Sales/5
        
        public async Task<IActionResult> Details(GetSaleByIdQuery query, CancellationToken token)
        {
            var result = await _mediator.Send(query, token).ConfigureAwait(false);
            if (result.CurrentResponseStatus.Equals(ResponseStatus.Error))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //GET: Sales/MakeASale
        public IActionResult MakeASale()
        {
            var saleViewModel = new MakeASaleViewModel();
            return View(saleViewModel);
        }


        public IActionResult ItemToBeSold()
        {

            return PartialView("_ItemToBeSold");
        }

        // POST: Sales/MakeASale
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MakeASale(CreateSaleCommand command, CancellationToken token)
        {
            var result = await _mediator.Send(command, token)
                .ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
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
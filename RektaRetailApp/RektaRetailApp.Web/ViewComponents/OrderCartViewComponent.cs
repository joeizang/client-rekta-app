using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ViewModels.OrderCart;

namespace RektaRetailApp.Web.ViewComponents
{
    public class OrderCart : ViewComponent
    {
        private readonly RektaContext _context;

        public OrderCart(RektaContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            //var r = await GetOrderCartList(1).
            return View();
        }

        //private async Task<IEnumerable<OrderCartViewModel>> GetOrderCartList(int x, CancellationToken token = default)
        //{
        //    var result = await _context.OrderCarts.AsNoTracking()
        //        .Where(o => o.Id == x)
                
        //        .ToArrayAsync(token)
        //        .ConfigureAwait(false);
        //    return result;
        //}
    }
}

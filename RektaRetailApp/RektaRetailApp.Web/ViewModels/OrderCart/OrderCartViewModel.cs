using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.ViewModels.OrderCart
{
    public class OrderCartViewModel
    {
        public string? ItemName { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public string OrderCartSessionId { get; set; }

        public OrderCartViewModel()
        {
            OrderCartSessionId = $"{Guid.NewGuid():D}-{DateTimeOffset.Now.Ticks:D}";
        }
    }
}

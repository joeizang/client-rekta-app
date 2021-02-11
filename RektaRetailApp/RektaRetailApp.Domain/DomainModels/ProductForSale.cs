using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class ProductForSale : BaseDomainModel
    {
        public string ProductName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

    }
}

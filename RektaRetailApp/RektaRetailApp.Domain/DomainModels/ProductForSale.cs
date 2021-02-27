using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class ProductForSale : BaseDomainModel
    {
        [StringLength(100)]
        public string ProductName { get; set; } = null!;

        [StringLength(400)]
        public string? ImageUrl { get; set; }

        public double Quantity { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }

    }
}

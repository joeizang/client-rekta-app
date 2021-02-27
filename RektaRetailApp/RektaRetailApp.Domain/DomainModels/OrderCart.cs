 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class OrderCart : BaseDomainModel
    {
        public OrderCart()
        {
            OrderedItems = new List<ProductForSale>();
        }
        public List<ProductForSale> OrderedItems { get; set; }

        public ApplicationUser SalesStaff { get; set; } = default!;

        public string SalesStaffId { get; set; } = null!;

        [Column(TypeName = "decimal(9,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }

        public bool CloseCart { get; set; } = false;

        public string OrderCartSessionId { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; }

    }
}

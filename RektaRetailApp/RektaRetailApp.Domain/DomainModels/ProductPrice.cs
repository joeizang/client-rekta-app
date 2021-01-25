using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class ProductPrice : BaseDomainModel
    {
        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal RetailPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal CostPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal UnitPrice { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
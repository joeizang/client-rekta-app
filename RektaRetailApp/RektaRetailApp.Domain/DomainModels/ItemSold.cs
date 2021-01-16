using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class ItemSold : BaseDomainModel
    {

        [Required]
        public string ItemName { get; set; } = null!;

        [Required]
        public float Quantity { get; set; }

        public string? Comments { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        [Required]
        public decimal Price { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

    }
}

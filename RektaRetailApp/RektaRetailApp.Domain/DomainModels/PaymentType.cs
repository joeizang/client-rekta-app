using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RektaRetailApp.Domain.DomainModels
{
    public class PaymentType
    {
        [Key]
        [Column(TypeName = "text")]
        public string Type { get; set; } = null!;

        [StringLength(300)]
        [Column(TypeName = "text")]
        public string? Description { get; set; }
    }
}
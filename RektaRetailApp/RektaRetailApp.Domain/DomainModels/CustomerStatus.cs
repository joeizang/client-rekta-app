using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RektaRetailApp.Domain.DomainModels
{
    public class CustomerStatus
    {
        [Key]
        [StringLength(20)]
        public string StatusType { get; set; } = null!;

        [StringLength(300)]
        [Column(TypeName = "text")]
        public string? Description { get; set; }
    }
}
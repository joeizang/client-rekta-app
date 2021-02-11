using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RektaRetailApp.Domain.DomainModels
{
    public class UnitMeasure
    {
        [Key]
        [Column(TypeName = "text")]
        public string Type { get; set; } = null!;

        [StringLength(300)]
        [Column(TypeName = "text")]
        public string? Description { get; set; }
    }
}

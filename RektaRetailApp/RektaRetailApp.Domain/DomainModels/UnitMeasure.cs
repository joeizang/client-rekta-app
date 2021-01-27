using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RektaRetailApp.Domain.DomainModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public class UnitMeasure
    {
        [Key]
        [Column(TypeName = "text")]
        public string Type { get; set; }
        
        [StringLength(300)]
        [Column(TypeName = "text")]
        public string? Description { get; set; }
    }
}

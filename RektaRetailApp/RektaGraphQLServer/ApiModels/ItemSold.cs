using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.ApiModels
{
    public class ItemSold
    {
        public string Name { get; set; } = null!;
        
        public DateTimeOffset SupplyDate { get; set; }
        
        public float Quantity { get; set; }

        public string? Brand { get; set; }

        public string? ImageUrl { get; set; } = string.Empty;
        
        public string? Comments { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public UnitMeasure UnitMeasure { get; set; }

        public bool Verified { get; set; }

        public int InventoryId { get; set; }

        public int ProductPriceId { get; set; }

        public int SupplierId { get; set; }
    }
}
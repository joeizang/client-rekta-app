using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RektaRetailApp.Domain.Abstractions;
using Newtonsoft.Json.Converters;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Product : BaseDomainModel
    {
        public Product()
        {
            ProductCategories = new List<ProductCategory>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTimeOffset SupplyDate { get; set; }

        [Required]
        public float Quantity { get; set; }

        public string? Brand { get; set; }

        public string? ImageUrl { get; set; } = string.Empty;
        
        public string? Comments { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public UnitMeasure UnitMeasure { get; set; }

        public bool Verified { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }

        public Inventory? Inventory { get; set; }

        public int InventoryId { get; set; }

        public ProductPrice Price { get; set; }

        public int ProductPriceId { get; set; }

        public int SupplierId { get; set; }
        
        public Supplier Supplier { get; set; }
    }
}

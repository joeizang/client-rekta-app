﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Supplier : BaseDomainModel
    {
        public Supplier()
        {
            ProductsSupplied = new List<Product>();
            ProductInventories = new List<SuppliersInventories>();
        }

        public Supplier(string? name, string? mobileNumber, string? description)
        {
            Name = name;
            MobileNumber = mobileNumber;
            Description = description;
            ProductsSupplied = new List<Product>();
            ProductInventories = new List<SuppliersInventories>();
        }

        [StringLength(50)]
        [Required]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? MobileNumber { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
        
        public List<Product> ProductsSupplied { get; set; }

        public List<SuppliersInventories> ProductInventories { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class Sale : BaseDomainModel
    {
        public Sale()
        {
            ProductSold = new List<Product>();
        }

        public DateTimeOffset SaleDate { get; set; }

        public string SalesPersonId { get; set; } = null!;

        public decimal SubTotal { get; set; }

        public decimal GrandTotal { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public SaleType TypeOfSale { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentType ModeOfPayment { get; set; }

        [StringLength(50)] public string? CustomerName { get; set; }

        [StringLength(50)] public string? CustomerPhoneNumber { get; set; }

        public List<Product> ProductSold { get; set; }
    }
}

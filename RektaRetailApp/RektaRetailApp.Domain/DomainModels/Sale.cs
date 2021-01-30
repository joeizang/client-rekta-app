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

        [ForeignKey(nameof(SalesPerson))]
        public string SalesPersonId { get; set; } = null!;

        public ApplicationUser? SalesPerson { get; set; }

        public decimal SubTotal { get; set; }

        public decimal GrandTotal { get; set; }

        [ForeignKey(nameof(TypeOfSale))]
        public string TypeOfSaleId { get; set; } = null!;

        public SaleType? TypeOfSale { get; set; }

        [ForeignKey(nameof(ModeOfPayment))]
        public string ModeOfPaymentId { get; set; } = null!;

        public PaymentType? ModeOfPayment { get; set; }

        [StringLength(50)]
        [Required]
        public string? CustomerName { get; set; }

        [StringLength(50)] public string? CustomerPhoneNumber { get; set; }

        public List<Product> ProductSold { get; set; }
    }
}

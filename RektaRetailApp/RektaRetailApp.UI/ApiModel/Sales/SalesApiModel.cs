using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.ApiModel.Product;

namespace RektaRetailApp.UI.ApiModel.Sales
{
    public class SaleApiModel
    {
        public DateTimeOffset SaleDate { get; set; }

        public int Id { get; set; }

        public decimal GrandTotal { get; set; }

        public int NumberOfItemsSold { get; set; }

        public string SalesPerson { get; set; } = null!;

        public PaymentType? TypeOfPayment { get; set; }

        public SaleType? TypeOfSale { get; set; }

        public List<ItemSoldApiModel> ProductsBought { get; set; }


        public SaleApiModel()
        {
            ProductsBought = new List<ItemSoldApiModel>();
        }
    }

    public class SaleDetailApiModel
    {
        public SaleDetailApiModel()
        {
            ProductsBought = new List<ItemSoldApiModel>();
        }

        public SaleDetailApiModel(int id, string salesPerson, DateTimeOffset saleDate, SaleType saleType, PaymentType paymentType)
        {
            Id = id;
            SalesPerson = salesPerson;
            SaleDate = saleDate;
            SaleType = saleType;
            PaymentType = paymentType;
            ProductsBought = new List<ItemSoldApiModel>();
        }
        public DateTimeOffset SaleDate { get; set; }

        public int Id { get; set; }

        public string SalesPerson { get; set; } = null!;

        public List<ItemSoldApiModel> ProductsBought { get; set; }

        public SaleType? SaleType { get; set; }

        public PaymentType? PaymentType { get; set; }
    }

    public class ItemSoldApiModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = null!;

        public float Quantity { get; set; }

        public decimal Price { get; set; }

    }
}

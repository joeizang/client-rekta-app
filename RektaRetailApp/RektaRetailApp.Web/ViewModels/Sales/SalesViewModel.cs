using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ViewModel.Product;
using RektaRetailApp.Web.ViewModels.OrderCart;

namespace RektaRetailApp.Web.ViewModel.Sales
{
    public class SaleViewModel
    {
        public DateTimeOffset SaleDate { get; set; }

        public int Id { get; set; }

        public decimal GrandTotal { get; set; }

        public int NumberOfItemsSold { get; set; }

        public string SalesPerson { get; set; } = null!;

        public PaymentType? TypeOfPayment { get; set; }

        public SaleType? TypeOfSale { get; set; }

        public List<ItemSoldViewModel> ProductsBought { get; set; }


        public SaleViewModel()
        {
            ProductsBought = new List<ItemSoldViewModel>();
        }
    }

    public class SaleDetailViewModel
    {
        public SaleDetailViewModel()
        {
            ProductsBought = new List<ItemSoldViewModel>();
        }

        public SaleDetailViewModel(int id, string salesPerson, DateTimeOffset saleDate, SaleType saleType, PaymentType paymentType)
        {
            Id = id;
            SalesPerson = salesPerson;
            SaleDate = saleDate;
            SaleType = saleType;
            PaymentType = paymentType;
            ProductsBought = new List<ItemSoldViewModel>();
        }
        public DateTimeOffset SaleDate { get; set; }

        public int Id { get; set; }

        public string SalesPerson { get; set; } = null!;

        public List<ItemSoldViewModel> ProductsBought { get; set; }

        public SaleType? SaleType { get; set; }

        public PaymentType? PaymentType { get; set; }
    }

    public class ItemSoldViewModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = null!;

        public double Quantity { get; set; }

        public string? ProductImage { get; set; }

        public decimal Price { get; set; }

    }

    public class MakeASaleViewModel
    {
        public List<ItemSoldViewModel> ProductsOnSale { get; set; }

        public double Quantity { get; set; }

        public DateTimeOffset SaleDate { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerPhoneNumber { get; set; }

        public decimal GrandTotal { get; set; }

        public decimal SubTotal { get; set; }

        public string? SaleType { get; set; }

        public List<SelectListItem> SaleTypes { get; set; }

        public OrderCartViewModel? OrderCart { get; set; }

        public MakeASaleViewModel()
        {
            ProductsOnSale = new List<ItemSoldViewModel>();
            SaleTypes = new List<SelectListItem>();
        }
    }
}

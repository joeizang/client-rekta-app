using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.Web.ViewModel.Category;

namespace RektaRetailApp.Web.ViewModel.Product
{
public class ProductViewModel
    {
        public ProductViewModel(string name, int supplierId, float quantity, decimal suppliedPrice, decimal unitPrice, decimal retailPrice, int id)
        {
            Name = name;
            SupplierId = supplierId;
            Quantity = quantity;
            CostPrice = suppliedPrice;
            RetailPrice = retailPrice;
            ProductCategories = new List<CategoryViewModel>();
            Id = id;
        }

        public ProductViewModel()
        {
            ProductCategories = new List<CategoryViewModel>();
        }

        public string Name { get; } = null!;

        public int Id { get; set; }

        public decimal RetailPrice { get; }

        public decimal CostPrice { get; }

        public float Quantity { get; }

        public int SupplierId { get; }

        public List<CategoryViewModel> ProductCategories { get; }
    }

    public class ProductsForSaleViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }
    }

    public class ProductSummaryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public float Quantity { get; set; }
    }

    public class CreateProductViewModel
    {
        public CreateProductViewModel(int supplierId, decimal suppliedPrice, float quantity, decimal unitPrice, decimal retailPrice, DateTimeOffset supplyDate)
        {
            SupplierId = supplierId;
            SuppliedPrice = suppliedPrice;
            Quantity = quantity;
            UnitPrice = unitPrice;
            RetailPrice = retailPrice;
            SupplyDate = supplyDate;
            ProductCategories = new List<CategoryViewModel>();
        }

        public string Name { get; } = null!;

        public decimal RetailPrice { get; }

        public decimal UnitPrice { get; }

        public float Quantity { get; }

        public decimal SuppliedPrice { get; }

        public List<CategoryViewModel> ProductCategories { get; }

        public int SupplierId { get; }

        public DateTimeOffset SupplyDate { get; }
    }

    public class ProductDetailViewModel
    {
        public ProductDetailViewModel(decimal retailPrice, decimal unitPrice, string name,
            float quantity, decimal suppliedPrice, string? supplierName, string? mobileNumber, string? imageUrl, DateTimeOffset supplyDate, int id)
        {
            Name = name;
            RetailPrice = retailPrice;
            Quantity = quantity;
            CostPrice = suppliedPrice;
            ProductCategories = new List<CategoryViewModel>();
            SupplyDate = supplyDate;
            Id = id;
            ImageUrl = imageUrl;
            SupplierMobileNumber = mobileNumber;
            SupplierName = supplierName;
            UnitPrice = unitPrice;
        }

        public decimal UnitPrice { get; }

        public ProductDetailViewModel()
        {
            ProductCategories = new List<CategoryViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; } = null!;

        public decimal RetailPrice { get; }

        public float Quantity { get; }

        public decimal CostPrice { get; }

        public List<CategoryViewModel> ProductCategories { get; }
        
        public string? SupplierName { get; }

        public string? SupplierMobileNumber { get; }
        
        public string? ImageUrl { get; set; }
        
        public DateTimeOffset SupplyDate { get; }
    }
}

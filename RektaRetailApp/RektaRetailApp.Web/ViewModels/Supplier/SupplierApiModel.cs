using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.Web.ViewModel.Product;

namespace RektaRetailApp.Web.ViewModel.Supplier
{
    public class SupplierViewModel
    {
        public SupplierViewModel(string? name, string? mobileNumber, int supplierId)
        {
            Name = name;
            MobileNumber = mobileNumber;
            SupplierId = supplierId;
        }

        public SupplierViewModel()
        {
            
        }

        public string? Name { get; }

        public string? MobileNumber { get; }

        public int SupplierId { get; }
    }

    public class SupplierDetailViewModel
    {
        public SupplierDetailViewModel(string? name, string? phoneNumber, string? description, int id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Description = description;
            SupplierId = id;
        }

        public int SupplierId { get; }
        public string? Name { get; }

        public string? PhoneNumber { get; }

        public string? Description { get; }

        public List<ProductSummaryViewModel> ProductsSupplied { get; set; } = new List<ProductSummaryViewModel>();

    }
}

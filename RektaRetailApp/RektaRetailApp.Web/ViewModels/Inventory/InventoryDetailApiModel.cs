using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.Web.ViewModel.Category;
using RektaRetailApp.Web.ViewModel.Product;

namespace RektaRetailApp.Web.ViewModel.Inventory
{
    public class InventoryDetailViewModel
    {
        public InventoryDetailViewModel()
        {
            Categories = new List<CategoryViewModel>();
            InventoryItems = new List<ProductViewModel>();
        }
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset InventoryDate { get; set; }

        public List<ProductViewModel> InventoryItems { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }
}

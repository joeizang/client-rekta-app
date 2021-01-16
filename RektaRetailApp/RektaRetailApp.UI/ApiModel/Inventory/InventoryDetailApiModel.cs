using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.UI.ApiModel.Category;
using RektaRetailApp.UI.ApiModel.Product;

namespace RektaRetailApp.UI.ApiModel.Inventory
{
    public class InventoryDetailApiModel
    {
        public InventoryDetailApiModel()
        {
            Categories = new List<CategoryApiModel>();
            InventoryItems = new List<ProductApiModel>();
        }
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset InventoryDate { get; set; }

        public List<ProductApiModel> InventoryItems { get; set; }

        public List<CategoryApiModel> Categories { get; set; }
    }
}

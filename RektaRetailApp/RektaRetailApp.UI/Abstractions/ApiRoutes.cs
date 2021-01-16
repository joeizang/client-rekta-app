namespace RektaRetailApp.UI.Abstractions
{
    public static class RektaRoutes
    {
        public static string Base = "api";
        public static string GetAllProducts = Base + "/products";
        public static string GetOneProduct = Base + "/products/" + "{id}";
        public static string CreateProduct = Base + "/products";
        public static string UpdateProduct = Base + "/products";
        public static string DeleteProduct = Base + "/products/" + "{id}";

        public static string GetAllSales = Base + "/sales";
        public static string GetOneSale = Base + "/sales/" + "{id}";
        public static string CreateSale = Base + "/sales";
        public static string UpdateSale = Base + "/sales";
        public static string DeleteSale = Base + "/sales/" + "{id}";
        public static string GetAllInventories = Base + "/inventories";
        public static string GetOneInventory = Base + "/inventories/" + "{id}";
        public static string CreateInventory = Base + "/inventories";
        public static string UpdateInventory = Base + "/inventories";
        public static string DeleteInventory = Base + "/inventories/" + "{id}";
        public static string GetAllSuppliers = Base + "/suppliers";
        public static string GetOneSupplier = Base + "/suppliers/" + "{id}";
        public static string CreateSupplier = Base + "/suppliers";
        public static string UpdateSupplier = Base + "/suppliers";
        public static string DeleteSupplier = Base + "/suppliers/" + "{id}";
    }
}

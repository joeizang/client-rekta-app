using RektaGraphQLServer.Common;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Inventories
{
    public class AddInventoryPayload : InventoryPayload
    {
        public AddInventoryPayload(Inventory inventory) : base(inventory)
        {
        }

        public AddInventoryPayload(UserError error) : base(error)
        {
        }
    }
}
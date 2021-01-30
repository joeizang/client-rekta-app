using RektaGraphQLServer.Common;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Inventories
{
    public class InventoryPayload : PayloadBase
    {
        public InventoryPayload(Inventory inventory)
        {
            Inventory = inventory;
        }

        public InventoryPayload(UserError error) : base(new[] { error })
        {
        }

        public Inventory? Inventory { get; private set; }
    }
}
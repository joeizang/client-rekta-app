using System;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.Helpers;

namespace RektaRetailApp.UI.DomainEvents.Product
{
    public class ProductDeleteEvent : DomainEvent
    {
        public int DeletedProductId { get; set; }

        public ProductDeleteEvent()
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Deletion;
        }

    }
}

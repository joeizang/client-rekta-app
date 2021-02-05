using System;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.DomainEvents.Product
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

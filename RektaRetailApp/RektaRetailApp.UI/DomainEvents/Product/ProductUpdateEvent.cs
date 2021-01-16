using System;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Product;
using RektaRetailApp.UI.Helpers;

namespace RektaRetailApp.UI.DomainEvents.Product
{
    public class ProductUpdateEvent : DomainEvent
    {
        public ProductUpdateEvent(ProductDetailApiModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Modification;
            PayLoad = model;
        }
    }
}

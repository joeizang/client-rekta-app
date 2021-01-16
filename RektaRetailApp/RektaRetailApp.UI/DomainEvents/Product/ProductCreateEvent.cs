using System;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Product;
using RektaRetailApp.UI.Helpers;

namespace RektaRetailApp.UI.DomainEvents.Product
{
        public class ProductCreateEvent : DomainEvent
    {
        public ProductCreateEvent(ProductDetailApiModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Creation;
            PayLoad = model;
        }
    }
}

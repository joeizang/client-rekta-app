using System;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Product;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.DomainEvents.Product
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

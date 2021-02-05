using System;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Product;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.DomainEvents.Product
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

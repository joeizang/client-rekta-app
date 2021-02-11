using System;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModel.Product;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.DomainEvents.Product
{
    public class ProductUpdateEvent : DomainEvent
    {
        public ProductUpdateEvent(ProductDetailViewModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Modification;
            PayLoad = model;
        }
    }
}

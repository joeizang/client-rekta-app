using System;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModel.Product;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.DomainEvents.Product
{
        public class ProductCreateEvent : DomainEvent
    {
        public ProductCreateEvent(ProductDetailViewModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Creation;
            PayLoad = model;
        }
    }
}

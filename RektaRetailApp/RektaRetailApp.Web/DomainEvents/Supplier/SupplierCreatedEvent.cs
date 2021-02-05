using System;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModel.Supplier;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.DomainEvents.Supplier
{
    public class SupplierCreatedEvent : DomainEvent
    {

        public SupplierCreatedEvent(SupplierViewModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Creation;
            PayLoad = model;
        }
    }
}

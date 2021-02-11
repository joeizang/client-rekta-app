using System;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.ViewModel.Sales;
using RektaRetailApp.Web.ViewModels;

namespace RektaRetailApp.Web.DomainEvents.Sales
{
    public class SaleCreateEvent : DomainEvent
    {
        public SaleCreateEvent(SaleViewModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Creation;
            PayLoad = model;
        }
    }
}

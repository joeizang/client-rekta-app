using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Sales;
using RektaRetailApp.UI.Helpers;

namespace RektaRetailApp.UI.DomainEvents.Sales
{
    public class SaleCreateEvent : DomainEvent
    {
        public SaleCreateEvent(SaleApiModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Creation;
            PayLoad = model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Sales;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.DomainEvents.Sales
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

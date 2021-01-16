using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.UI.Abstractions;
using RektaRetailApp.UI.ApiModel;
using RektaRetailApp.UI.ApiModel.Supplier;
using RektaRetailApp.UI.Helpers;

namespace RektaRetailApp.UI.DomainEvents.Supplier
{
    public class SupplierCreatedEvent : DomainEvent
    {

        public SupplierCreatedEvent(SupplierApiModel model)
        {
            HappenedAt = DateTimeOffset.Now;
            ActionPerformed = TaskPerformed.Creation;
            PayLoad = model;
        }
    }
}

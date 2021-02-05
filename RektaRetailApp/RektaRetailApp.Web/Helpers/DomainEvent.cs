using System;
using RektaRetailApp.Web.Abstractions;

namespace RektaRetailApp.Web.Helpers
{
    public class DomainEvent : IDomainEvent
    {
        public DateTimeOffset HappenedAt { get; set; }

        public string ActionPerformed { get; set; } = null!;
        public object? PayLoad { get; set; }
    }
}

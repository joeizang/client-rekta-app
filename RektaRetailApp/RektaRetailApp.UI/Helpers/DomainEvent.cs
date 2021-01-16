using System;
using RektaRetailApp.UI.Abstractions;

namespace RektaRetailApp.UI.Helpers
{
    public class DomainEvent : IDomainEvent
    {
        public DateTimeOffset HappenedAt { get; set; }

        public string ActionPerformed { get; set; } = null!;
        public object? PayLoad { get; set; }
    }
}

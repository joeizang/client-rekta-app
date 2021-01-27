using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RektaRetailApp.Domain.Abstractions
{
    public abstract class BaseDomainModel : IDomainModel
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        [Key]
        public int Id { get; set; }

        public string CreatedBy { get; set; } = "Anonymous";

        public string UpdatedBy { get; set; } = "Anonymous";

        public bool IsDeleted { get; set; } = false;
    }
}

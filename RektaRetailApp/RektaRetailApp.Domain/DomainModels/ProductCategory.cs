using RektaRetailApp.Domain.Abstractions;

namespace RektaRetailApp.Domain.DomainModels
{
    public class ProductCategory : BaseDomainModel
    {
        public string? CategoryName { get; set; }

        public string? Description { get; set; }
    }
}
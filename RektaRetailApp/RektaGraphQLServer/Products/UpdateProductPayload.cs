using RektaGraphQLServer.Common;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Products
{
    public class UpdateProductPayload : PayloadBase
    {
        public UpdateProductPayload(Product product)
        {
            Product = product;
        }

        public UpdateProductPayload(UserError error) : base(new[] { error })
        {
        }

        public Product? Product { get; private set; }
    }
}
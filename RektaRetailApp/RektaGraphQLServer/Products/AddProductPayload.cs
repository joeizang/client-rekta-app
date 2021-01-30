using RektaGraphQLServer.Common;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Products
{
    public class AddProductPayload : PayloadBase
    {
        public AddProductPayload(Product product)
        {
            Product = product;
        }

        public AddProductPayload(UserError error) : base(new[] { error })
        {

        }

        public Product? Product { get; set; }


    }
}
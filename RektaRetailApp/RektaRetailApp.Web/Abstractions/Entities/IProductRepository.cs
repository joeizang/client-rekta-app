using System.Threading;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Commands.Product;
using RektaRetailApp.Web.Queries.Product;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.Abstractions.Entities
{
    public interface IProductRepository : IRepository
    {
        Task<PagedList<Product>> GetAllProducts(GetAllProductsQuery query, CancellationToken token);

        Task<Product> GetProductByIdAsync(int id, CancellationToken token);

        Task<Product> GetProductByAsync(CancellationToken token, Expression<Func<Product, object>>[]? includes,
            params Expression<Func<Product, bool>>[] searchTerms);

        Task CreateProductAsync(CreateProductCommand command, CancellationToken token);

        Task UpdateProductAsync(UpdateProductCommand command, CancellationToken token);

        void DeleteProductAsync(DeleteProductCommand command);

        Task SaveAsync(CancellationToken token);
    }
}

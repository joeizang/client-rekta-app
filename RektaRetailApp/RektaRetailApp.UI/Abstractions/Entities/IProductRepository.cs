using System.Threading;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.Commands.Product;
using RektaRetailApp.UI.Queries.Product;
using RektaRetailApp.UI.Helpers;

namespace RektaRetailApp.UI.Abstractions.Entities
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

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.Commands.Sales;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Sales;

namespace RektaRetailApp.Web.Services
{
        public class SalesRepository : GenericBaseRepository, ISalesRepository
    {
        private readonly RektaContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<Sale> _set;
        public SalesRepository([NotNull] IHttpContextAccessor accessor,
            [NotNull] RektaContext db, IMapper mapper) : base(accessor, db)
        {
            _db = db;
            _mapper = mapper;
            _set = _db.Sales;
        }

        public async Task CreateSale(CreateSaleCommand command, CancellationToken token)
        {
            var products = _db.ProductsForSale;
            var sale = _mapper.Map<Sale>(command);
            //make deductions from the quantity of every product that has been sold
            foreach(var p in command.ProductsSold)
            {
                var product = await products.SingleOrDefaultAsync(x => x.Id == p.Id,token)
                    .ConfigureAwait(false);

                product.Quantity -= p.Quantity;
                _db.Entry(product).State = EntityState.Modified;
            }
            //if there are any discounts then you can calculate before persisting.
            sale.ProductsForSale.AddRange(products!);
            await _set.AddAsync(sale, token).ConfigureAwait(false);
        }

        public Task UpdateSale(UpdateSaleCommand command, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task CancelASale(CancelSaleCommand command, CancellationToken token)
        {
            throw new NotImplementedException();
            //after cancellation, inventory quantities should be updated
        }

        public async Task<PagedList<Sale>> GetAllSales(GetAllSalesQuery query, CancellationToken token)
        {  
            var queryable = _set.AsNoTracking();

            //TODO: BUILD YOUR DIFFERENT QUERIES ON THE IQUERYABLE INSTANCE TO SEARCH FILTER AND ORDER
            if (string.IsNullOrEmpty(query.SearchTerm))
            {
                //default query is performed.
                var result = new PagedList<Sale>(
                        await queryable.CountAsync(token)
                            .ConfigureAwait(false), query.PageNumber, query.PageSize,
                        await queryable.ToListAsync(token).ConfigureAwait(false));
                    return result;
            }

            //If we are here then SearchTerm and Orderby term are not null or empty
            var parsedTotal = decimal.Parse(query.SearchTerm!);
            var parsedDate = DateTimeOffset.Parse(query.SearchTerm!);
            queryable = queryable.Where(s => s.SalesPersonId.Equals(query.SearchTerm) 
                                             || s.SaleDate.Equals(parsedDate) 
                                             || s.GrandTotal.Equals(parsedTotal));
            queryable = queryable.OrderBy(x => x.SaleDate);
            var processedResult = await PagedList<Sale>.CreatePagedList(queryable,
                 query.PageNumber, query.PageSize, token).ConfigureAwait(false);
            return processedResult;
        }

        public async Task<Sale> GetSaleById(GetSaleByIdQuery query, CancellationToken token)
        {
            var result = await _set.SingleOrDefaultAsync(x => x.Id == query.Id, token).ConfigureAwait(false);
            return result;
        }

        public Task SaveAsync(CancellationToken token)
        {
            return Commit<Sale>(token);
        }
    }

}

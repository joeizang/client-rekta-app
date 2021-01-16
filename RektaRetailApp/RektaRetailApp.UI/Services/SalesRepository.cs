using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.Abstractions;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.Commands.Sales;
using RektaRetailApp.UI.Data;
using RektaRetailApp.UI.Helpers;
using RektaRetailApp.UI.Queries.Sales;

namespace RektaRetailApp.UI.Services
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
            var products = _db.Products.Include(p => p.Inventory);
            var sale = _mapper.Map<Sale>(command);
            var items = _db.ItemsSold;
            //make deductions from the quantity of every product that has been sold
            foreach(var p in command.ProductsSold)
            {
                var product = await products.SingleOrDefaultAsync(x => x.Id == p.Id,token).ConfigureAwait(false);

                product.Quantity -= p.Quantity;
                //if (product.Inventory != null) product.Inventory.Quantity -= p.Quantity;
                _db.Entry(product).State = EntityState.Modified;
                var item = new ItemSold
                {
                    ItemName = p.ItemName,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    ProductId = product.Id
                };
                await items.AddAsync(item, token).ConfigureAwait(false);
            }
            //if there are any discounts then you can calculate before persisting.
            sale.ItemsSold.AddRange(items!);
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
            if (query.SearchTerm is null && query.OrderByTerm is null)
            {
                var result = new PagedList<Sale>(
                        await queryable.CountAsync(token)
                            .ConfigureAwait(false), query.PageNumber, query.PageSize,
                        await queryable.ToListAsync(token).ConfigureAwait(false));
                    return result;
            }

            var parsedTotal = decimal.Parse(query.SearchTerm!);
            var parsedDate = DateTimeOffset.Parse(query.SearchTerm!);
            queryable = queryable.Where(s => s.SalesPersonId.Equals(query.SearchTerm) 
                                             || s.SaleDate.Equals(parsedDate) || s.GrandTotal.Equals(parsedTotal));
            queryable = queryable.OrderBy(x => x.SaleDate);
            //TODO: ALSO FACTOR IN THAT YOUR RESULT MUST BE PAGINATED.
            // queryable.Select(x => new SaleApiModel
            // {
            //     GrandTotal = x.GrandTotal,
            //     Id = x.Id,
            //     NumberOfItemsSold = x.ItemsSold.Count,
            //     SaleDate = x.SaleDate,
            //     SalesPerson = x.SalesPersonId,
            //     TypeOfPayment = x.ModeOfPayment,
            //     TypeOfSale = x.TypeOfSale
            // }),
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

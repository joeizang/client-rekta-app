using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Web.ViewModel.Sales;

namespace RektaRetailApp.Web.Queries.Sales
{
    public class MakeASaleQuery : IRequest<MakeASaleViewModel>
    {
    }


    public class MakeASaleQueryHandler : IRequestHandler<MakeASaleQuery, MakeASaleViewModel>
    {
        private readonly RektaContext _db;

        public MakeASaleQueryHandler(RektaContext db)
        {
            _db = db;
        }
        public async Task<MakeASaleViewModel> Handle(MakeASaleQuery request, CancellationToken cancellationToken)
        {
            var saleTypes = await _db.SaleTypes.AsNoTracking()
                .Select(x => new {x.Type, x.Description})
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            var saleTypeList = new List<SelectListItem>();
            saleTypes.ForEach(x =>
            {
                saleTypeList.Add(new SelectListItem { Value = x.Type, Text = x.Type });
            });

            var model = new MakeASaleViewModel {SaleTypes = saleTypeList};

            return model;
        }
    }
}

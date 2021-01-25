using System.Collections.Generic;
using RektaGraphQLServer.Common;
using RektaRetailApp.Domain.DomainModels;

namespace RektaGraphQLServer.Sales
{
    public class SalePayloadBase : PayloadBase
    {
        protected SalePayloadBase(IReadOnlyList<UserError>? errors = null) : base(errors)
        {
        }

        protected SalePayloadBase(Sale sale)
        {
            Sale = sale;
        }

        public Sale? Sale { get; }
        
    }
    
    public class AddSalesPayload : SalePayloadBase
    {
        public AddSalesPayload(Sale sales) : base(sales)
        {
        }

        protected AddSalesPayload(IReadOnlyList<UserError>? errors = null) : base(errors)
        {
        }
    }
}
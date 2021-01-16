using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.UI.Abstractions;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.ApiModel.Category;

namespace RektaRetailApp.UI.Queries.Category
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryApiModel>>
    {
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryApiModel>>
    {
        private readonly ICategoryRepository _repo;

        public GetCategoriesQueryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<CategoryApiModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetCategories().ConfigureAwait(false);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel.Category;

namespace RektaRetailApp.Web.Queries.Category
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryViewModel>>
    {
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryViewModel>>
    {
        private readonly ICategoryRepository _repo;

        public GetCategoriesQueryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<CategoryViewModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetCategories().ConfigureAwait(false);
            return result;
        }
    }
}

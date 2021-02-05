using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel.Category;

namespace RektaRetailApp.Web.Queries.Category
{
    public class GetCategoryDetailQuery : IRequest<CategoryViewModel>
    {
        public GetCategoryDetailQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryViewModel>
    {
        private readonly ICategoryRepository _repo;

        public GetCategoryDetailQueryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<CategoryViewModel> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetCategoryById(request.Id);
            return result;
        }
    }
}

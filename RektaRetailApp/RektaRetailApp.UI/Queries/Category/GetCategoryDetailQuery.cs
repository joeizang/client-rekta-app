﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.UI.Abstractions.Entities;
using RektaRetailApp.UI.ApiModel.Category;

namespace RektaRetailApp.UI.Queries.Category
{
    public class GetCategoryDetailQuery : IRequest<CategoryApiModel>
    {
        public GetCategoryDetailQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryApiModel>
    {
        private readonly ICategoryRepository _repo;

        public GetCategoryDetailQueryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<CategoryApiModel> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetCategoryById(request.Id);
            return result;
        }
    }
}

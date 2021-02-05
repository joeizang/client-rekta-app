using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel.Category;

namespace RektaRetailApp.Web.Commands.Category
{
    public class CreateCategoryCommand : IRequest<CategoryViewModel>
    {
        [Required] 
        [StringLength(50)] 
        public string Name { get; set; } = null!;

        [StringLength(450)]
        public string? Description { get; set; }
    }


    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryViewModel>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<CategoryViewModel> Handle(CreateCategoryCommand request, 
            CancellationToken cancellationToken)
        {
            var model = _mapper.Map<CreateCategoryViewModel>(request);
            _repo.Create(model);
            await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
            var response = await _repo.GetCategoryBy(model.Name, model.Description);
            return response;
        }
    }
}

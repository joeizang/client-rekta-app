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
    public class UpdateCategoryCommand : IRequest<CategoryViewModel>
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(450)]
        public string? Description { get; set; }

        [Required]
        public int Id { get; set; }
    }


    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repo;

        public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<CategoryViewModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<UpdateCategoryCommand,UpdateCategoryViewModel>(request);
            await _repo.Update(model);
            await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
            return await _repo.GetCategoryById(model.CategoryId);
        }
    }
}

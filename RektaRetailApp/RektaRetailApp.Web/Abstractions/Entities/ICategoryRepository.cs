using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ViewModel.Category;

namespace RektaRetailApp.Web.Abstractions.Entities
{
  public interface ICategoryRepository : IRepository
  {
    Task<IEnumerable<CategoryViewModel>> GetCategories();

    Task<IEnumerable<CategoryDropDownViewModel>> GetForDropDown();

    Task<CategoryViewModel> GetCategoryById(int id);

    Task<CategoryViewModel> GetCategoryBy(string name, string desc);

    void Create(CreateCategoryViewModel model);

    Task Update(UpdateCategoryViewModel entity);

    Task Delete(DeleteCategoryViewModel entity);

    Task SaveAsync(CancellationToken token);
  }
}

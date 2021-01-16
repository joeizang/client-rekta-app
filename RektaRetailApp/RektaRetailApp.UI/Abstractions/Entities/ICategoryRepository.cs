using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.ApiModel.Category;

namespace RektaRetailApp.UI.Abstractions.Entities
{
  public interface ICategoryRepository : IRepository
  {
    Task<IEnumerable<CategoryApiModel>> GetCategories();

    Task<IEnumerable<CategoryDropDownApiModel>> GetForDropDown();

    Task<CategoryApiModel> GetCategoryById(int id);

    Task<CategoryApiModel> GetCategoryBy(string name, string desc);

    void Create(CreateCategoryApiModel model);

    Task Update(UpdateCategoryApiModel entity);

    Task Delete(DeleteCategoryApiModel entity);

    Task SaveAsync(CancellationToken token);
  }
}

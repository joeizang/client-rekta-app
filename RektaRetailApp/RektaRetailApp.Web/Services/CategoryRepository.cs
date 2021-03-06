﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ViewModel.Category;


namespace RektaRetailApp.Web.Services
{
  public class CategoryRepository : GenericBaseRepository ,ICategoryRepository
  {
    private readonly RektaContext _db;
    private readonly IMapper _mapper;

    public CategoryRepository(RektaContext db, IMapper mapper, IHttpContextAccessor accessor) : base(accessor, db)
    {
      _db = db;
      _mapper = mapper;
    }
    public async Task<IEnumerable<CategoryViewModel>> GetCategories()
    {
      var result = await _db.Categories.AsNoTracking()
          .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
          .ToListAsync();
      return result;
    }

    public async Task<IEnumerable<CategoryDropDownViewModel>> GetForDropDown()
    {
      var result = await _db.Categories.AsNoTracking()
          .ProjectTo<CategoryDropDownViewModel>(_mapper.ConfigurationProvider)
          .ToArrayAsync();
      return result;
    }

    public async Task<CategoryViewModel> GetCategoryById(int id)
    {
      if (id == int.MaxValue || id == int.MinValue)
        throw new ArgumentException("the id passed doesn't identify a category");
      var result = await _db.Categories.AsNoTracking()
          .Where(x => x.Id == id)
          .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync()
          .ConfigureAwait(false);
      return result;
    }

    public async Task<CategoryViewModel> GetCategoryBy(string name, string desc)
    {
      var result = await _db.Categories.AsNoTracking()
          .Where(x => x.Description != null &&
                      x.Name.Equals(name.ToUpperInvariant()) &&
                      x.Description.Equals(desc.ToUpperInvariant()))
          .SingleOrDefaultAsync();
      var category = _mapper.Map<Category, CategoryViewModel>(result);
      return category;
    }

    public void Create(CreateCategoryViewModel entity)
    {
      if (entity == null)
        throw new ArgumentException("category to be created cannot be null!");
      var category = _mapper.Map<CreateCategoryViewModel, Category>(entity);
      category.Name = category.Name.Trim().ToUpperInvariant();
      category.Description = category.Description?.Trim().ToUpperInvariant();
      _db.Categories.Add(category);
    }

    public async Task Update(UpdateCategoryViewModel entity)
    {
      if (entity is null)
        throw new ArgumentException("cannot update null values");
      //var category = _mapper.Map<UpdateCategoryViewModel, Category>(entity);
      var category = await _db.Categories.AsNoTracking()
                      .SingleOrDefaultAsync(x => x.Id == entity.CategoryId);
      category.Description = category.Description?.Trim().ToUpperInvariant();
      category.Name = category.Name.Trim().ToUpperInvariant();
      _db.Entry(category).State = EntityState.Modified;
    }

    public async Task Delete(DeleteCategoryViewModel entity)
    {
      if(entity is null)
          throw new ArgumentNullException("cannot delete null!");
      var category = await _db.Categories.AsNoTracking()
          .SingleOrDefaultAsync(x => x.Id == entity.Id);

      _db.Categories.Remove(category);
    }

    public async Task SaveAsync(CancellationToken token)
    {
        await Commit<Category>(token).ConfigureAwait(false);
    }

  }
}

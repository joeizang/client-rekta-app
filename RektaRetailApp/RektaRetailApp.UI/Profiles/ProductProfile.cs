using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.UI.ApiModel.Category;
using RektaRetailApp.UI.ApiModel.Product;
using RektaRetailApp.UI.Commands.Product;

namespace RektaRetailApp.UI.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductApiModel>();
            CreateMap<ProductApiModel, Product>();
            CreateMap<Product, ProductSummaryApiModel>();
            CreateMap<CreateProductCommand, Product>()
                .ForMember(d => d.ProductCategories, conf => conf.Ignore());
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Services;
using RektaRetailApp.Web.ViewModel.Supplier;

namespace RektaRetailApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RektaContext>(options =>
                options.UseNpgsql(
                        Configuration.GetConnectionString("DefaultConnection"),
                        n => n.MigrationsAssembly("RektaRetailApp.Web")).EnableSensitiveDataLogging());
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<RektaContext>();
            services.AddControllersWithViews();

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<PagedList<Supplier>>();
            services.AddScoped<PagedList<SupplierViewModel>>();
            services.AddTransient<PaginatedMetaData>();
            services.AddScoped<IRepository, GenericBaseRepository>();

            services.AddTransient<IUriGenerator, UriGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
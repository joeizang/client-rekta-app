using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RektaGraphQLServer.DataLoader;
using RektaGraphQLServer.Inventories;
using RektaGraphQLServer.Products;
using RektaGraphQLServer.Sales;
using RektaGraphQLServer.Types;
using RektaRetailApp.Domain.Data;

namespace RektaGraphQLServer
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
            services.AddPooledDbContextFactory<RektaContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                        n => n.MigrationsAssembly("RektaGraphQLServer"))
                    .EnableSensitiveDataLogging();
            });
            //Add GraphQL Server
            services
                .AddGraphQLServer()
                .AddQueryType(descriptor => descriptor.Name("Query"))
                .AddTypeExtension<SalesQueries>()
                .AddTypeExtension<ProductQueries>()
                .AddTypeExtension<InventoryQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<SaleMutation>()
                .AddTypeExtension<ProductMutation>()
                .AddTypeExtension<InventoryMutation>()
                .AddType<SaleType>()
                .AddType<ProductType>()
                .AddType<InventoryType>()
                .AddType<SupplierType>()
                .AddType<ApplicationUserType>()
                .AddType<ProductPriceType>()
                .AddType<ProductCategoryType>()
                .AddType<CategoryType>()
                .AddType<CustomerType>()
                .EnableRelaySupport()
                .AddDataLoader<SalesByIdDataLoader>()
                .AddDataLoader<ProductByIdDataLoader>()
                .AddDataLoader<ApplicationUserByIdDataLoader>()
                .AddDataLoader<ProductPriceByIdDataLoader>()
                .AddDataLoader<ProductCategoryByIdDataLoader>()
                .AddDataLoader<CustomerByIdDataLoader>()
                .AddDataLoader<SupplierByIdDataLoader>()
                .AddDataLoader<InventoryByIdDataLoader>()
                .AddDataLoader<CategoryByIdDataLoader>();
            // services.AddControllers();
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo {Title = "RektaGraphQLServer", Version = "v1"});
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RektaGraphQLServer v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
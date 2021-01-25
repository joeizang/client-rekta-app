using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RektaGraphQLServer.DataLoader;
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
                .AddQueryType<Query>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<SaleMutation>()
                .AddType<SaleType>()
                .EnableRelaySupport()
                .AddDataLoader<SalesByIdDataLoader>()
                .AddDataLoader<ProductByIdDataLoader>();
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
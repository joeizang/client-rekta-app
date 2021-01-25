using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Domain.Data
{
    public class RektaContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        private IOptions<OperationalStoreOptions> OperationalStoreOptions { get; }

        public RektaContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            OperationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<Sale> Sales { get; set; } = default!;

        public DbSet<Inventory> Inventories { get; set; } = default!;

        public DbSet<Customer> Customers { get; set; } = default!;

        public DbSet<Product> Products { get; set; } = default!;
        
        public DbSet<ProductPrice> ProductPrices { get; set; } = default!;
        
        public DbSet<ProductCategory> ProductCategories { get; set; } = default!;

        public DbSet<Shift> WorkerShifts { get; set; } = default!;

        public DbSet<Supplier> Suppliers { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;

        public DbSet<ApplicationRole> ApplicationRoles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Sale>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Product>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Inventory>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ProductPrice>()
                .HasQueryFilter(x => x.IsDeleted);
            builder.Entity<ProductCategory>()
                .HasQueryFilter(x => x.IsDeleted);
            builder.Entity<Category>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Supplier>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Shift>().HasQueryFilter(x => x.IsDeleted);
            builder.Entity<ApplicationUser>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ApplicationRole>().HasQueryFilter(x => !x.IsDeleted);

            builder.Entity<Inventory>()
                .HasMany(i => i.InventoryItems)
                .WithOne().OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(p => p.Price)
                .WithOne(p => p.Product)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.SalesYouOwn)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Sale>()
                .HasMany(s => s.ProductSold)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Product>()
                .HasMany(p => p.ProductCategories)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Product>()
                .HasOne(p => p.Inventory)
                .WithMany(i => i!.InventoryItems)
                .HasForeignKey(p => p.InventoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(p => p.Price)
                .WithOne(p => p.Product)
                .HasForeignKey<ProductPrice>(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Supplier>()
                .HasMany(s => s.ProductsSupplied)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Sale>()
                .Property(s => s.GrandTotal)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Sale>()
                .Property(s => s.SubTotal)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Shift>()
                .Property(s => s.HourlyRate)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Inventory>()
                .Property(i => i.TotalCostValue)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Inventory>()
                .Property(i => i.TotalRetailValue)
                .HasColumnType("decimal(12,2)");
            builder.Entity<ProductPrice>()
                .Property(p => p.RetailPrice)
                .HasColumnType("decimal(12,2)");
            builder.Entity<ProductPrice>()
                .Property(p => p.CostPrice)
                .HasColumnType("decimal(12,2)");
            builder.Entity<ProductPrice>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
            builder.Entity<Inventory>()
                .HasIndex(i => i.Name)
                .IsUnique();
            builder.Entity<Inventory>()
                .HasIndex(i => i.BatchNumber)
                .IsUnique();
            builder.Entity<Product>()
                .HasIndex(p => p.SupplyDate);
            builder.Entity<Supplier>()
                .HasIndex(p => p.Name)
                .IsUnique();
            builder.Entity<Supplier>()
                .HasIndex(s => s.MobileNumber)
                .IsUnique();
            builder.Entity<Sale>()
                .HasIndex(s => s.SaleDate);
            builder.Entity<ProductPrice>()
                .HasIndex(p => p.RetailPrice);
            builder.Entity<ProductCategory>()
                .HasIndex(p => p.CategoryName);
        }
    }
}

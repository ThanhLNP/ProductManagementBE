using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductManagementBE.Entities.Contexts
{
    public class ProductManagementDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(_ => _.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(_ => _.Name)
                .IsUnique();

            modelBuilder.Entity<ProductCategory>()
                .HasIndex(_ => new { _.ProductId, _.CategoryId })
                .IsUnique();

            modelBuilder.Entity<Category>()
                .Property(p => p.AllowedAttributes)
                .HasDefaultValue("{}");

            modelBuilder.Entity<ProductCategory>()
                .Property(p => p.Attributes)
                .HasDefaultValue("{}");

            base.OnModelCreating(modelBuilder);
        }
    }
}

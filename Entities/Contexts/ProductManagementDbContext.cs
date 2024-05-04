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
    }
}

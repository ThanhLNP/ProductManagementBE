using Microsoft.AspNetCore.Identity;
using ProductManagementBE.Common.Constants;
using ProductManagementBE.Entities;

namespace ProductManagementBE.Services.DataInitialize
{
    public class DummyDataInitializeService : IDataInitializeService
    {
        private readonly ProductManagementDbContext _productManagementDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DummyDataInitializeService(
            ProductManagementDbContext productManagementDbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _productManagementDbContext = productManagementDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public int Order
        {
            get => 1;
            set { }
        }

        public async Task RunAsync()
        {
            if (await _roleManager.FindByNameAsync(AppRoleConstants.ADMINISTRATOR) == null)
            {
                var role = new ApplicationRole { Name = AppRoleConstants.ADMINISTRATOR };
                await _roleManager.CreateAsync(role);
            }

            if (await _roleManager.FindByNameAsync(AppRoleConstants.USER) == null)
            {
                var role = new ApplicationRole { Name = AppRoleConstants.USER };
                await _roleManager.CreateAsync(role);
            }

            if (await _userManager.FindByNameAsync("admin@example.com") == null)
            {
                var user = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
                await _userManager.CreateAsync(user, "adminP@ssw0rd");
                await _userManager.AddToRoleAsync(user, AppRoleConstants.ADMINISTRATOR);
            }

            if (await _userManager.FindByNameAsync("user@example.com") == null)
            {
                var user = new ApplicationUser { UserName = "user@example.com", Email = "user@example.com" };
                await _userManager.CreateAsync(user, "userP@ssw0rd");
                await _userManager.AddToRoleAsync(user, AppRoleConstants.USER);
            }

            var hasCategories = await _productManagementDbContext.Categories.AnyAsync();
            if (!hasCategories)
            {
                var categories = new List<Category>
                {
                    new() {
                        Name = "Tivi",
                        AllowedAttributes = "{\"display_size\": \"number\", \"color\": \"string\"}"
                    },
                    new()
                    {
                        Name = "Laptop",
                        AllowedAttributes = "{\"ram_gb\": \"number\", \"display_size\": \"number\"}"
                    }
                };

                _productManagementDbContext.Categories.AddRange(categories);

                await _productManagementDbContext.SaveChangesAsync();

                var products = new List<Product>
                {
                    new()
                    {
                        Name = "Big TV",
                        Price = 1,
                    },
                    new()
                    {
                        Name = "Small  TV",
                        Price= 2,
                    },
                    new()
                    {
                        Name = "High-End Laptop",
                        Price= 3,
                    }
                };

                _productManagementDbContext.Products.AddRange(products);

                await _productManagementDbContext.SaveChangesAsync();

                var productCategories = new List<ProductCategory>
                {
                    new()
                    {
                        ProductId = products[0].Id,
                        CategoryId= categories[0].Id,
                        Attributes = "{\"display_size\": 60}"
                    },
                    new()
                    {
                        ProductId = products[1].Id,
                        CategoryId= categories[0].Id,
                        Attributes = "{\"display_size\": 32}"
                    },
                    new()
                    {
                        ProductId = products[2].Id,
                        CategoryId= categories[1].Id,
                        Attributes = "{\"ram_gb\": 128}"
                    }
                };

                _productManagementDbContext.ProductCategories.AddRange(productCategories);

                await _productManagementDbContext.SaveChangesAsync();
            }
        }
    }
}

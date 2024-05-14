using ProductManagementBE.Common.Constants;
using ProductManagementBE.Entities;
using ProductManagementBE.Entities.Contexts;
using ProductManagementBE.Models.Products.Response;
using ProductManagementBE.Models.Products.Resquest;

namespace ProductManagementBE.Controllers
{
    [Route("/product")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize()]
    public class ProductController : ControllerBase
    {
        private readonly ProductManagementDbContext _productManagementDbContext;

        public ProductController(ProductManagementDbContext productManagementDbContext)
        {
            _productManagementDbContext = productManagementDbContext;
        }

        [HttpGet()]
        public async Task<ActionResult> GetProductListAsync([FromQuery] ProductListRequest request)
        {
            var products = await _productManagementDbContext.Products
                .AsNoTracking()
                .Where(_ => _.IsDeleted == request.IsDeleted)
                .Where(_ => request.CategoryIds.Count == 0
                    || _.ProductCategories.Any(pc => request.CategoryIds.Contains(pc.CategoryId)))
                .Select(_ => new ProductListResponse
                {
                    Id = _.Id,
                    Name = _.Name,
                    Price = _.Price,
                    Brand = _.Brand,
                    Category = _.ProductCategories
                        .Select(pc => new ProductCategoryListResponse
                        {
                            Id = pc.CategoryId,
                            Name = pc.Category.Name,
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetProductByIdAsync(Guid id)
        {
            var product = await _productManagementDbContext.Products
                .AsNoTracking()
                .Where(_ => _.Id == id && !_.IsDeleted)
                .Select(_ => new ProductGetByIdResponse
                {
                    Id = _.Id,
                    Name = _.Name,
                    Price = _.Price,
                    Brand = _.Brand,
                    Description = _.Description,
                    Category = _.ProductCategories
                        .Select(pc => new ProductCategoryListResponse
                        {
                            Id = pc.CategoryId,
                            Name = pc.Category.Name,
                            Attributes = pc.Attributes
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Authorize(Roles = AppRoleConstants.ADMINISTRATOR)]
        [HttpPost]
        public async Task<ActionResult> PostProductAsync([FromBody] ProductCreateRequest request)
        {
            var existedProduct = await _productManagementDbContext.Products
                .AsNoTracking()
                .AnyAsync(_ => _.Name == request.Name);

            if (existedProduct)
                return BadRequest();

            var existedCategory = await _productManagementDbContext.Categories
               .AsNoTracking()
               .AnyAsync(_ => _.Id == request.CategoryId && !_.IsDeleted);

            if (!existedCategory)
                return BadRequest();

            var userLoggedEmail = User.Identity?.Name;

            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Brand = request.Brand,
                Description = request.Description,
                CreatedBy = userLoggedEmail,
            };

            _productManagementDbContext.Products.Add(product);

            var productCategory = new ProductCategory
            {
                ProductId = product.Id,
                CategoryId = request.CategoryId,
                Attributes = request.Attributes.ToString(),
            };

            _productManagementDbContext.ProductCategories.Add(productCategory);

            await _productManagementDbContext.SaveChangesAsync();

            return Created();
        }

        [Authorize(Roles = AppRoleConstants.ADMINISTRATOR)]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> PutProductAsync(Guid id, [FromBody] ProductEditRequest request)
        {
            var product = await _productManagementDbContext.Products
                .Include(_=>_.ProductCategories)
                .FirstOrDefaultAsync(_ => _.Id == id && !_.IsDeleted);

            if (product == null)
                return NotFound();

            var existedCategory = await _productManagementDbContext.Categories
              .AsNoTracking()
              .AnyAsync(_ => _.Id == request.CategoryId && !_.IsDeleted);

            if (!existedCategory)
                return BadRequest();

            var userLoggedEmail = User.Identity?.Name;

            product.Price = request.Price;
            product.Brand = request.Brand;
            product.Description = request.Description;
            product.UpdatedBy = userLoggedEmail;
            product.UpdatedOn = DateTime.UtcNow;

            _productManagementDbContext.ProductCategories.RemoveRange(product.ProductCategories);

            var productCategory = new ProductCategory
            {
                ProductId = product.Id,
                CategoryId = request.CategoryId,
                Attributes = request.Attributes.ToString(),
            };

            _productManagementDbContext.ProductCategories.Add(productCategory);

            await _productManagementDbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = AppRoleConstants.ADMINISTRATOR)]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteProductAsync(Guid id)
        {
            var product = await _productManagementDbContext.Products
                 .FirstOrDefaultAsync(_ => _.Id == id && !_.IsDeleted);

            if (product == null)
                return NoContent();

            var userLoggedEmail = User.Identity?.Name;

            product.IsDeleted = true;
            product.DeletedBy = userLoggedEmail;
            product.DeletedOn = DateTime.UtcNow;

            await _productManagementDbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = AppRoleConstants.ADMINISTRATOR)]
        [HttpPut("{id:guid}/restore")]
        public async Task<ActionResult> RestoreProductAsync(Guid id)
        {
            var product = await _productManagementDbContext.Products
               .FirstOrDefaultAsync(_ => _.Id == id && _.IsDeleted);

            if (product == null)
                return NotFound();

            product.IsDeleted = false;

            await _productManagementDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

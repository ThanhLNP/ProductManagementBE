using ProductManagementBE.Entities;
using ProductManagementBE.Models.Products.Response;
using ProductManagementBE.Models.Products.Resquest;

namespace ProductManagementBE.Controllers
{
    [Route("/product")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly ProductManagementDbContext _productManagementDbContext;

        public ProductController(ProductManagementDbContext productManagementDbContext)
        {
            _productManagementDbContext = productManagementDbContext;
        }

        [Authorize()]
        [HttpGet()]
        public async Task<ActionResult> GetProductListAsync([FromQuery] ProductListRequest request)
        {
            var categories = await _productManagementDbContext.Products
                .AsNoTracking()
                .Where(_ => _.IsDeleted == request.IsDeleted)
                .Select(_ => new ProductListResponse
                {
                    Id = _.Id,
                    Name = _.Name,
                    Price = _.Price,
                    Brand = _.Brand,
                })
                .ToListAsync();

            return Ok(categories);
        }

        [Authorize()]
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
                    Description = _.Description
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Authorize()]
        [HttpPost]
        public async Task<ActionResult> PostProductAsync([FromBody] ProductCreateRequest request)
        {
            var existedProduct = await _productManagementDbContext.Products
                .AsNoTracking()
                .AnyAsync(_ => _.Name == request.Name);

            if (existedProduct)
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

            await _productManagementDbContext.SaveChangesAsync();

            return Created();
        }

        [Authorize()]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> PutProductAsync(Guid id, [FromBody] ProductEditRequest request)
        {
            var product = await _productManagementDbContext.Products
                  .FirstOrDefaultAsync(_ => _.Id == id && !_.IsDeleted);

            if (product == null)
                return NotFound();

            var userLoggedEmail = User.Identity?.Name;

            product.Price = request.Price;
            product.Brand = request.Brand;
            product.Description = request.Description;
            product.UpdatedBy = userLoggedEmail;
            product.UpdatedOn = DateTime.UtcNow;

            await _productManagementDbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize()]
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

            return NoContent();
        }

        [Authorize()]
        [HttpPut("{id:guid}/restore")]
        public async Task<ActionResult> RestoreProductAsync(Guid id)
        {
            var product = await _productManagementDbContext.Products
               .FirstOrDefaultAsync(_ => _.Id == id && _.IsDeleted);

            if (product == null)
                return NotFound();

            product.IsDeleted = false;

            return NoContent();
        }
    }
}

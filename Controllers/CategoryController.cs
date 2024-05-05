using ProductManagementBE.Common.Constants;
using ProductManagementBE.Entities;
using ProductManagementBE.Models.Categories.Response;
using ProductManagementBE.Models.Categories.Resquest;

namespace ProductManagementBE.Controllers
{
    [Route("/category")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize()]
    public class CategoryController : ControllerBase
    {
        private readonly ProductManagementDbContext _productManagementDbContext;

        public CategoryController(ProductManagementDbContext productManagementDbContext)
        {
            _productManagementDbContext = productManagementDbContext;
        }

        [HttpGet()]
        public async Task<ActionResult> GetCategoryListAsync([FromQuery] CategoryListRequest request)
        {
            var categories = await _productManagementDbContext.Categories
                .AsNoTracking()
                .Where(_ => _.IsDeleted == request.IsDeleted)
                .Select(_ => new CategoryListResponse
                {
                    Id = _.Id,
                    Name = _.Name,
                })
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetCategoryByIdAsync(Guid id)
        {
            var category = await _productManagementDbContext.Categories
                .AsNoTracking()
                .Where(_ => _.Id == id && !_.IsDeleted)
                .Select(_ => new CategoryGetByIdResponse
                {
                    Id = _.Id,
                    Name = _.Name,
                    AllowedAttributes = _.AllowedAttributes,
                })
                .FirstOrDefaultAsync();

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [Authorize(Roles = AppRoleConstants.ADMINISTRATOR)]
        [HttpPost]
        public async Task<ActionResult> PostCategoryAsync([FromBody] CategoryCreateRequest request)
        {
            var existedCategory = await _productManagementDbContext.Categories
                .AsNoTracking()
                .AnyAsync(_ => _.Name == request.Name);

            if (existedCategory)
                return BadRequest();

            var userLoggedEmail = User.Identity?.Name;

            var category = new Category
            {
                Name = request.Name,
                AllowedAttributes = request.AllowedAttributes.ToString(),
                CreatedBy = userLoggedEmail,
            };

            _productManagementDbContext.Categories.Add(category);

            await _productManagementDbContext.SaveChangesAsync();

            return Created();
        }

        [Authorize(Roles = AppRoleConstants.ADMINISTRATOR)]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> PutCategoryAsync(Guid id, [FromBody] CategoryEditRequest request)
        {
            var category = await _productManagementDbContext.Categories
                  .FirstOrDefaultAsync(_ => _.Id == id && !_.IsDeleted);

            if (category == null)
                return NotFound();

            var userLoggedEmail = User.Identity?.Name;

            category.AllowedAttributes = request.AllowedAttributes.ToString();
            category.UpdatedBy = userLoggedEmail;
            category.UpdatedOn = DateTime.UtcNow;

            await _productManagementDbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = AppRoleConstants.ADMINISTRATOR)]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCategoryAsync(Guid id)
        {
            var category = await _productManagementDbContext.Categories
                 .FirstOrDefaultAsync(_ => _.Id == id && !_.IsDeleted);

            if (category == null)
                return NoContent();

            var userLoggedEmail = User.Identity?.Name;

            category.IsDeleted = true;
            category.DeletedBy = userLoggedEmail;
            category.DeletedOn = DateTime.UtcNow;

            return NoContent();
        }

        [Authorize(Roles = AppRoleConstants.ADMINISTRATOR)]
        [HttpPut("{id:guid}/restore")]
        public async Task<ActionResult> RestoreCategoryAsync(Guid id)
        {
            var category = await _productManagementDbContext.Categories
               .FirstOrDefaultAsync(_ => _.Id == id && _.IsDeleted);

            if (category == null)
                return NotFound();

            category.IsDeleted = false;

            return NoContent();
        }
    }
}

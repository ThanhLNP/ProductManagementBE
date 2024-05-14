using ProductManagementBE.Models.Selections.Response;

namespace ProductManagementBE.Controllers
{
    [Route("/selection")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize()]
    public class SelectionController : ControllerBase
    {
        private readonly ProductManagementDbContext _productManagementDbContext;

        public SelectionController(ProductManagementDbContext productManagementDbContext)
        {
            _productManagementDbContext = productManagementDbContext;
        }

        [HttpGet("categories")]
        public async Task<ActionResult> GetCategorySelectionAsync()
        {
            var categories = await _productManagementDbContext.Categories
                .AsNoTracking()
                .Where(_ => !_.IsDeleted)
                .Select(_ => new BaseSelectionResponse
                {
                    Id = _.Id,
                    Name = _.Name,
                })
                .ToListAsync();

            return Ok(categories);
        }
    }
}

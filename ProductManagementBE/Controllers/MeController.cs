using ProductManagementBE.Models.Me.Response;

namespace ProductManagementBE.Controllers
{
    [Route("/me")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize()]
    public class MeController : ControllerBase
    {
        private readonly ProductManagementDbContext _productManagementDbContext;

        public MeController(ProductManagementDbContext productManagementDbContext)
        {
            _productManagementDbContext = productManagementDbContext;
        }

        [HttpGet("get-me")]
        public async Task<ActionResult> GetMeAsync()
        {
            var userLoggedEmail = User.Identity?.Name;

            var user = await _productManagementDbContext.ApplicationUsers
                .Where(_ => _.Email == userLoggedEmail)
                .Select(_ => new UserProfileResponse
                {
                    Id = _.Id,
                    Name = _.UserName,
                    Email = _.Email,
                    PhoneNumber = _.PhoneNumber,
                    Address = _.Address,
                    Gender = _.Gender,
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}

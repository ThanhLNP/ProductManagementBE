namespace ProductManagementBE.Models.Categories.Response
{
    public class CategoryGetByIdResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string AllowedAttributes { get; set; }
    }
}

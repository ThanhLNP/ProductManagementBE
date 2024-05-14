namespace ProductManagementBE.Models.Categories.Response
{
    public class CategoryListResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
    }
}

namespace ProductManagementBE.Models.Products.Response
{
    public class ProductCategoryListResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Attributes { get; set; }
    }
}

namespace ProductManagementBE.Models.Products.Response
{
    public class ProductListResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string? Brand { get; set; }

        public List<ProductCategoryListResponse>? Category { get; set; }
    }
}

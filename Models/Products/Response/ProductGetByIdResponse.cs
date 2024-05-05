namespace ProductManagementBE.Models.Products.Response
{
    public class ProductGetByIdResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string? Brand { get; set; }

        public string? Description { get; set; }
    }
}

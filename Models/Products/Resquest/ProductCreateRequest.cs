using System.ComponentModel.DataAnnotations;

namespace ProductManagementBE.Models.Products.Resquest
{
    public class ProductCreateRequest
    {
        [MaxLength(255)]
        public required string Name { get; set; }

        public decimal Price { get; set; }

        [MaxLength(255)]
        public string? Brand { get; set; }

        public string? Description { get; set; }
    }
}

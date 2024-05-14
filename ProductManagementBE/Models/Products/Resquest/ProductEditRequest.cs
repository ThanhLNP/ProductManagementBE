using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementBE.Models.Products.Resquest
{
    public class ProductEditRequest
    {
        public decimal Price { get; set; }

        [MaxLength(255)]
        public string? Brand { get; set; }

        public string? Description { get; set; }

        public Guid CategoryId { get; set; }

        public required JValue Attributes { get; set; }
    }
}

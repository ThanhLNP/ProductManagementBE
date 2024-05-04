using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementBE.Entities
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [MaxLength(255)]
        public required string Name { get; set; }

        public decimal Price { get; set; }

        [MaxLength(255)]
        public string? Brand { get; set; }

        public string? Description { get; set; }

        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}

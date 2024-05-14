using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementBE.Entities
{
    [Table("ProductCategory")]
    public class ProductCategory : BaseEntity
    {
        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        [Required]
        [Column(TypeName = "jsonb")]
        public string Attributes { get; set; } = "{}";
    }
}

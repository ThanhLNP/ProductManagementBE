using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementBE.Entities
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        [MaxLength(255)]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "jsonb")]
        public string AllowedAttributes { get; set; } = "[]";

        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}

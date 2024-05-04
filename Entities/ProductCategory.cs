using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementBE.Entities
{
    [Table("ProductCategory")]
    public class ProductCategory : BaseEntity
    {
        public Guid ProductId { get; set; }

        public required Product Product { get; set; }

        public Guid CategoryId { get; set; }

        public required Category Category { get; set; }

        [Required]
        [Column(TypeName = "jsonb")]
        public string Attributes { get; set; } = "[]";
    }
}

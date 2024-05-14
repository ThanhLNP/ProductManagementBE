using System.ComponentModel.DataAnnotations;

namespace ProductManagementBE.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsDeleted { get; set; } = false;

        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

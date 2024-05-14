using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementBE.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [MaxLength(255)]
        public string? Address { get; set; }

        public bool? Gender { get; set; }
    }
}

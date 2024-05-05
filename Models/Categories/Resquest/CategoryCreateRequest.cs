using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementBE.Models.Categories.Resquest
{
    public class CategoryCreateRequest
    {
        [MaxLength(255)]
        public required string Name { get; set; }

        public required JValue AllowedAttributes { get; set; }
    }
}

using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementBE.Models.Categories.Resquest
{
    public class CategoryEditRequest
    {
        [Required]
        public JValue AllowedAttributes { get; set; }
    }
}

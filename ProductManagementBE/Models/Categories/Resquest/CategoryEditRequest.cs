using Newtonsoft.Json.Linq;

namespace ProductManagementBE.Models.Categories.Resquest
{
    public class CategoryEditRequest
    {
        public required JValue AllowedAttributes { get; set; }
    }
}

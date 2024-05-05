namespace ProductManagementBE.Models.Products.Resquest
{
    public class ProductListRequest
    {
        public bool IsDeleted { get; set; } = false;

        public List<Guid>? CategoryIds { get; set; }
    }
}

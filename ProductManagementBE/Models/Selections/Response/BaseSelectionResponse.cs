namespace ProductManagementBE.Models.Selections.Response
{
    public class BaseSelectionResponse
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
    }
}

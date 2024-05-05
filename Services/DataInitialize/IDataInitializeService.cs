namespace ProductManagementBE.Services.DataInitialize
{
    public interface IDataInitializeService
    {
        int Order { get; set; }

        Task RunAsync();
    }
}

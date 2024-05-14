using ProductManagementBE.Extensions;

namespace ProductManagementBE.Services.DataInitialize
{
    public class MigrationScriptsInitializeService : IDataInitializeService
    {
        private readonly ProductManagementDbContext _productManagementDbContext;

        public MigrationScriptsInitializeService(ProductManagementDbContext productManagementDbContext)
        {
            _productManagementDbContext = productManagementDbContext;
        }

        public int Order
        {
            get => 2;
            set { }
        }

        public async Task RunAsync()
        {
            await _productManagementDbContext.Database.MigrateScriptsAsync("ProductManagementBE");
        }
    }
}

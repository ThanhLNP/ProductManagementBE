using Dapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using ProductManagementBE.Utilities;

namespace ProductManagementBE.Extensions
{
    public static class DatabaseFacadeExtensions
    {
        public static async Task MigrateScriptsAsync(this DatabaseFacade databaseFacade, string assemblyName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) ArgumentNullException.ThrowIfNull(assemblyName);

            await databaseFacade.MigrateAsync(cancellationToken).ConfigureAwait(false);

            var allResources = ReflectionUtilities.LoadEmbeddedResources(assemblyName, "MigrationScripts");

            using var connection = new NpgsqlConnection(databaseFacade.GetConnectionString());

            foreach (var resource in allResources)
            {
                connection.Execute(resource);
            }
        }
    }
}

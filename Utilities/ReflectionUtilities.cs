using System.Reflection;

namespace ProductManagementBE.Utilities
{
    public static class ReflectionUtilities
    {
        public static Assembly GetAssembly(string assemblyName)
        {
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            return currentAssemblies.FirstOrDefault(a => a.FullName != null && a.FullName.Contains(assemblyName, StringComparison.OrdinalIgnoreCase));
        }

        public static string LoadEmbeddedResource(string resourceName, string assemblyName)
        {
            var assembly = GetAssembly(assemblyName);

            using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream is null)
                return string.Empty;

            using var reader = new StreamReader(stream);

            var result = reader.ReadToEnd();

            return result;
        }

        public static IEnumerable<string> LoadEmbeddedResources(string assemblyName, string folder)
        {
            var contents = new List<string>();

            var assembly = GetAssembly(assemblyName);

            var resourcePaths = assembly.GetManifestResourceNames().Where(_ => _.Contains(folder, StringComparison.OrdinalIgnoreCase));

            foreach (var resourcePath in resourcePaths)
            {
                contents.Add(LoadEmbeddedResource(resourcePath, assemblyName));
            }

            return contents;
        }
    }
}

using StructureMap.Configuration.DSL;

namespace Yogam.AMC.Infrastructure.Registries
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssembliesFromApplicationBaseDirectory(
                    a => a.FullName.StartsWith("Yogam")); // Needed if Registries are in different project than website
                scan.WithDefaultConventions();
            });
        }
    }
}

using Yogam.AMC.Infrastructure.Conventions;
using StructureMap.Configuration.DSL;

namespace Yogam.AMC.Infrastructure.Registries
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssembliesFromApplicationBaseDirectory(
                    a => a.FullName.StartsWith("Yogam")); // Needed if Registries are in different project than website
                scan.With(new ControllerConvention());
            });
        }
    }
}

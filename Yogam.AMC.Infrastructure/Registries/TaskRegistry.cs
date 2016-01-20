using Yogam.AMC.Infrastructure.Tasks;
using StructureMap.Configuration.DSL;

namespace Yogam.AMC.Infrastructure.Registries
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(
                    a => a.FullName.StartsWith("Yogam"));
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartup>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
                scan.AddAllTypesOf<IRunAfterEachRequest>();
            });
        }
    }
}

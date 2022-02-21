using Identity.Api.Infrastructure.Registries;
using StructureMap;

namespace Identity.Api.Infrastructure
{
    public class ApplicationFramework
    {
        public static IContainer BootstarpContainersForIdentityApi(IConfigurationRoot configuration, 
            Registry? registry = null)
        {
            var registryToUse = registry ?? new Registry();
            registryToUse.For<IConfigurationRoot>().Use(configuration);
            registryToUse.IncludeRegistry<RepositoryRegistry>();

            return new Container(registryToUse);
        }
    }
}

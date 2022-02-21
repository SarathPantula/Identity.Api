using Identity.Api.IdentityManagers;
using StructureMap;

namespace Identity.Api.Infrastructure.Registries
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<IIdentityManager>().Use<IdentityCacheManager>();
            For<IIdentityManager>().DecorateAllWith<IdentityValidationManager>();
            For<IIdentityManager>().DecorateAllWith<IdentitySecurityManager>();
        }
    }
}

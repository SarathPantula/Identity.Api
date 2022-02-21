namespace Identity.Api.IdentityManagers
{
    public class IdentitySecurityManager : IIdentityManager
    {
        private readonly IIdentityManager _identityManager;

        public IdentitySecurityManager(Func<string, IIdentityManager> identityManager)
        {
            _identityManager = identityManager("Cache"); 
        }
        public bool CanLogin()
        {
            var temp = true;

            return _identityManager.CanLogin();
        }
    }
}

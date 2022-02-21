namespace Identity.Api.IdentityManagers
{
    public class IdentityValidationManager : IIdentityManager
    {
        private readonly IIdentityManager _identityManager;

        public IdentityValidationManager(Func<string, IIdentityManager> identityManager)
        {
            _identityManager = identityManager("Security"); 
        }
        public bool CanLogin()
        {
            var temp = true;

            return _identityManager.CanLogin();
        }
    }
}

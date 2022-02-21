using Identity.Api.IdentityManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IIdentityManager _identityManager;
        public LoginController(Func<string, IIdentityManager> identityManager)
        {
            _identityManager = identityManager("Validation");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public IActionResult Login()
        {
            return Ok(_identityManager.CanLogin());
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api
{
    [Route("test")]
    public class TestController : ControllerBase
    {
        public IActionResult Get()
        {
            return new JsonResult("OK");
        }

        [Route("identity")]
        [Authorize]
        public IActionResult Identity()
        {
            var claims = from c in User.Claims select new { c.Type, c.Value };

            var sidValue = User.Claims.Where(x => x.Type == "sid").FirstOrDefault();
            var jtiValue = User.Claims.Where(x => x.Type == "jti").FirstOrDefault();
            
            _enforcer.AssertAuthorized("B", "blah");
            
            return new JsonResult(claims);
        }

        private IEnforcePermissions _enforcer;

        public TestController(IEnforcePermissions enforcer)
        {
            _enforcer = enforcer;
        }
    }
}
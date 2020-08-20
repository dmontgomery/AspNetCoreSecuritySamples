using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Host.Controller
{
    [Route("backchannellogout")]
    public class BackChannelLogoutController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            // this is the code that should be calling us:
            // https://github.com/IdentityServer/IdentityServer4/blob/53d3b7f5c4415a733d8392b3c3a85c3adf17646b/src/IdentityServer4/src/Services/Default/DefaultBackChannelLogoutService.cs
            
            var r = Request;
            return Ok();
        }
        
    }
}
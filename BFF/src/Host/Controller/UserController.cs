using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IdentityModel.AspNetCore;
using Microsoft.AspNetCore.Authentication;

namespace Host.Controller
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        private IHttpClientFactory _clientFactory;
        private ISessionAssessorFake faker;

        public UserController(IHttpClientFactory clientFactory, ISessionAssessorFake faker)
        {
            _clientFactory = clientFactory;
            this.faker = faker;
        }

        [Route("info")]
        public IActionResult GetUser()
        {
            var user = new { name = User.Identity.Name };

            return new JsonResult(faker.SomeData);
        }
        
        [Route("info-external")]
        public async Task<IActionResult> GetUserFromExternalApi()
        {
            var client = _clientFactory.CreateClient("user_client");
            var user = await client.GetStringAsync("test/identity");
            
            
            

            return new JsonResult(user);
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            return SignOut("cookies", "oidc");
        }
    }
}
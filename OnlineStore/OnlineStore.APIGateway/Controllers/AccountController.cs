using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.Account;
using OnlineStore.Communication.Account.Models;

namespace OnlineStore.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginRequestModel model)
        {
            var userManagementProxy = ServiceProxy.Create<IAccount>(new Uri("fabric:/OnlineStore/OnlineStore.UserManagement"));
            var result = await userManagementProxy.Login(model);

            if (result is null)
            {
                return Unauthorized("You do not have an account! Register first");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterRequestModel model)
        {
            var userManagementProxy = ServiceProxy.Create<IAccount>(new Uri("fabric:/OnlineStore/OnlineStore.UserManagement"));
            var result = await userManagementProxy.Register(model);

            if (result is null)
            {
                return BadRequest("Something went wrong!");
            }

            return Ok(result);
        }
    }
}

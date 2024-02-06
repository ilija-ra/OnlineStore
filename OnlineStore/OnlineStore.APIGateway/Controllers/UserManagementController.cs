using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.UserManagement;
using OnlineStore.Communication.UserManagement.Models;

namespace OnlineStore.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        [HttpGet]
        [Route("GetById/{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var userManagementProxy = ServiceProxy.Create<IUserManagement>(new Uri("fabric:/OnlineStore/OnlineStore.UserManagement"));
            var result = await userManagementProxy.UserGetById(userId);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UserManagementUserUpdateRequestModel model)
        {
            var userManagementProxy = ServiceProxy.Create<IUserManagement>(new Uri("fabric:/OnlineStore/OnlineStore.UserManagement"));
            var result = await userManagementProxy.UserUpdate(model);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetPurchaseHistory/{userId}")]
        public async Task<IActionResult> GetPurchaseHistory(string userId)
        {
            var userManagementProxy = ServiceProxy.Create<IUserManagement>(new Uri("fabric:/OnlineStore/OnlineStore.UserManagement"));
            var result = await userManagementProxy.PurchaseGetAll(userId);

            return Ok(result);
        }
    }
}

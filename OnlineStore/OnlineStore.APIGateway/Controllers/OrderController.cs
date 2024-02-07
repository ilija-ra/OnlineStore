using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.Order;
using OnlineStore.Communication.Order.Models;

namespace OnlineStore.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        [Route("PurchaseConfirm")]
        public async Task<IActionResult> PurchaseConfirm([FromBody] OrderPurchaseConfirmRequestModel model)
        {
            var orderProxy = ServiceProxy.Create<IOrder>(new Uri("fabric:/OnlineStore/OnlineStore.Order"));
            var result = await orderProxy.PurchaseConfirm(model);

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("PurchaseGetAll/{userId}")]
        public async Task<IActionResult> PurchaseGetAll(string userId)
        {
            var orderProxy = ServiceProxy.Create<IOrder>(new Uri("fabric:/OnlineStore/OnlineStore.Order"));
            var result = await orderProxy.PurchaseGetAll(userId);

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}

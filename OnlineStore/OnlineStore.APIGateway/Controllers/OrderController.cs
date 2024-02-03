using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.Order;
using OnlineStore.Communication.Order.Models;

namespace OnlineStore.APIGateway.Controllers
{
    public class OrderController : ControllerBase
    {
        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> Confirm([FromBody] OrderConfirmRequestModel model)
        {
            var orderProxy = ServiceProxy.Create<IOrder>(new Uri("fabric:/OnlineStore/OnlineStore.Order"));
            var result = await orderProxy.Confirm(model);

            return Ok(result);
        }
    }
}

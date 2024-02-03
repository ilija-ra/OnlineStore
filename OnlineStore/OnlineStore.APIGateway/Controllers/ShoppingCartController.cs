using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.ShoppingCart;

namespace OnlineStore.APIGateway.Controllers
{
    public class ShoppingCartController : ControllerBase
    {
        // Add TODO:
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add()
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"));
            var result = await shoppingCartProxy.Add("model");

            return Ok(result);
        }

        // Remove TODO:
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> Remove()
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"));
            var result = await shoppingCartProxy.Remove("model");

            return Ok(result);
        }
    }
}

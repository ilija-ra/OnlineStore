using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.ShoppingCart;
using OnlineStore.Communication.ShoppingCart.Models;
using System.Fabric;

namespace OnlineStore.APIGateway.Controllers
{
    public class ShoppingCartController : ControllerBase
    {
        [HttpPost]
        [Route("Add/{userId}")]
        public async Task<IActionResult> Add([FromBody] ShoppingCartProductAddRequestModel model, string userId)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.Add(model, userId);

            return Ok(result);
        }

        [HttpPost]
        [Route("IncreaseQuantity/{productId:long}/{userId}")]
        public async Task<IActionResult> IncreaseQuantity(long productId, string userId)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.IncreaseQuantity(productId, userId);

            return Ok(result);
        }

        [HttpPost]
        [Route("Remove/{userId}")]
        public async Task<IActionResult> Remove([FromBody] ShoppingCartProductRemoveRequestModel model, string userId)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.Remove(model, userId);

            return Ok(result);
        }

        [HttpPost]
        [Route("DecreaseQuantity/{productId:long}/{userId}")]
        public async Task<IActionResult> DecreaseQuantity(long productId, string userId)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.DecreaseQuantity(productId, userId);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll/{userId}")]
        public async Task<IActionResult> GetAll(string? userId)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.GetAll(userId);

            return Ok(result);
        }

        private async Task<ServicePartitionKey> getAvailablePartitionKey()
        {
            var partitionKey = new ServicePartitionKey();
            var fabricClient = new FabricClient();
            var availablePartitions = await fabricClient.QueryManager.GetPartitionListAsync(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"));

            foreach (var partition in availablePartitions)
            {
                var key = partition.PartitionInformation as Int64RangePartitionInformation;

                if (key == null)
                {
                    continue;
                }

                partitionKey = new ServicePartitionKey(key.LowKey);
            }

            return partitionKey;
        }
    }
}

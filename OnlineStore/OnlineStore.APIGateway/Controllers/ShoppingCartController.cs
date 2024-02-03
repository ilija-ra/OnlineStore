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
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] ShoppingCartProductAddRequestModel model)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.Add(model);

            return Ok(result);
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> Remove([FromBody] ShoppingCartProductRemoveRequestModel model)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.Remove(model);

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

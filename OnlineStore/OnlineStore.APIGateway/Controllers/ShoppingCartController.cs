using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.ShoppingCart;
using OnlineStore.Communication.ShoppingCart.Models;
using System.Fabric;

namespace OnlineStore.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        [HttpPost]
        [Route("Add/{userId}")]
        public async Task<IActionResult> Add([FromBody] ShoppingCartProductAddRequestModel model, string userId)
        {
            var result = new ShoppingCartProductAddResponseModel();
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            //var productCatalogProxy = ServiceProxy.Create<IProductCatalog>(new Uri("fabric:/OnlineStore/OnlineStore.ProductCatalog"));

            //var product = await productCatalogProxy.GetById(model.Id);

            //if (product == null || (product.Quantity - model.Quantity) < 1)
            //{
            //    return Ok(result);
            //}

            result = await shoppingCartProxy.Add(model, userId);

            return Ok(result);
        }

        [HttpPost]
        [Route("IncreaseQuantity/{productId:long}/{userId}")]
        public async Task<IActionResult> IncreaseQuantity(long productId, string userId)
        {
            var result = new ShoppingCartQuantityIncreaseResponseModel();
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            //var productCatalogProxy = ServiceProxy.Create<IProductCatalog>(new Uri("fabric:/OnlineStore/OnlineStore.ProductCatalog"));

            //var product = await productCatalogProxy.GetById(productId);

            //if (product == null || (product.Quantity - 1) < 1)
            //{
            //    return Ok(result);
            //}

            result = await shoppingCartProxy.IncreaseQuantity(productId, userId);

            return Ok(result);
        }

        [HttpPost]
        [Route("Remove/{productId:long}/{userId}")]
        public async Task<IActionResult> Remove(long productId, string userId)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.Remove(productId, userId);

            return Ok(result);
        }

        [HttpPost]
        [Route("DecreaseQuantity/{productId:long}/{userId}")]
        public async Task<IActionResult> DecreaseQuantity(long productId, string userId)
        {
            var result = new ShoppingCartQuantityDecreaseResponseModel();
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            //var productCatalogProxy = ServiceProxy.Create<IProductCatalog>(new Uri("fabric:/OnlineStore/OnlineStore.ProductCatalog"));

            //var productFromCart = (await shoppingCartProxy.GetAll(userId)).Items!.Where(item => item.Id == productId).FirstOrDefault();
            //var productFromDb = await productCatalogProxy.GetById(productId);

            //if (productFromDb == null || productFromCart == null || (productFromCart.Quantity + 1) > productFromDb.Quantity)
            //{
            //    return Ok(result);
            //}

            result = await shoppingCartProxy.DecreaseQuantity(productId, userId);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll/{userId}")]
        public async Task<IActionResult> GetAll(string? userId)
        {
            var shoppingCartProxy = ServiceProxy.Create<IShoppingCart>(new Uri("fabric:/OnlineStore/OnlineStore.ShoppingCart"), await getAvailablePartitionKey());
            var result = await shoppingCartProxy.GetAll(userId);

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        private async Task<ServicePartitionKey> getAvailablePartitionKey()
        {
            var partitionKey = new ServicePartitionKey();

            //string serviceName = "";
            var fabricClient = new FabricClient();
            //int partitionNumber = (await fabricClient.QueryManager.GetPartitionListAsync(new Uri($"fabric:/OnlineStore/{serviceName}"))).Count;

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

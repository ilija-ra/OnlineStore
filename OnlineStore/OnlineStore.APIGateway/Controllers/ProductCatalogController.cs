using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.ProductCatalog;

namespace OnlineStore.APIGateway.Controllers
{
    public class ProductCatalogController : ControllerBase
    {
        [HttpPost]
        [Route("Search/{query}")]
        public async Task<IActionResult> Search(string query)
        {
            var productCatalogProxy = ServiceProxy.Create<IProductCatalog>(new Uri("fabric:/OnlineStore/OnlineStore.ProductCatalog"));
            var result = await productCatalogProxy.Search(query);

            return Ok(result);
        }
    }
}

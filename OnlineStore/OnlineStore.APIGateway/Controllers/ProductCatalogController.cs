using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using OnlineStore.Communication.ProductCatalog;
using OnlineStore.Communication.ProductCatalog.Models;

namespace OnlineStore.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCatalogController : ControllerBase
    {
        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> Search([FromBody] ProductCatalogProductSearchRequestModel model)
        {
            var productCatalogProxy = ServiceProxy.Create<IProductCatalog>(new Uri("fabric:/OnlineStore/OnlineStore.ProductCatalog"));
            var result = await productCatalogProxy.Search(model);

            return Ok(result);
        }
    }
}

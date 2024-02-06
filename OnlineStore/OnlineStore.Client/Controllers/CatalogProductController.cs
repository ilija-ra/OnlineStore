using Microsoft.AspNetCore.Mvc;
using OnlineStore.Client.Models.CatalogProduct;
using System.Text;
using System.Text.Json;

namespace OnlineStore.Client.Controllers
{
    public class CatalogProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiGatewayUrl;

        public CatalogProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiGatewayUrl = "http://localhost:8363";
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts(QueryViewModel model)
        {
            model = model is null ? new QueryViewModel() : model;

            var jsonModel = JsonSerializer.Serialize(model);

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            var response = await _httpClientFactory.CreateClient().PostAsync($"{_apiGatewayUrl}/Search", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<CatalogProductListViewModel>(jsonResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(result);
            }
            else
            {
                return View("Error");
            }
        }
    }
}

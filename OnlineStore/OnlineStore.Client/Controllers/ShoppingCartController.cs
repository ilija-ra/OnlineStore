using Microsoft.AspNetCore.Mvc;
using OnlineStore.Client.Models.CatalogProduct;
using System.Text;
using System.Text.Json;

namespace OnlineStore.Client.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiGatewayUrl;

        public ShoppingCartController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiGatewayUrl = "http://localhost:8363";
        }

        [HttpGet]
        [Route("AddProduct")]
        public async Task<string> AddProduct(long id, string name, string description, double price, long quantity, string category, string userId)
        {
            var model = new CatalogProductViewModel()
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                Quantity = quantity,
                Category = category
            };

            var jsonModel = JsonSerializer.Serialize(model);

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            var response = await _httpClientFactory.CreateClient().PostAsync($"{_apiGatewayUrl}/Add/{userId}", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                //var result = JsonSerializer.Deserialize<CatalogProductListViewModel>(jsonResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                ViewBag.ResponseMessage = jsonResult;

                return jsonResult;
            }
            else
            {
                return "Error";
            }
        }

        [HttpGet]
        [Route("GetCartProducts")]
        public async Task<IActionResult> GetCartProducts(string userId)
        {
            var response = await _httpClientFactory.CreateClient().GetAsync($"{_apiGatewayUrl}/GetAll/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<ShoppingCartListViewModel>(jsonResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(result);
            }
            else
            {
                return View("Error");
            }
        }
    }
}

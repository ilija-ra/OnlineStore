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
        public async Task<IActionResult> AddProduct(long productId, string name, string description, double price, string category, string query, string userId = "1111")
        {
            var model = new CatalogProductViewModel()
            {
                Id = productId,
                Name = name,
                Description = description,
                Price = price,
                Quantity = 1,
                Category = category
            };

            var jsonModel = JsonSerializer.Serialize(model);

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            var response = await _httpClientFactory.CreateClient().PostAsync($"{_apiGatewayUrl}/ShoppingCart/Add/{userId}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Search", "CatalogProduct", new QueryViewModel() { Query = query });
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Route("IncreaseQuantity")]
        public async Task<IActionResult> IncreaseQuantity(long productId, string userId = "1111")
        {
            var response = await _httpClientFactory.CreateClient().PostAsync($"{_apiGatewayUrl}/ShoppingCart/IncreaseQuantity/{productId}/{userId}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetCartProducts");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Route("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(long productId, string userId = "1111")
        {
            var response = await _httpClientFactory.CreateClient().PostAsync($"{_apiGatewayUrl}/ShoppingCart/Remove/{productId}/{userId}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetCartProducts");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Route("DecreaseQuantity")]
        public async Task<IActionResult> DecreaseQuantity(long productId, string userId = "1111")
        {
            var response = await _httpClientFactory.CreateClient().PostAsync($"{_apiGatewayUrl}/ShoppingCart/DecreaseQuantity/{productId}/{userId}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetCartProducts");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Route("GetCartProducts")]
        public async Task<IActionResult> GetCartProducts(string userId = "1111")
        {
            var response = await _httpClientFactory.CreateClient().GetAsync($"{_apiGatewayUrl}/ShoppingCart/GetAll/{userId}");

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

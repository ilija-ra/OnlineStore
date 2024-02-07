using Microsoft.AspNetCore.Mvc;
using OnlineStore.Client.Models.CatalogProduct;
using OnlineStore.Client.Models.Order;
using System.Text;
using System.Text.Json;

namespace OnlineStore.Client.Controllers
{
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiGatewayUrl;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiGatewayUrl = "http://localhost:8363";
        }

        [HttpGet]
        [Route("Confirm")]
        public async Task<IActionResult> Confirm()
        {
            string userId = "111";

            var response = await _httpClientFactory.CreateClient().GetAsync($"{_apiGatewayUrl}/ShoppingCart/GetAll/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var model = new PurchaseConfirmViewModel();
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ShoppingCartListViewModel>(jsonResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result != null)
                {
                    model.Products = productNames(result.Items!.ToList());
                    model.TotalAmount = calculateTotalAmount(result.Items!.ToList());
                }

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Route("PurchaseConfirm")]
        public async Task<IActionResult> PurchaseConfirm(PurchaseConfirmViewModel model)
        {
            if (model.PaymentMethod == "CashOnDelivery")
            {
                model.CardNumber = null;
            }
            model.UserId = "111";

            var jsonModel = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            var response = await _httpClientFactory.CreateClient().PostAsync($"{_apiGatewayUrl}/Order/PurchaseConfirm/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Search", "CatalogProduct", new QueryViewModel() { Query = null });
            }
            else
            {
                return View("Error");
            }
        }

        private string productNames(List<ShoppingCartViewModel> items)
        {
            var productNames = string.Empty;

            foreach (var item in items)
            {
                productNames += " " + item.Name;
            }

            return productNames;
        }

        private double calculateTotalAmount(List<ShoppingCartViewModel> items)
        {
            double totalAmount = 0;

            foreach (var item in items)
            {
                totalAmount += (item.Quantity!.Value * item.Price!.Value);
            }

            return totalAmount;
        }
    }
}

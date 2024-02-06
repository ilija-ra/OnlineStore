using Microsoft.AspNetCore.Mvc;
using OnlineStore.Client.Models.UserManagement;
using System.Text;
using System.Text.Json;

namespace OnlineStore.Client.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiGatewayUrl;

        public UserManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiGatewayUrl = "http://localhost:8363";
        }

        [HttpGet]
        [Route("UserProfile")]
        public async Task<IActionResult> UserProfile()
        {
            string userId = "111";
            var response = await _httpClientFactory.CreateClient().GetAsync($"{_apiGatewayUrl}/UserManagement/GetById/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<UserInformationViewModel>(jsonResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(result);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Route("UserProfile")]
        public async Task<IActionResult> UserProfile(UserInformationViewModel model)
        {
            var jsonModel = JsonSerializer.Serialize(model);

            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            var response = await _httpClientFactory.CreateClient().PutAsync($"{_apiGatewayUrl}/UserManagement/Update", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<UserInformationViewModel>(jsonResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(result);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Route("PurchaseHistory")]
        public async Task<IActionResult> PurchaseHistory()
        {
            string userId = "111";
            var response = await _httpClientFactory.CreateClient().GetAsync($"{_apiGatewayUrl}/UserManagement/GetPurchaseHistory/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<PurchaseHistoryListViewModel>(jsonResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(result);
            }
            else
            {
                return View("Error");
            }
        }
    }
}

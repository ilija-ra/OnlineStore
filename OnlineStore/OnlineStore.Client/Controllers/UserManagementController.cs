using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Client.Controllers
{
    public class UserManagementController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> Profile()
        {
            return View();
        }
    }
}

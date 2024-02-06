using Microsoft.AspNetCore.Mvc;
using OnlineStore.Client.Models.Account;

namespace OnlineStore.Client.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}

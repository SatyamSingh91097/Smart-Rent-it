using Microsoft.AspNetCore.Mvc;
using Smart_Rent_it.Models;

namespace Smart_Rent_it.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Email == "admin@test.com" && model.Password == "123")
            {
                HttpContext.Session.SetString("UserEmail", model.Email);
                return RedirectToAction("Index", "Product"); // Product controller par bhejo
            }
            ViewBag.Error = "Invalid Login!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
    }
}

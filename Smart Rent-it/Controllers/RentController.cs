using Microsoft.AspNetCore.Mvc;
using Smart_Rent_it.Models;

namespace Smart_Rent_it.Controllers
{
    public class RentController : Controller
    {
        // Create Item (GET)
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
                return RedirectToAction("Login", "Account");
            return View();
        }

        // Create Item (POST)
        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            newProduct.Id = ProductController._products.Max(p => p.Id) + 1;
            newProduct.ImageUrl = "https://picsum.photos/id/50/200/150";
            ProductController._products.Add(newProduct);
            return RedirectToAction("Index", "Product");
        }

        // Rent Confirmation
        public IActionResult Confirm(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
                return RedirectToAction("Login", "Account");

            var product = ProductController._products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
    }
}

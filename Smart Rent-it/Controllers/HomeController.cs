using Microsoft.AspNetCore.Mvc;
using Smart_Rent_it.Models;
using System.Diagnostics;

namespace Smart_Rent_it.Controllers
{
    public class HomeController : Controller
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "DSLR Camera", Description = "Canon EOS R5 for professional photography.", PricePerDay = 1500, ImageUrl = "https://picsum.photos/200/150?random=1", IsAvailable = true },
            new Product { Id = 2, Name = "Camping Tent", Description = "4-person waterproof tent for mountains.", PricePerDay = 500, ImageUrl = "https://picsum.photos/200/150?random=2", IsAvailable = true },
            new Product { Id = 3, Name = "Electric Drill", Description = "High-power drill for home improvement.", PricePerDay = 300, ImageUrl = "https://picsum.photos/200/150?random=3", IsAvailable = false }
        };

        public IActionResult Index()
        {
            return View(_products);
        }

        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // Login GET
        public IActionResult Login()
        {
            return View();
        }

        // Login POST
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Email == "admin@test.com" && model.Password == "123")
            {
                HttpContext.Session.SetString("UserEmail", model.Email);
                return RedirectToAction("Create");
            }
            ViewBag.Error = "Invalid Login Details!";
            return View();
        }

        // Create Item GET
        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // Create Item POST
        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            newProduct.Id = _products.Max(p => p.Id) + 1;
            newProduct.ImageUrl = "https://picsum.photos/id/20/200/150";
            _products.Add(newProduct);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy() 
        {
            return View();
        }
    }
}

